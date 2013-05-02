using System.Linq;
using Cik.MagazineWeb.Application.ViewModels;

namespace Cik.MagazineWeb.Application
{
    public partial class MagazineApplication
    {
        public HomePageViewModel BuildHomePageViewModel(int numOfItemOnHomePage)
        {
            var homePageViewModel = new HomePageViewModel();
            homePageViewModel.TopMenu = GetCategoryMenu();
            homePageViewModel.HottestItems = _queryForHottestItems.GetHottestItems(numOfItemOnHomePage).ToList();
            homePageViewModel.LatestItems = _queryForLatestItems.GetLatestItems(numOfItemOnHomePage).ToList();

            return homePageViewModel;
        }

        public CategoryPageViewModel BuildCategoryPageViewModel()
        {
            var categoryPageViewModel = new CategoryPageViewModel();
            categoryPageViewModel.TopMenu = GetCategoryMenu();

            return categoryPageViewModel;
        }

        public CategoryMenuViewModel GetCategoryMenu()
        {
            var viewModel = new CategoryMenuViewModel();
            var categories = GetAllCategorySummaries();
            viewModel.Categories = categories;

            return viewModel;
        }
    }
}