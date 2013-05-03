using System.Collections.Generic;
using Cik.MagazineWeb.Application.Magazines.Dtos;

namespace Cik.MagazineWeb.Application.Magazines.Services
{
    public interface IItemSummaryService
    {
        IEnumerable<ItemSummaryDto> GetItemSummaries();
        IEnumerable<ItemSummaryDto> GetHottestItems(int numOfItemOnHomePage);
        IEnumerable<ItemSummaryDto> GetLatestItems(int numOfItemOnHomePage);
        IEnumerable<ItemSummaryDto> GetItemsByCategoryId(int categoryId);
        ItemDetailsDto GetItemDetails(int itemId);
    }
}