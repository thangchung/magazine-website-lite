﻿using System.Collections.Generic;
using Cik.MagazineWeb.Application.Dtos;

namespace Cik.MagazineWeb.Application.ViewModels
{
    public class CategorySummaryViewModel
    {
        public IEnumerable<CategorySummaryDto> Categories { get; set; }

        public int TotalPage { get; set; }
    }
}