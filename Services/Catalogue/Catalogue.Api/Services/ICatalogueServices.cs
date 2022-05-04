
namespace Catalogue.Api.Services
{
    public interface ICatalogueServices
    {
        Task<Models.Catalogue> AddItemToCatalogue(Models.Catalogue catalogue);
        Task<IEnumerable<Models.Catalogue>> GetCatalogueAsync();
        Task<Models.Catalogue> GetCatalogueByIdAsync(string id);
        Task RemoveItemFromCatalogue(string id);
        Task<Models.Catalogue> UpdateItemInCatalogue(string id, Models.Catalogue catalogue);
    }
}