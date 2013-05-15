using System.Web.Mvc;
using System.Web.Routing;
using Cik.MagazineWeb.Application;
using Cik.MagazineWeb.Domain.UserMgmt;
using Cik.MagazineWeb.Utilities;

namespace Cik.MagazineWeb.WebApp.App_Start.Filters
{
    public class SimpleAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly IUserApplication _userApplication;
        private readonly Role _role;

        public SimpleAuthorizeAttribute(Role role)
            : this(role, DependencyResolver.Current.GetService<IUserApplication>())
        {
        }

        public SimpleAuthorizeAttribute(Role role, IUserApplication userApplication)
        {
            Guard.ArgumentNotNull(userApplication, "UserApplication");

            _userApplication = userApplication;
            _role = role;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            var userName = filterContext.HttpContext.User.Identity.Name;
            var currentUser = _userApplication.GetUserByUserName(userName);
            if (currentUser != null)
            {
                var isValidRole = currentUser.Role == _role;

                if (!isValidRole || filterContext.Result is HttpUnauthorizedResult)
                {
                    RedirectToLoginRoute(filterContext);
                }
            }
            else
            {
                RedirectToLoginRoute(filterContext);
            }
        }

        private void RedirectToLoginRoute(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary
                    {
                        {"Default", filterContext.RouteData.Values["Default"]},
                        {"controller", "Account"},
                        {"action", "Login"},
                        {"ReturnUrl", filterContext.HttpContext.Request.RawUrl}
                    });

            return;
        }
    }
}