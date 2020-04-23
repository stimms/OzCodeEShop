using Microsoft.eShopWeb.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using System.Linq;

namespace Microsoft.eShopWeb.Web.Controllers.Api
{
    public class CatalogController : BaseApiController
    {
        private readonly ICatalogViewModelService _catalogViewModelService;
        readonly IAsyncRepository<CatalogType> _typeRepository;

        public CatalogController(ICatalogViewModelService catalogViewModelService, IAsyncRepository<CatalogType> typeRepository)
        {
            _typeRepository = typeRepository;
            _catalogViewModelService = catalogViewModelService;
            
        }

        [HttpGet]
        public async Task<IActionResult> List(int? brandFilterApplied, int? typesFilterApplied, int? page)
        {
            int itemsPage = typesFilterApplied.HasValue ? (await _typeRepository.ListAllAsync()).Single(x => x.Id == typesFilterApplied).ItemsToDisplay : 10;
            var catalogModel = await _catalogViewModelService.GetCatalogItems(page ?? 0, itemsPage, brandFilterApplied, typesFilterApplied);
            return Ok(catalogModel);
        }
    }
}
