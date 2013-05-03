using System.Collections.Generic;
using System.Linq;
using Cik.MagazineWeb.Application.Magazines.Dtos;
using Cik.MagazineWeb.Application.Magazines.ViewModels;

namespace Cik.MagazineWeb.Application.Magazines
{
    public partial class MagazineApplication
    {
        public HomePageViewModel BuildHomePageViewModel(int numOfItemOnHomePage)
        {
            var homePageViewModel = new HomePageViewModel();
            homePageViewModel.TopMenu = GetCategoryMenu(0);
            homePageViewModel.HottestItems = _itemSummaryService.GetHottestItems(numOfItemOnHomePage).ToList();
            homePageViewModel.LatestItems = _itemSummaryService.GetLatestItems(numOfItemOnHomePage).ToList();

            return homePageViewModel;
        }

        public CategoryPageViewModel BuildCategoryPageViewModel(int id)
        {
            var categoryPageViewModel = new CategoryPageViewModel();
            categoryPageViewModel.CategoryId = id;
            categoryPageViewModel.TopMenu = GetCategoryMenu(id);
            categoryPageViewModel.ItemsConverting(_itemSummaryService.GetItemsByCategoryId(id).ToList());

            return categoryPageViewModel;
        }

        public ItemDetailsViewModel BuildItemDetailsViewModel(int categoryId, int itemId)
        {
            var itemDetailsViewModel = new ItemDetailsViewModel();
            itemDetailsViewModel.TopMenu = GetCategoryMenu(categoryId);
            itemDetailsViewModel.ItemDetails = _itemSummaryService.GetItemDetails(itemId);

            return itemDetailsViewModel;
        }

        public CategoryMenuViewModel GetCategoryMenu(int id)
        {
            var viewModel = new CategoryMenuViewModel();
            var categories = GetAllCategorySummaries();
            var realCategories = new List<CategorySummaryDto>();
            realCategories.Add(new CategorySummaryDto
                {
                    Id = 0,
                    Name = "Home",
                    IsCurrentPage = id == 0
                });

            foreach (var categorySummaryDto in categories)
            {
                if (categorySummaryDto.Id == id)
                    categorySummaryDto.IsCurrentPage = true;
                realCategories.Add(categorySummaryDto);
            }
            
            viewModel.Categories = realCategories;

            return viewModel;
        }
    }
}