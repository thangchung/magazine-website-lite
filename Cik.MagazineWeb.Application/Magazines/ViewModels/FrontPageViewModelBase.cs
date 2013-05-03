namespace Cik.MagazineWeb.Application.Magazines.ViewModels
{
    public abstract class FrontPageViewModelBase
    {
        protected FrontPageViewModelBase()
        {
            TopMenu = new CategoryMenuViewModel();
        }

        public CategoryMenuViewModel TopMenu { get; set; }  
    }
}