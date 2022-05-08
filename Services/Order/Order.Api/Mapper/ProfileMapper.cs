using AutoMapper;
using MessagingBrokerDefaults.Models;
using Order.Application.Features.Order.Commands.CreateOrder;

namespace Order.Api.Mapper
{
    public class ProfileMapper:Profile
    {
        public ProfileMapper()
        {
            CreateMap<CreateOrderCommand, CheckOutOrder>().ReverseMap();
        }
    }
}
