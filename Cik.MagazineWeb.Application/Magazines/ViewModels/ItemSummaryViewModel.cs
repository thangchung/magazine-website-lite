using System.Collections.Generic;
using Cik.MagazineWeb.Application.Magazines.Dtos;

namespace Cik.MagazineWeb.Application.Magazines.ViewModels
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