using AutoMapper;
using Discount.Grpc.Models;
using Discount.Grpc.Protos;

namespace Discount.Grpc.Mappings
{
    public class Mapper:Profile
    {
        public Mapper()
        {
            CreateMap<DiscountedProduct, DiscountResponse>().ReverseMap();
        }
    }
}
