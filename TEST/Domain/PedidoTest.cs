using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Domain.Enums;
using Bogus;
using FluentAssertions;
using Web_Api_CRUD.Domain;
using Xunit;

namespace TEST.Domain
{
    public class PedidoTest
    {
        private Faker faker = new Faker("pt_BR");

        private List<Produto> listProducts = new();

        private List<PedidoProduto> listPedidoProduto = new();

        private Cliente cliente;
        public PedidoTest()
        {
            for (int i = 0; i < 10; i++)
            {
                listProducts.Add(new Produto() { Id = Guid.NewGuid(), Nome = faker.Commerce.ProductName(), Valor = faker.Random.Decimal(1, 10) });
            }

            foreach (Produto produto in listProducts)
            {
                listPedidoProduto.Add(new PedidoProduto(1, produto));
            }
            cliente = new Cliente() { Id = Guid.NewGuid(), Nome = faker.Name.FullName(), Senha = faker.Random.String2(10) };
        }

        [Fact]
        public void ValorTotalTest()
        {
            decimal valorTotal = 0;
            Pedido pedido = new Pedido(cliente.Id, listPedidoProduto);
            foreach (PedidoProduto pedidoProduto in listPedidoProduto)
            {
                valorTotal += pedidoProduto.ValorTotalLinha;
            }
            pedido.ValorTotal.Should().Be(valorTotal);
        }

        [Fact]
        public void CupomTest()
        {
            decimal valorTotal = 0;
            Pedido pedido = new Pedido(cliente.Id, listPedidoProduto, "BlackFriday");
            foreach (PedidoProduto pedidoProduto in listPedidoProduto)
            {
                valorTotal += pedidoProduto.ValorTotalLinha;
            }
            decimal valorDescontado = valorTotal * (((int)Cupons.BlackFriday) / 100);
            valorTotal = valorTotal - valorDescontado;
            pedido.ValorTotal.Should().Be(valorTotal);
        }
    }
}