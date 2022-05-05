using Discount.Api.Models;

namespace Discount.Api.Repository
{
    public interface IDiscountRepository
    {
        Task AddDiscountedProductAsync(DiscountedProduct product);
        Task DeleteDiscountedProductAsync(string productName);
        Task<DiscountedProduct?> GetDiscountedProductAsync(string productName);
        Task<IEnumerable<DiscountedProduct>> GetDiscountedProductsAsync();
        Task UpdateDiscountedProductAsync(DiscountedProduct product);
    }
}