using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace gcsb.ecommerce.webapi.UseCases.Order.CreateOrder
{
    [DataContract(Name = "PostOrderRequest")]
    public sealed class CreateOrderRequest
    {
        [Required]
       public DateTime OrderDate { get; set; }
        [Required]
        public decimal TotalOrder { get; set; }
    }
}