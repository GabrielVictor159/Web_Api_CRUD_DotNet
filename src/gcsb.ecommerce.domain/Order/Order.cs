using System;
using System.Collections.Generic;
using System.Linq;
using gcsb.ecommerce.domain.Enums;
using gcsb.ecommerce.domain.Validator.Order;

namespace gcsb.ecommerce.domain.Order
{
    public class Order : Entity
    {
        public Guid Id { get; private set; }
        public Guid IdClient { get; private set; }
        public decimal TotalOrder { get; private set; }
        public string Cupons { get; private set; } = "";
        public Guid IdPagamento { get; set; }
        public List<domain.OrderProduct.OrderProduct> List { get; private set; } = new();
        public DateTime OrderDate { get; private set; }

        public Order(Guid idClient, List<domain.OrderProduct.OrderProduct> list, DateTime orderDate, string? nameCupom = null)
        {
            InitializeOrder(idClient, list, orderDate, nameCupom);
        }

        public Order(Guid id, Guid idClient, List<domain.OrderProduct.OrderProduct> list, DateTime orderDate, string? nameCupom = null)
        {
            InitializeOrder(idClient, list, orderDate, nameCupom);
            this.Id = id;
        }

        private void InitializeOrder(Guid idClient, List<domain.OrderProduct.OrderProduct> list, DateTime orderDate, string? nameCupom)
        {
            this.Id = Guid.NewGuid();
            this.IdClient = idClient;
            this.List = list;
            CalculateTotalOrder();
            this.OrderDate = orderDate;
            if (nameCupom != null)
            {
                ApplyCupom(nameCupom);
            }
        }

        private void CalculateTotalOrder()
        {
            decimal totalOrder = 0;
            foreach (var orderProduct in List)
            {
                totalOrder += orderProduct.TotalOrderLine;
            }
            this.TotalOrder = totalOrder;
        }

        public void WithList(List<domain.OrderProduct.OrderProduct> list)
        {
            this.List = list;
            CalculateTotalOrder();
        }

        public void WithCupom(string nameCupom)
        {
            ApplyCupom(nameCupom);
        }

        private void ApplyCupom(string nameCupom)
        {
            Type typeEnum = typeof(Cupons);
            if (Enum.IsDefined(typeEnum, nameCupom))
            {
                Cupons cupom = (Cupons)Enum.Parse(typeEnum, nameCupom);
                int value = (int)cupom;
                decimal discountedValue = TotalOrder * (value / 100);
                this.TotalOrder = TotalOrder - discountedValue;
                this.Cupons = nameCupom;
            }
        }
    }
}
