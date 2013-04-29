using System.Collections.Generic;

namespace Cik.MagazineWeb.Domain.MagazineMgmt.Queries
{
    public interface IQueryForItemSummaries
    {
        IEnumerable<ItemSummaryDto> GetItemSummaries();
    }
}