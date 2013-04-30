using System.Collections.Generic;
using Cik.MagazineWeb.Application.Dtos;
using Cik.MagazineWeb.Application.ViewModels;

namespace Cik.MagazineWeb.Application
{
    public interface IMagazineApplication
    {
        #region admin

        IEnumerable<CategorySummaryDto> GetCategorySummaries();

        CategorySummaryDto GetCategoryById(int id);

        CategorySummaryViewModel GetCategoryPaging(int pageSize, int page);

        void SaveCategory(CategorySummaryDto dto);

        void DeleteCategory (int id);

        ItemSummaryViewModel GetItemSummaryPaging(int pageSize, int page);

        #endregion

        #region front-end

        HomePageViewModel BuildHomePageViewModel();

        CategoryMenuViewModel GetCategoryMenu();

        #endregion
    }
}