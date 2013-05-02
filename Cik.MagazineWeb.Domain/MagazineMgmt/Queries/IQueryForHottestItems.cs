using System.Collections.Generic;

namespace Cik.MagazineWeb.Domain.MagazineMgmt.Queries
{
    public interface IQueryForHottestItems
    {
        IEnumerable<ItemSummaryDto> GetHottestItems(int numOfItemOnHomePage);
    }
}