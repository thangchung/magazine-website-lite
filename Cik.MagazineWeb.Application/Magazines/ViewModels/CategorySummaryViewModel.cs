using System.Collections.Generic;
using Cik.MagazineWeb.Application.Magazines.Dtos;

namespace Cik.MagazineWeb.Application.Magazines.ViewModels
{
    public class CategorySummaryViewModel
    {
        public IEnumerable<CategorySummaryDto> Categories { get; set; }

        public int TotalPage { get; set; }
    }
}