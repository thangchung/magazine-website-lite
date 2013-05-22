using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Cik.MagazineWeb.Application;
using Cik.MagazineWeb.Application.Magazines.Dtos;
using Cik.MagazineWeb.Application.Magazines.ViewModels;
using Cik.MagazineWeb.Domain.UserMgmt;
using Cik.MagazineWeb.Utilities;
using Cik.MagazineWeb.WebApp.App_Start.Filters;

namespace Cik.MagazineWeb.WebApp.Controllers.Apis
{
    [SimpleAuthorize(Role.Administrator)]
    public class ItemApiController : ApiControllerBase
    {
        private readonly IMagazineApplication _magazineApp;

        public ItemApiController(IMagazineApplication magazineApp)
        {
            Guard.ArgumentNotNull(magazineApp, "MagazineApplication");

            _magazineApp = magazineApp;
        }
        
        #region Public Item APIs

        [HttpGet]
        public async Task<ItemSummaryViewModel> ItemPaging(int page)
        {
            var viewModel = await Task.Run(() => _magazineApp.GetItemSummaryPaging(PageSize, page));
            if (viewModel != null)
            {
                return viewModel;
            }

            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
        }

        [HttpGet]
        public async Task<ItemDetailsDto> GetItemById(int id)
        {
            var dto = await Task.Run(() => _magazineApp.GetItemById(id));
            if (dto != null)
            {
                return dto;
            }

            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
        }

        [HttpPost]
        public void SaveItem(ItemDetailsDto dto)
        {
            _magazineApp.SaveItem(dto);
        }

        [HttpGet]
        public void DeleteItem(int id)
        {
            _magazineApp.DeleteItem(id);
        }

        #endregion
    }
}