using AutoMapper;
using Order.Application.Features.Order.Commands.CreateOrder;
using Order.Application.Features.Order.Commands.UpdateOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Mappers
{
    public class ProfileMapper:Profile
    {
        public ProfileMapper()
        {
            CreateMap<Order.Domain.Models.Order, Order.Application.Features.Order.Queries.OrderDTO>().ReverseMap();
            CreateMap<Order.Domain.Models.Order, CreateOrderCommand>().ReverseMap();
            CreateMap<Order.Domain.Models.Order, UpdateOrderCommand>().ReverseMap();
        }
    }
}
