using Discount.Api.DataAccess;
using Discount.Api.Models;

namespace Discount.Api.Repository
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IGenericRepository _Client;
        private ILogger<DiscountRepository> _logger;
        public DiscountRepository(IGenericRepository genericRepository, ILogger<DiscountRepository> logger)
        {
            _Client = genericRepository;
            _logger = logger;
        }
        public async Task<IEnumerable<DiscountedProduct>> GetDiscountedProductsAsync()
        {
            var squery = "select * from DiscountedProducts";
            var result = await _Client.LoadData<DiscountedProduct, dynamic>(query: squery);
            return result;
        }
        public async Task<DiscountedProduct?> GetDiscountedProductAsync(string productName)
        {
            var squery = "select * from DiscountedProducts where ProductName=@ProductName";
            var result = (await _Client.LoadData<DiscountedProduct, dynamic>(query: squery, parameters: new { ProductName = productName })).SingleOrDefault();
            return result;
        }
        public async Task AddDiscountedProductAsync(DiscountedProduct product)
        {
            string squery = "insert into DiscountedProducts(ProductName,Description,DiscountedPrice) values(@ProductName,@Description,@DiscountedPrice)";
            await _Client.InsertData<DiscountedProduct, dynamic>(query: squery, parameters: new { ProductName = product.ProductName, Description=product.Description, DiscountedPrice = product.DiscountedPrice });
        }
        public async Task UpdateDiscountedProductAsync(DiscountedProduct product)
        {
            string squery = "update DiscountedProducts set Description=@Description,DiscountedPrice=@DiscountedPrice where ProductName=@ProductName";
            await _Client.InsertData<DiscountedProduct, dynamic>(query: squery, parameters: new { ProductName = product.ProductName, Description=product.Description, DiscountedPrice = product.DiscountedPrice });
        }
        public async Task DeleteDiscountedProductAsync(string productName)
        {
            string squery = "delete from DiscountedProducts where ProductName=@ProductName";
            await _Client.InsertData<DiscountedProduct, dynamic>(parameters: new { ProductName = productName }, query: squery);
        }


    }
}
