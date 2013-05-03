using System.Net;
using System.Net.Http;
using System.Web.Http;
using Cik.MagazineWeb.Application.Magazines;
using Cik.MagazineWeb.Application.Magazines.ViewModels;
using Cik.MagazineWeb.Utilities;

namespace Cik.MagazineWeb.WebApp.Controllers.Apis
{
    public class ItemApiController : ApiControllerBase
    {
        private readonly IMagazineApplication _magazineApp;

        public ItemApiController(IMagazineApplication magazineApp)
        {
            Guard.ArgumentNotNull(magazineApp, "MagazineApplication");

            _magazineApp = magazineApp;
        }
        
        [HttpGet]
        public ItemSummaryViewModel ItemPaging(int page)
        {
            var viewModel = _magazineApp.GetItemSummaryPaging(PageSize, page);
            if (viewModel != null)
            {
                return viewModel;
            }

            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
        }
    }
}