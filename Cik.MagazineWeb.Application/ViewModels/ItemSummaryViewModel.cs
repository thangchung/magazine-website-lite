using System.Collections.Generic;
using Cik.MagazineWeb.Domain.MagazineMgmt.Queries;

namespace Cik.MagazineWeb.Application.ViewModels
{
    public class ItemSummaryViewModel
    {
        public ItemSummaryViewModel()
        {
            Items = new List<ItemSummaryDto>();
        }

        public int TotalPage { get; set; }
        public IEnumerable<ItemSummaryDto> Items { get; set; }
    }
}