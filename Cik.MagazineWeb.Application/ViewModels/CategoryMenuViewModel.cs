using System.Collections.Generic;
using Cik.MagazineWeb.Application.Dtos;

namespace Cik.MagazineWeb.Application.ViewModels
{
    public class CategoryMenuViewModel
    {
        public IEnumerable<CategorySummaryDto> Categories { get; set; }
    }
}