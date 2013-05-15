using System.Web.Mvc;
using Cik.MagazineWeb.Domain.UserMgmt;
using Cik.MagazineWeb.WebApp.App_Start.Filters;

namespace Cik.MagazineWeb.WebApp.Controllers
{
    [SimpleAuthorize(Role.Administrator)]
    public class DashboardController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
