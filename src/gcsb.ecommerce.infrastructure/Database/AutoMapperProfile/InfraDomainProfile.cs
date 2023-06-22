using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.infrastructure.Database.Entities;

namespace gcsb.ecommerce.infrastructure.Database.AutoMapperProfile
{
    public class InfraDomainProfile : AutoMapper.Profile
    {
        public InfraDomainProfile()
        {
            CreateMap<Order, domain.Order.Order>().ReverseMap();
        }
    }
}