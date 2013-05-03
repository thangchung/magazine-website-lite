using System.Collections.Generic;
using Cik.MagazineWeb.Application.Magazines.Dtos;

namespace Cik.MagazineWeb.Application.Magazines.ViewModels
{
    public class CategoryPageViewModel : FrontPageViewModelBase
    {
        public CategoryPageViewModel()
        {
            RowItems = new Dictionary<int, List<ItemSummaryDto>>();
        }

        public int CategoryId { get; set; }

        public Dictionary<int, List<ItemSummaryDto>> RowItems { get; private set; }

        public void ItemsConverting(List<ItemSummaryDto> sources)
        {
            var counter = 1;
            var row = new List<ItemSummaryDto>();

            if (sources.Count <= 4)
            {
                RowItems.Add(counter, sources);
            }
            else
            {
                foreach (var item in sources)
                {
                    row.Add(item);
                    if (counter%4 != 0)
                    {
                        RowItems.Add(counter, row);
                        row = new List<ItemSummaryDto>();
                    }
                    counter++;
                }
            }
        }
    }
}