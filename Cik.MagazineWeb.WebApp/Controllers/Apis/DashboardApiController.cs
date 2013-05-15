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
    // [SimpleAuthorize(Role.Administrator)]
    public class DashboardApiController : ApiControllerBase
    {
        private readonly IMagazineApplication _magazineApp;

        public DashboardApiController(IMagazineApplication magazineApp)
        {
            Guard.ArgumentNotNull(magazineApp, "MagazineApplication");

            _magazineApp = magazineApp;
        }

        [HttpGet]
        public CategorySummaryViewModel CategoryPaging(int page)
        {
            var viewModel = _magazineApp.GetCategoryPaging(PageSize, page);

            if (viewModel != null)
            {
                return viewModel;
            }

            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
        }

        [HttpGet]
        public async Task<CategorySummaryDto> GetCategoryById(int id)
        {
            var dto = await Task.Run(() => _magazineApp.GetCategoryById(id));
            if (dto != null)
            {
                return dto;
            }

            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
        }

        [HttpPost]
        public void SaveCategory(CategorySummaryDto dto)
        {
            _magazineApp.SaveCategory(dto);
        }

        [HttpGet]
        public void DeleteCategory(int id)
        {
            _magazineApp.DeleteCategory(id);
        }
    }
}