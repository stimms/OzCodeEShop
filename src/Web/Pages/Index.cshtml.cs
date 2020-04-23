using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.Web.Services;
using Microsoft.eShopWeb.Web.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ICatalogViewModelService _catalogViewModelService;
        readonly IAsyncRepository<CatalogType> typeRepository;

        public IndexModel(ICatalogViewModelService catalogViewModelService, IAsyncRepository<CatalogType> typeRepository)
        {
            this.typeRepository = typeRepository;
            _catalogViewModelService = catalogViewModelService;
        }

        public CatalogIndexViewModel CatalogModel { get; set; } = new CatalogIndexViewModel();

        public async Task OnGet(CatalogIndexViewModel catalogModel, int? pageId)
        {
            int itemsPage = catalogModel.TypesFilterApplied.HasValue ? (await typeRepository.ListAllAsync()).Single(x => x.Id == catalogModel.TypesFilterApplied).ItemsToDisplay : 10;
            CatalogModel = await _catalogViewModelService.GetCatalogItems(pageId ?? 0, itemsPage, catalogModel.BrandFilterApplied, catalogModel.TypesFilterApplied);
        }
    }
}
