using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.Boundaries.Client;
using gcsb.ecommerce.infrastructure.Database.Entities;

namespace gcsb.ecommerce.infrastructure.Database.AutoMapperProfile
{
  public class InfraDomainProfile : AutoMapper.Profile
{
    public InfraDomainProfile()
    {
        CreateMap<Client, domain.Client.Client>().ReverseMap();
        CreateMap<domain.Client.Client, GetClientOutput>().ReverseMap();
        CreateMap<domain.Client.Client, CreateClientOutput>().ReverseMap();
        CreateMap<domain.Client.Client, UpdateClientOutput>().ReverseMap();
        CreateMap<OrderProduct, domain.OrderProduct.OrderProduct>().ReverseMap();
        CreateMap<Order, domain.Order.Order>()
            .ForMember(dest => dest.ListOrderProduct, opt => opt.Ignore())
            .ReverseMap();
        CreateMap<Product, domain.Product.Product>().ReverseMap();
    }
}

}