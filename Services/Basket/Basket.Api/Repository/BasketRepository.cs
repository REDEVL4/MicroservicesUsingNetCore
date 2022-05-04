using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text.Json;
namespace Basket.Api.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _Cache;
        public BasketRepository(IDistributedCache cache)
        {
            _Cache = cache;
        }
        public async Task<Models.Basket?> GetBasketAsync(string username)
        {
            var rawBytes = _Cache.GetString(username);
            if (rawBytes == null)
            {
                return new Models.Basket();
            }
            else
            {
                var basket = JsonConvert.DeserializeObject<Models.Basket>(rawBytes);
                return basket;
            }
        }
        /*public async Task AddToBasket(Models.Basket basket)
        {
            var options = new DistributedCacheEntryOptions()
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddDays(30),
                SlidingExpiration = TimeSpan.FromDays(50)
            };
            var rawData =JsonSerializer.Serialize(basket);
            await _Cache.SetAsync(basket.UserName,System.Text.Encoding.UTF8.GetBytes(basket.ToString()),options);
        }*/
        public async Task<Models.Basket> UpdateBasket(Models.Basket basket)
        {
            var options = new DistributedCacheEntryOptions()
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddDays(30),
                SlidingExpiration = TimeSpan.FromDays(50)
            };
            var rawData = JsonConvert.SerializeObject(basket);
            await _Cache.SetStringAsync(basket.UserName,rawData,options);
            return basket;
        }
        public async Task DeleteBasket(string username)
        {
            await _Cache.RemoveAsync(username);
        }

    }
}
