using Basket.Api.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace Basket.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private IBasketRepository _basketRepository;
        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository= basketRepository;
        }
        [HttpGet("{username}")]
        public async Task<ActionResult<Models.Basket>> GetBasket(string username)
        {
            var result=await _basketRepository.GetBasketAsync(username);
            return result;
        }
        [HttpPost]
        public async Task<ActionResult<Models.Basket>> UpdateBasket([FromBody] Models.Basket basket)
        {
            var result = await _basketRepository.UpdateBasket(basket);
            return result;
        }
        [HttpDelete("{username}")]
        public async Task<ActionResult> RemoveBasket(string username)
        {
            await _basketRepository.DeleteBasket(username);
            return Ok(new {Message=$"Sucessfully removed" });
        }
    }
}
