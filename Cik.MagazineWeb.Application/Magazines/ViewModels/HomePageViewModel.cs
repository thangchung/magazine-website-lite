using System.Collections.Generic;
using Cik.MagazineWeb.Application.Magazines.Dtos;

namespace Cik.MagazineWeb.Application.Magazines.ViewModels
{
    public class HomePageViewModel : FrontPageViewModelBase
    {
        public HomePageViewModel()
        {
            HottestItems = new List<ItemSummaryDto>();
            LatestItems = new List<ItemSummaryDto>();
        }

        public List<ItemSummaryDto> HottestItems { get; set; }

        public List<ItemSummaryDto> LatestItems { get; set; }
    }
}