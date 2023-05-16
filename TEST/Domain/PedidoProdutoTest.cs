using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using Web_Api_CRUD.Domain;
using Xunit;

namespace TEST.Domain
{
    public class PedidoProdutoTest
    {
        private Faker faker = new Faker("pt_BR");

        private Produto produto;

        public PedidoProdutoTest()
        {

            produto = new Produto() { Id = Guid.NewGuid(), Nome = faker.Commerce.ProductName(), Valor = faker.Random.Decimal(1, 10) };
        }

        [Fact]
        public void ValorTotalLinhaTest()
        {
            int quantidade = faker.Random.Int(1, 10);
            PedidoProduto pedidoProduto = new PedidoProduto(quantidade, produto);
            decimal valorLinhaTeste = quantidade * produto.Valor;
            pedidoProduto.ValorTotalLinha.Should().Be(valorLinhaTeste);
        }
    }
}