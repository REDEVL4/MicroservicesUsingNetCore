using MediatR;
using Order.Application.Features.Order.Queries.GetOrders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Features.Order.Queries
{
    public class GetOrdersByUserName:IRequest<IList<OrderDTO>>
    {
        public string UserName { get; set; }
        public GetOrdersByUserName(string userName)
        {
            UserName = userName;
        }
    }
}
