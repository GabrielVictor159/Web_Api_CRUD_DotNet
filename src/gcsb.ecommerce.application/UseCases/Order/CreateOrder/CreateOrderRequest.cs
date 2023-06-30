using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.application.Boundaries.Order;

namespace gcsb.ecommerce.application.UseCases.Order.CreateOrder
{
    public class CreateOrderRequest
    {
       public domain.Order.Order? Order {get;private set;} 
       public Guid IdUser {get; private set;}
       public string? Cupons { get; private set; } 
       public List<(Guid Id, int Quantity)> listProducts = new List<(Guid Id, int Quantity)>();
       public CreateOrderOutput? OrderOutput {get; private set;}
       public CreateOrderRequest(Guid idUser,  List<(Guid Id, int Quantity)> listProducts, string? Cupons)
       {
        this.IdUser = idUser;
        this.Cupons = Cupons;
        this.listProducts = listProducts;
       }
       public void SetOrder(domain.Order.Order order)
       =>Order = order;
       public void SetOutput(CreateOrderOutput output)
        =>OrderOutput = output;
    }
}