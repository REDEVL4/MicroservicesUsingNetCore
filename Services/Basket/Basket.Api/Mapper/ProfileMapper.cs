using MessagingBrokerDefaults.Models;
using AutoMapper;
using Basket.Api.Publisher;

namespace Basket.Api.Mapper
{
    public class ProfileMapper:Profile
    {
        public ProfileMapper()
        {
            CreateMap<CheckOutModelForOrders,CheckOutOrder>().ReverseMap();
        }
    }
}
