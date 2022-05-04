
namespace Basket.Api.Repository
{
    public interface IBasketRepository
    {
        Task DeleteBasket(string username);
        Task<Models.Basket> GetBasketAsync(string username);
        Task<Models.Basket> UpdateBasket(Models.Basket basket);
    }
}