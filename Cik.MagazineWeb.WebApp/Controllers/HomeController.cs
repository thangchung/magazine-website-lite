using System.Web.Mvc;
using Cik.MagazineWeb.Application;
using Cik.MagazineWeb.Utilities;

namespace Cik.MagazineWeb.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMagazineApplication _magazineApplication;

        public HomeController(IMagazineApplication magazineApplication)
        {
            Guard.ArgumentNotNull(magazineApplication, "MagazineApplication");

            _magazineApplication = magazineApplication;
        }

        public ActionResult Index()
        {
            var viewModel = _magazineApplication.BuildHomePageViewModel();
            return View(viewModel);
        }

        public ActionResult Category(int id)
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}
