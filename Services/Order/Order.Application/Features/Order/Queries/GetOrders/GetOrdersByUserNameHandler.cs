using AutoMapper;
using MediatR;
using Order.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Features.Order.Queries.GetOrders
{
    public class GetOrdersByUserNameHandler : IRequestHandler<GetOrdersByUserName,IList<OrderDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;

        public GetOrdersByUserNameHandler(IMapper mapper, IOrderRepository orderRepository)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
        }

        public async Task<IList<OrderDTO>> Handle(GetOrdersByUserName request, CancellationToken cancellationToken)
        {
            var Orders=await _orderRepository.GetOrdersByUserName(request.UserName);
            var OrdersResponse=_mapper.Map<List<OrderDTO>>(Orders);
            return OrdersResponse;
        }
    }
}
