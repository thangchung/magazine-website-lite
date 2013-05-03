using Cik.MagazineWeb.Application.Magazines.Dtos;

namespace Cik.MagazineWeb.Application.Magazines.ViewModels
{
    public class ItemDetailsViewModel : FrontPageViewModelBase
    {
        public ItemDetailsViewModel()
        {
            ItemDetails = new ItemDetailsDto();
        }
        
        public ItemDetailsDto ItemDetails { get; set; }
    }
}