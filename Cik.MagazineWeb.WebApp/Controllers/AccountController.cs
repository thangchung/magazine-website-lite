using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Cik.MagazineWeb.Application;
using Cik.MagazineWeb.Application.UserAuthentications.ViewModels;
using Cik.MagazineWeb.Domain.UserMgmt;
using Cik.MagazineWeb.Utilities;
using Cik.MagazineWeb.Utilities.Encyption;
using Cik.MagazineWeb.WebApp.App_Start.Filters;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;

namespace Cik.MagazineWeb.WebApp.Controllers
{
    [Authorize]
    [InitializeSimpleMembership] 
    public class AccountController : Controller
    {
        private readonly IEncrypting _encryptor;
        private readonly IUserApplication _userApplication;

        private const string DefaultPassword = "Hello123";
        private const string DefaultEmail = "thangchung@ymail.com";
        private const string DefaultCreatedByUser = "thangchung";

        public AccountController(IEncrypting encryptor, IUserApplication userApplication)
        {
            Guard.ArgumentNotNull(encryptor, "SimpleEncryptor");
            Guard.ArgumentNotNull(userApplication, "UserApplication");

            _encryptor = encryptor;
            _userApplication = userApplication;
        }

        // 
        // GET: /Account/Login 
 
        [AllowAnonymous] 
        public ActionResult Login(string returnUrl) 
        { 
            ViewBag.ReturnUrl = returnUrl; 
            return View(); 
        } 
 
        // 
        // POST: /Account/Login 
 
        [HttpPost] 
        [AllowAnonymous] 
        [ValidateAntiForgeryToken] 
        public ActionResult Login(LoginViewModel model, string returnUrl) 
        { 
            var encodePassword = _encryptor.Encode(model.Password);

            if (ModelState.IsValid && WebSecurity.Login(model.UserName, encodePassword, persistCookie: model.RememberMe)) 
            { 
                return RedirectToLocal(returnUrl); 
            } 
 
            // If we got this far, something failed, redisplay form 
            ModelState.AddModelError("", "The user name or password provided is incorrect."); 
            return View(model); 
        } 
 
        // 
        // POST: /Account/LogOff 
 
        [HttpPost] 
        // [ValidateAntiForgeryToken] 
        public ActionResult LogOff() 
        { 
            WebSecurity.Logout(); 
 
            return RedirectToAction("Login", "Account"); 
        } 
 
        // 
        // GET: /Account/Register 
 
        [AllowAnonymous] 
        public ActionResult Register() 
        { 
            return View(); 
        } 
 
        // 
        // POST: /Account/Register 
 
        [HttpPost] 
        [AllowAnonymous] 
        [ValidateAntiForgeryToken] 
        public ActionResult Register(RegisterViewModel model) 
        { 
            if (ModelState.IsValid) 
            { 
                // Attempt to register the user 
                try 
                {
                    //var values = new Dictionary<string, object>();
                    //values.Add("DisplayName", model.UserName);
                    //values.Add("Password", model.Password);
                    //values.Add("Email", model.UserName + "@cik.com");
                    //values.Add("CreatedBy", model.UserName);
                    //values.Add("CreatedDate", DateTime.UtcNow);
                    //values.Add("Role", 1);
                    var encodePassword = _encryptor.Encode(model.Password);
                    WebSecurity.CreateUserAndAccount(model.UserName, encodePassword, new
                        {
                            DisplayName = model.UserName,
                            Password = encodePassword,
                            Email = model.UserName + "@cik.com",
                            CreatedBy = model.UserName,
                            CreatedDate = DateTime.UtcNow,
                            Role = 1
                        });
                    WebSecurity.Login(model.UserName, encodePassword);
                    return RedirectToAction("Index", "Dashboard"); 
                } 
                catch (MembershipCreateUserException e) 
                { 
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode)); 
                } 
            } 
 
            // If we got this far, something failed, redisplay form 
            return View(model); 
        } 
 
        // 
        // POST: /Account/Disassociate 
 
        [HttpPost] 
        [ValidateAntiForgeryToken] 
        public ActionResult Disassociate(string provider, string providerUserId) 
        { 
            string ownerAccount = OAuthWebSecurity.GetUserName(provider, providerUserId); 
            ManageMessageId? message = null; 
 
            // Only disassociate the account if the currently logged in user is the owner 
            if (ownerAccount == User.Identity.Name) 
            { 
                // Use a transaction to prevent the user from deleting their last login credential 
                using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = (System.Transactions.IsolationLevel)IsolationLevel.Serializable })) 
                { 
                    bool hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name)); 
                    if (hasLocalAccount || OAuthWebSecurity.GetAccountsFromUserName(User.Identity.Name).Count > 1) 
                    { 
                        OAuthWebSecurity.DeleteAccount(provider, providerUserId); 
                        scope.Complete(); 
                        message = ManageMessageId.RemoveLoginSuccess; 
                    } 
                } 
            } 
 
            return RedirectToAction("Manage", new { Message = message }); 
        } 
 
        // 
        // GET: /Account/Manage 
 
        public ActionResult Manage(ManageMessageId? message) 
        { 
            ViewBag.StatusMessage = 
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed." 
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set." 
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed." 
                : ""; 
            ViewBag.HasLocalPassword = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name)); 
            ViewBag.ReturnUrl = Url.Action("Manage"); 
            return View(); 
        } 
 
        // 
        // POST: /Account/Manage 
 
        [HttpPost] 
        [ValidateAntiForgeryToken] 
        public ActionResult Manage(LocalPasswordViewModel model) 
        { 
            bool hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name)); 
            ViewBag.HasLocalPassword = hasLocalAccount; 
            ViewBag.ReturnUrl = Url.Action("Manage"); 
            if (hasLocalAccount) 
            { 
                if (ModelState.IsValid) 
                { 
                    // ChangePassword will throw an exception rather than return false in certain failure scenarios. 
                    bool changePasswordSucceeded; 
                    try 
                    { 
                        changePasswordSucceeded = 
                            WebSecurity.ChangePassword(User.Identity.Name, 
                                _encryptor.Encode(model.OldPassword), 
                                _encryptor.Encode(model.NewPassword)); 
                    } 
                    catch (Exception) 
                    { 
                        changePasswordSucceeded = false; 
                    } 
 
                    if (changePasswordSucceeded) 
                    { 
                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess }); 
                    } 
                    else 
                    { 
                        ModelState.AddModelError("", "The current password is incorrect or the new password is invalid."); 
                    } 
                } 
            } 
            else 
            { 
                // User does not have a local password so remove any validation errors caused by a missing 
                // OldPassword field 
                ModelState state = ModelState["OldPassword"]; 
                if (state != null) 
                { 
                    state.Errors.Clear(); 
                } 
 
                if (ModelState.IsValid) 
                { 
                    try 
                    { 
                        WebSecurity.CreateAccount(User.Identity.Name, _encryptor.Encode(model.NewPassword)); 
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess }); 
                    } 
                    catch (Exception e) 
                    { 
                        ModelState.AddModelError("", e); 
                    } 
                } 
            } 
 
            // If we got this far, something failed, redisplay form 
            return View(model); 
        } 
 
        // 
        // POST: /Account/ExternalLogin 
 
        [HttpPost] 
        [AllowAnonymous] 
        [ValidateAntiForgeryToken] 
        public ActionResult ExternalLogin(string provider, string returnUrl) 
        { 
            return new ExternalLoginResult(provider, Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl })); 
        } 
 
        // 
        // GET: /Account/ExternalLoginCallback 
 
        [AllowAnonymous] 
        public ActionResult ExternalLoginCallback(string returnUrl) 
        { 
            AuthenticationResult result = OAuthWebSecurity.VerifyAuthentication(Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl })); 
            if (!result.IsSuccessful) 
            { 
                return RedirectToAction("ExternalLoginFailure"); 
            } 
 
            if (OAuthWebSecurity.Login(result.Provider, result.ProviderUserId, createPersistentCookie: false)) 
            { 
                return RedirectToLocal(returnUrl); 
            } 
 
            if (User.Identity.IsAuthenticated) 
            { 
                // If the current user is logged in add the new account 
                OAuthWebSecurity.CreateOrUpdateAccount(result.Provider, result.ProviderUserId, User.Identity.Name); 
                return RedirectToLocal(returnUrl); 
            } 
            else 
            { 
                // User is new, ask for their desired membership name 
                string loginData = OAuthWebSecurity.SerializeProviderUserId(result.Provider, result.ProviderUserId); 
                ViewBag.ProviderDisplayName = OAuthWebSecurity.GetOAuthClientData(result.Provider).DisplayName; 
                ViewBag.ReturnUrl = returnUrl; 
                return View("ExternalLoginConfirmation", new RegisterExternalLoginViewModel { UserName = result.UserName, ExternalLoginData = loginData }); 
            } 
        } 
 
        // 
        // POST: /Account/ExternalLoginConfirmation 
 
        [HttpPost] 
        [AllowAnonymous] 
        [ValidateAntiForgeryToken] 
        public ActionResult ExternalLoginConfirmation(RegisterExternalLoginViewModel model, string returnUrl) 
        { 
            string provider = null; 
            string providerUserId = null; 
 
            if (User.Identity.IsAuthenticated || !OAuthWebSecurity.TryDeserializeProviderUserId(model.ExternalLoginData, out provider, out providerUserId)) 
            { 
                return RedirectToAction("Manage"); 
            } 
 
            if (ModelState.IsValid) 
            {
                var user = _userApplication.GetUserByUserName(model.UserName); 
 
                // if does not have any user with this username 
                if (user == null) 
                {
                    _userApplication.CreateUser( 
                        model.UserName, 
                        model.UserName, 
                        DefaultPassword, 
                        DefaultEmail,
                        (int)Role.RegisterUser, 
                        DefaultCreatedByUser);
 
                    OAuthWebSecurity.CreateOrUpdateAccount(provider, providerUserId, model.UserName); 
                    OAuthWebSecurity.Login(provider, providerUserId, createPersistentCookie: false); 
 
                    return RedirectToLocal(returnUrl); 
                } 
 
                this.ModelState.AddModelError("UserName", "User name already exists. Please enter a different user name."); 
            } 
 
            ViewBag.ProviderDisplayName = OAuthWebSecurity.GetOAuthClientData(provider).DisplayName; 
            ViewBag.ReturnUrl = returnUrl; 
            return View(model); 
        }
 
        // 
        // GET: /Account/ExternalLoginFailure 
 
        [AllowAnonymous] 
        public ActionResult ExternalLoginFailure() 
        { 
            return View(); 
        } 
 
        [AllowAnonymous] 
        [ChildActionOnly] 
        public ActionResult ExternalLoginsList(string returnUrl) 
        { 
            ViewBag.ReturnUrl = returnUrl; 
            return PartialView("_ExternalLoginsListPartial", OAuthWebSecurity.RegisteredClientData); 
        } 
 
        [ChildActionOnly] 
        public ActionResult RemoveExternalLogins() 
        { 
            ICollection<OAuthAccount> accounts = OAuthWebSecurity.GetAccountsFromUserName(User.Identity.Name); 
            List<ExternalLogin> externalLogins = new List<ExternalLogin>(); 
            foreach (OAuthAccount account in accounts) 
            { 
                AuthenticationClientData clientData = OAuthWebSecurity.GetOAuthClientData(account.Provider); 
 
                externalLogins.Add(new ExternalLogin 
                { 
                    Provider = account.Provider, 
                    ProviderDisplayName = clientData.DisplayName, 
                    ProviderUserId = account.ProviderUserId, 
                }); 
            } 
 
            ViewBag.ShowRemoveButton = externalLogins.Count > 1 || OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name)); 
            return PartialView("_RemoveExternalLoginsPartial", externalLogins); 
        } 
 
        #region Helpers 
        private ActionResult RedirectToLocal(string returnUrl) 
        { 
            if (Url.IsLocalUrl(returnUrl)) 
            { 
                return Redirect(returnUrl); 
            }

            return this.RedirectToAction("Index", "Dashboard"); 
        } 
 
        public enum ManageMessageId 
        { 
            ChangePasswordSuccess, 
            SetPasswordSuccess, 
            RemoveLoginSuccess, 
        } 
 
        internal class ExternalLoginResult : ActionResult 
        { 
            public ExternalLoginResult(string provider, string returnUrl) 
            { 
                Provider = provider; 
                ReturnUrl = returnUrl; 
            } 
 
            public string Provider { get; private set; } 
            public string ReturnUrl { get; private set; } 
 
            public override void ExecuteResult(ControllerContext context) 
            { 
                OAuthWebSecurity.RequestAuthentication(Provider, ReturnUrl); 
            } 
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for 
            // a full list of status codes. 
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return
                        "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return
                        "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return
                        "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
                default:
                    return
                        "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}