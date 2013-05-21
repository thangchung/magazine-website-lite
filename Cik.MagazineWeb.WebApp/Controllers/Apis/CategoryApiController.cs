using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Cik.MagazineWeb.Application;
using Cik.MagazineWeb.Application.Magazines.ViewModels;
using Cik.MagazineWeb.Domain.UserMgmt;
using Cik.MagazineWeb.Utilities;
using Cik.MagazineWeb.WebApp.App_Start.Filters;

namespace Cik.MagazineWeb.WebApp.Controllers.Apis
{
    [SimpleAuthorize(Role.Administrator)]
    public class CategoryApiController : ApiControllerBase
    {
        
        private readonly IMagazineApplication _magazineApp;

        public CategoryApiController(IMagazineApplication magazineApp)
        {
            Guard.ArgumentNotNull(magazineApp, "MagazineApplication");

            _magazineApp = magazineApp;
        }
        
        [HttpGet]
        public async Task<CategorySummaryViewModel> CategoriesPaging(int page)
        {
            var viewModel = await Task.Run(() => _magazineApp.GetCategoryPaging(this.PageSize, page));
            if (viewModel != null)
            {
                return viewModel;
            }

            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
        }
    }
}
