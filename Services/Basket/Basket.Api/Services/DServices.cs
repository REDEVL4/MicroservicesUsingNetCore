

using Discount.Grpc.Protos;

namespace Basket.Api.Services
{
    public class DServices 
    {
        private readonly ILogger<DServices> _logger;
        private readonly DiscountServices.DiscountServicesClient _Client;
        public DServices(DiscountServices.DiscountServicesClient Client,ILogger<DServices> logger)
        {
            _logger = logger;
            _Client = Client;
        }
        public async Task<DiscountResponse> GetDiscount(string productName)
        {
            try
            {
                var data =await _Client.GetDiscountedProductAsync(new DiscountRequest() { Productname = productName });
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new DiscountResponse();
            }
            // var data = await _Client.GetDiscountedProductAsync(new DiscountRequest() { ProductName = productName });
        }
    }
}
