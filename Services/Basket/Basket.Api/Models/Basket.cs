
using Newtonsoft.Json;

namespace Basket.Api.Models
{
    public class Basket
    {
        public Basket()
        {
/*            Id = Guid.NewGuid().ToString();*/
            Products = new List<Product>();
        }
/*        public string Id { get; set; }*/
        [JsonProperty("userName")]
        public string UserName { get; set; }
        [JsonProperty("products")]
        public IList<Product> Products { get; set; }
        [JsonProperty("totalPrice")]
        public double TotalPrice
        {
           get
            {
                double count = 0;
                foreach(var product in Products)
                {
                    count+=product.Cost;
                }
                return count;
            }
        }
    }
}
