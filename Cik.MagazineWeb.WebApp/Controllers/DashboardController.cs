using System.Web.Mvc;

namespace Cik.MagazineWeb.WebApp.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
