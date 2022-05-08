using AutoMapper;
using MassTransit;
using MediatR;
using MessagingBrokerDefaults.Models;
using Order.Application.Features.Order.Commands.CreateOrder;

namespace Order.Api.EventConsumer
{
    public class OrderEventConsumer : IConsumer<CheckOutOrder>
    {
        private readonly IMediator _mediaR;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderEventConsumer> _logger;
        public OrderEventConsumer(IMediator mediaR, IMapper mapper,ILogger<OrderEventConsumer> logger)
        {
            _logger = logger;
            _mediaR = mediaR;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<CheckOutOrder> context)
        {
            _logger.LogInformation("The consumer is triggered for {MessageId}", context.MessageId);
            var orderToCheckOut=_mapper.Map<CreateOrderCommand>(context.Message);
            await _mediaR.Send(orderToCheckOut);
            _logger.LogInformation("The event has been Consumed the {message}", context.Message);
        }
    }
}
