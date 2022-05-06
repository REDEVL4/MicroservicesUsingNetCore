using Discount.Api.Models;
using Discount.Api.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Discount.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountRepository _Client;

        public DiscountController(IDiscountRepository client)
        {
            _Client = client;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DiscountedProduct>>> GetDiscountedProducts()
        {
            return Ok(await _Client.GetDiscountedProductsAsync());
        }
        [HttpGet("{productName}")]
        public async Task<ActionResult<DiscountedProduct>> GetDiscountedProducts(string productName)
        {
            return Ok(await _Client.GetDiscountedProductAsync(productName));
        }
        [HttpPost]
        public async Task<ActionResult> AddProductsToDiscounts([FromBody] DiscountedProduct product)
        {
            await _Client.AddDiscountedProductAsync(product);
            return Ok(new {Message="Added Sucessfully"});
        }
        [HttpPatch]
        public async Task<ActionResult> UpdateDiscountedProducts([FromBody] DiscountedProduct product)
        {
            try
            {
                await _Client.UpdateDiscountedProductAsync(product);
                return Ok(new { Message = "Sucessfully updated" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{productName}")]
        public async Task<ActionResult> DeleteDiscountedProduct(string productName)
        {
            try
            {
                await _Client.DeleteDiscountedProductAsync(productName);
                return Ok(new { Message = "Sucessfully deleted" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
