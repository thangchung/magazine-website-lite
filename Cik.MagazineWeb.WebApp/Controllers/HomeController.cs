using System.Web.Mvc;
using Cik.MagazineWeb.Application;
using Cik.MagazineWeb.Utilities;
using ControllerBase = Cik.MagazineWeb.Utilities.ControllerBase;

namespace Cik.MagazineWeb.WebApp.Controllers
{
    public class HomeController : ControllerBase
    {
        private readonly IMagazineApplication _magazineApplication;

        public HomeController(IMagazineApplication magazineApplication)
        {
            Guard.ArgumentNotNull(magazineApplication, "MagazineApplication");

            _magazineApplication = magazineApplication;
        }

        public ActionResult Index()
        {
            var viewModel = _magazineApplication.BuildHomePageViewModel(NumOfItemOnHomePage);
            return View(viewModel);
        }

        public ActionResult Category(int id)
        {
            var viewModel = _magazineApplication.BuildCategoryPageViewModel();
            return View(viewModel);
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
