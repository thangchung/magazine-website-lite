using System.Collections.Generic;
using Cik.MagazineWeb.Application.Magazines.Dtos;

namespace Cik.MagazineWeb.Application.Magazines.ViewModels
{
    public class CategoryMenuViewModel
    {
        public CategoryMenuViewModel()
        {
            Categories = new List<CategorySummaryDto>();
        }

        public IEnumerable<CategorySummaryDto> Categories { get; set; }
    }
}