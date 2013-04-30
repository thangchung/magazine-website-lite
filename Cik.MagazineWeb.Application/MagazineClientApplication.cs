using Cik.MagazineWeb.Application.ViewModels;

namespace Cik.MagazineWeb.Application
{
    public partial class MagazineApplication
    {
        public HomePageViewModel BuildHomePageViewModel()
        {
            var homePageViewModel = new HomePageViewModel();
            homePageViewModel.TopMenu = GetCategoryMenu();

            return homePageViewModel;
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