using Order.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Contracts.Persistence
{
    public interface IOrderRepository : IAsyncRepository<Domain.Models.Order>
    {
        Task<IEnumerable<Domain.Models.Order>> GetOrdersByUserName(string userName);
    }
}
