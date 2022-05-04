using Newtonsoft.Json;

namespace Basket.Api.Models
{
    public class Product
    {
        [JsonProperty("productName")]
        public string? ProductName { get; set; }
        [JsonProperty("cost")]
        public int Cost { get; set; }
        [JsonProperty("imageUrl")]
        public string? ImageUrl { get; set; }
        [JsonProperty("description")]
        public string? Description { get; set; }
    }
}
