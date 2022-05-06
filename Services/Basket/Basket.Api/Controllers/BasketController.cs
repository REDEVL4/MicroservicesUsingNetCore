using Basket.Api.Repository;
using Basket.Api.Services;
using Microsoft.AspNetCore.Mvc;
namespace Basket.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly DServices _services;
        private IBasketRepository _basketRepository;
        public BasketController(IBasketRepository basketRepository,DServices services)
        {
            _services = services;
            _basketRepository= basketRepository;
        }
        [HttpGet("{username}")]
        public async Task<ActionResult<Models.Basket>> GetBasket(string username)
        {
            var result=await _basketRepository.GetBasketAsync(username);
            return result;
        }
        [HttpPost]
        public async Task<ActionResult<Models.Basket>> UpdateBasket([FromBody] Models.Basket? basket)
        {
            try
            {

                if (basket == null)
                    return BadRequest();
                foreach (var item in basket.Products)
                {
                    var discount = await _services.GetDiscount(item.ProductName);
                    item.Cost -= (int)discount.DiscountedPrice;
                }
                var result = await _basketRepository.UpdateBasket(basket);
                return result;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{username}")]
        public async Task<ActionResult> RemoveBasket(string username)
        {
            await _basketRepository.DeleteBasket(username);
            return Ok(new {Message=$"Sucessfully removed" });
        }
    }
}
