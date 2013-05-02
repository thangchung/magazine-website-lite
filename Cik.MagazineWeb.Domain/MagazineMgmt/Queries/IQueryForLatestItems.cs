using System.Collections.Generic;

namespace Cik.MagazineWeb.Domain.MagazineMgmt.Queries
{
    public interface IQueryForLatestItems
    {
        IEnumerable<ItemSummaryDto> GetLatestItems(int numOfItemOnHomePage);
    }
}