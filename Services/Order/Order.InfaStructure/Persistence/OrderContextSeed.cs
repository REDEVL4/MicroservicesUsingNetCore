using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Infastructure.Persistence
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
        {
            if (!orderContext.Orders.Any())
            {
                orderContext.Orders.AddRange(GetPreconfiguredOrders());
                await orderContext.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(OrderContext).Name);
            }
        }

        private static IEnumerable<Order.Domain.Models.Order> GetPreconfiguredOrders()
        {
            return new List<Domain.Models.Order>
            {
                new Domain.Models.Order() {UserName = "ngr", FirstName = "GReddy", LastName = "N", EmailAddress = "ezozkme@gmail.com", State="telangana",AddressLine = "3/140",ZipCode="1003423",Country = "India", TotalPrice = 350 }
            };
        }
    }
}
