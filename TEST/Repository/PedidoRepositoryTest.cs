using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using Web_Api_CRUD.Exceptions;
using Web_Api_CRUD.Infraestructure;
using Web_Api_CRUD.Domain;
using Web_Api_CRUD.Domain.DTO;
using Web_Api_CRUD.Domain.Enums;
using Web_Api_CRUD.Repository;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace TEST.Repository
{
    [UseAutofacTestFramework]
    public class PedidoRepositoryTest
    {
        private readonly IPedidoRepository _service;
        private readonly ApplicationDbContext _context;
        private Faker faker;
        private Cliente cliente;
        private List<Produto> listProduto = new();
        public PedidoRepositoryTest(IPedidoRepository service, ApplicationDbContext context)
        {
            _service = service;
            _context = context;
            faker = new Faker("pt_BR");
            cliente = new Cliente()
            {
                Id = Guid.NewGuid(),
                Nome = faker.Name.FullName(),
                Senha = faker.Random.String2(10),
                Role = Policies.ADMIN.ToString()
            };
            _context.clientes.Add(cliente);
            _context.SaveChanges();
            for (int i = 0; i < 20; i++)
            {
                listProduto.Add(new Produto() { Id = Guid.NewGuid(), Nome = faker.Commerce.ProductName(), Valor = faker.Random.Decimal() });
            }
            _context.produtos.AddRange(listProduto);
            _context.SaveChanges();
        }

        [Fact]
        public async void CreateAsyncTest()
        {
            List<ProdutoQuantidadeDTO> dto = new();
            for (int i = 0; i < 10; i++)
            {
                dto.Add(new ProdutoQuantidadeDTO() { Produto = listProduto[i].Id, Quantidade = faker.Random.Int() });
            }
            var result1 = await _service.CreateAsync(cliente.Id, dto);
            result1.Should().NotBeNull();
            Func<Task> result2 = async () => { await _service.CreateAsync(Guid.NewGuid(), dto); };
            await result2.Should().ThrowAsync<ClienteGetException>();
            dto[1].Produto = dto[0].Produto;
            Func<Task> result3 = async () => { await _service.CreateAsync(cliente.Id, dto); };
            await result3.Should().ThrowAsync<PedidoProdutoInvalidProducts>();
            dto[1].Produto = Guid.NewGuid();
            Func<Task> result4 = async () => { await _service.CreateAsync(cliente.Id, dto); };
            await result4.Should().ThrowAsync<PedidoProdutoInvalidProducts>();
        }

        [Fact]
        public async void GetAllPageAsyncTest()
        {
            PedidoConsultaDTO dto = new PedidoConsultaDTO();
            var result1 = await _service.GetAllPageAsync(dto);
            result1.Should().NotBeNull();
        }

        [Fact]
        public async void GetPedidoByIdAsyncTest()
        {
            Pedido pedido = new Pedido()
            {
                Id = Guid.NewGuid(),
                idCliente = cliente.Id,
                ValorTotal = faker.Random.Decimal()
            };
            _context.pedidos.Add(pedido);
            _context.SaveChanges();
            var result1 = await _service.GetPedidoByIdAsync(pedido.Id);
            result1.Should().NotBeNull();
            Func<Task> result2 = async () => { await _service.GetPedidoByIdAsync(Guid.NewGuid()); };
            await result2.Should().ThrowAsync<PedidoConsultaException>();
        }

        [Fact]
        public async void UpdatePedidoAsyncTest()
        {
            Pedido pedido = new Pedido()
            {
                Id = Guid.NewGuid(),
                idCliente = cliente.Id,
                ValorTotal = faker.Random.Decimal()
            };
            _context.pedidos.Add(pedido);
            _context.SaveChanges();
            List<ProdutoQuantidadeDTO> dto = new();
            for (int i = 0; i < 10; i++)
            {
                dto.Add(new ProdutoQuantidadeDTO() { Produto = listProduto[i].Id, Quantidade = faker.Random.Int() });
            }
            var result1 = await _service.UpdatePedidoAsync(pedido.Id, dto);
            result1.Should().NotBeNull();
            Func<Task> result2 = async () => { await _service.UpdatePedidoAsync(Guid.NewGuid(), dto); };
            await result2.Should().ThrowAsync<PedidoConsultaException>();
            dto[1].Produto = dto[0].Produto;
            Func<Task> result3 = async () => { await _service.UpdatePedidoAsync(pedido.Id, dto); };
            await result3.Should().ThrowAsync<PedidoProdutoInvalidProducts>();
            dto[1].Produto = Guid.NewGuid();
            Func<Task> result4 = async () => { await _service.UpdatePedidoAsync(pedido.Id, dto); };
            await result4.Should().ThrowAsync<PedidoProdutoInvalidProducts>();
        }

        [Fact]
        public async void DeletePedidoAsyncTest()
        {
            Pedido pedido = new Pedido()
            {
                Id = Guid.NewGuid(),
                idCliente = cliente.Id,
                ValorTotal = faker.Random.Decimal()
            };
            _context.pedidos.Add(pedido);
            _context.SaveChanges();
            Func<Task> result1 = async () => { await _service.DeletePedidoAsync(Guid.NewGuid()); };
            await result1.Should().ThrowAsync<PedidoConsultaException>();
            Func<Task> result2 = async () => { await _service.DeletePedidoAsync(pedido.Id); };
            await result2.Should().NotThrowAsync();
        }
    }
}