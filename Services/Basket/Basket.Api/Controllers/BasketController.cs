using AutoMapper;
using Basket.Api.Publisher;
using Basket.Api.Repository;
using Basket.Api.Services;
using MassTransit;
using MessagingBrokerDefaults.Models;
using Microsoft.AspNetCore.Mvc;
namespace Basket.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private IMapper _mapper;
        private IPublishEndpoint _publish;
        private readonly DServices _services;
        private IBasketRepository _basketRepository;
        public BasketController(IBasketRepository basketRepository,IMapper mapper,DServices services,IPublishEndpoint publish)
        {
            _mapper = mapper;
            _services = services;
            _basketRepository= basketRepository;
            _publish = publish;
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

        [HttpPost("{actionName}")]
        public async Task<ActionResult> CheckOut([FromBody] CheckOutModelForOrders Order)
        {
            if (Order == null)
                 return BadRequest();
            var basket=await _basketRepository.GetBasketAsync(Order.UserName);
            var orderToPublish=_mapper.Map<CheckOutOrder>(Order);
            orderToPublish.TotalPrice = basket.TotalPrice;
            try
            {
                await _publish.Publish(orderToPublish);
                await _basketRepository.DeleteBasket(Order.UserName);
                return Accepted();
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
