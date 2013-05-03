using System.Collections.Generic;
using Cik.MagazineWeb.Application.Magazines.Dtos;
using Cik.MagazineWeb.Application.Magazines.ViewModels;

namespace Cik.MagazineWeb.Application.Magazines
{
    public interface IMagazineApplication
    {
        #region admin page

        IEnumerable<CategorySummaryDto> GetCategorySummaries();

        CategorySummaryDto GetCategoryById(int id);

        CategorySummaryViewModel GetCategoryPaging(int pageSize, int page);

        void SaveCategory(CategorySummaryDto dto);

        void DeleteCategory (int id);

        ItemSummaryViewModel GetItemSummaryPaging(int pageSize, int page);

        #endregion

        #region front-end page

        HomePageViewModel BuildHomePageViewModel(int numOfItemOnHomePage);

        CategoryPageViewModel BuildCategoryPageViewModel(int id);

        ItemDetailsViewModel BuildItemDetailsViewModel(int categoryId, int itemId);

        CategoryMenuViewModel GetCategoryMenu(int id);

        #endregion
    }
}