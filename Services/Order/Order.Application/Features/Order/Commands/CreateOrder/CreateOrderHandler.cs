using MediatR;
using Order.Application.Contracts.Persistence;
using AutoMapper;
using Order.Application.Features.Infastructure;
using Order.Application.Models;
using Microsoft.Extensions.Logging;

namespace Order.Application.Features.Order.Commands.CreateOrder
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly IEmailHandler _emailHandler;
        private readonly ILogger<CreateOrderHandler> _logger;

        public CreateOrderHandler(IMapper mapper, IOrderRepository orderRepository, IEmailHandler emailHandler, ILogger<CreateOrderHandler> logger)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _emailHandler = emailHandler;
            _logger = logger;
        }

        public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var Order=_mapper.Map<Domain.Models.Order>(request);
            var OrderResult=await _orderRepository.AddAsync(Order);
            await SendMail(OrderResult);
            return OrderResult.Id;
        }
        public  async Task SendMail(Domain.Models.Order order)
        {
            try
            {
                var email = new Email() { To=order.EmailAddress,Subject=$"Hello {string.Format("{0} {1}",order.FirstName,order.LastName)} Order has been Created Sucessfully",Body="Here is the Order Body"};
                await _emailHandler.SendEmail(email);
                _logger.LogInformation($"Mail For Order {order.Id} has been Sent.", order); 
            }
            catch(Exception ex)
            {
                _logger.LogError($"Failed to send the mail for {order.Id}");
            }
        }
    }
}
