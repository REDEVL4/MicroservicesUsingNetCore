using Catalogue.Api.Models;
using MongoDB.Driver;
namespace Catalogue.Api.Services
{
    public class CatalogueServices : ICatalogueServices
    {
        private readonly IMongoClient _mongoClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<CatalogueServices> _logger;
        public CatalogueServices(IConfiguration configuration, ILogger<CatalogueServices> logger)
        {
            _logger = logger;
            _logger.LogInformation($"Attempting to Connect to MONGO DB using {configuration.GetValue<string>("Secrets:MongoCS")}");
            _mongoClient = new MongoClient(configuration.GetValue<string>("Secrets:MongoCS"));
            _logger.LogInformation("Connection Established!!!");
            _configuration = configuration;
        }
        public async Task<IEnumerable<Models.Catalogue>> GetCatalogueAsync()
        {
            var db = _mongoClient.GetDatabase(_configuration.GetValue<string>("Secrets:Database"));
            var col = db.GetCollection<Models.Catalogue>(_configuration.GetValue<string>("Secrets:Collection"));
            _logger.LogInformation($"Sent the request for Getting List Of Items in Catalouge");
            var result = (await col.FindAsync(Builders<Models.Catalogue>.Filter.Empty)).ToList();
            _logger.LogInformation($"Response  List Of Items in Catalouge: {result.ToString()}");
            return result;
        }

        public async Task<Models.Catalogue> GetCatalogueByIdAsync(string id)
        {
            var db = _mongoClient.GetDatabase(_configuration.GetValue<string>("Secrets:Database"));
            var col = db.GetCollection<Models.Catalogue>(_configuration.GetValue<string>("Secrets:Collection"));
            var result = await col.FindAsync(c => c.Id == id);
            return result.SingleOrDefault();
        }

        public async Task<Models.Catalogue> AddItemToCatalogue(Models.Catalogue catalogue)
        {
            var db = _mongoClient.GetDatabase(_configuration.GetValue<string>("Secrets:Database"));
            var col = db.GetCollection<Models.Catalogue>(_configuration.GetValue<string>("Secrets:Collection"));
            await col.InsertOneAsync(catalogue);
            return catalogue;
        }

        public async Task<Models.Catalogue> UpdateItemInCatalogue(string id, Models.Catalogue catalogue)
        {
            var db = _mongoClient.GetDatabase(_configuration.GetValue<string>("Secrets:Database"));
            var col = db.GetCollection<Models.Catalogue>(_configuration.GetValue<string>("Secrets:Collection"));
            var result = await col.FindOneAndReplaceAsync(c => c.Id == id, catalogue);
            return catalogue;
        }

        public async Task RemoveItemFromCatalogue(string id)
        {
            var db = _mongoClient.GetDatabase(_configuration.GetValue<string>("Secrets:Database"));
            var col = db.GetCollection<Models.Catalogue>(_configuration.GetValue<string>("Secrets:Collection"));
            await col.FindOneAndDeleteAsync(c => c.Id == id);
        }
    }
}
