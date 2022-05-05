using AutoMapper;
using DiscountGRPC.Models;
using DiscountGRPC.Protos;

namespace DiscountGRPC.Mapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<DiscountedProduct, DiscountResponse>().ReverseMap();
        }
    }
}
