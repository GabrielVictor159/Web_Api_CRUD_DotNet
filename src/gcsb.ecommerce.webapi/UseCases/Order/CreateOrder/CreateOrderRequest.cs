using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using gcsb.ecommerce.application.Boundaries.Order;

namespace gcsb.ecommerce.webapi.UseCases.Order.CreateOrder
{
    public sealed class CreateOrderRequest
    {
       public required List<listProducts> listProducts {get;set;}
       public string? Cupons { get; set; } 
    }
}