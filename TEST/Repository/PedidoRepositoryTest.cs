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
using Microsoft.EntityFrameworkCore;

namespace TEST.Repository
{
    [UseAutofacTestFramework]
    public class PedidoRepositoryTest
    {
        private readonly IPedidoRepository _repository;
        private readonly ApplicationDbContext _context;
        private Faker faker;
        private Cliente cliente;
        private List<Produto> listProduto = new();
        public PedidoRepositoryTest(IPedidoRepository repository, ApplicationDbContext context)
        {
            _repository = repository;
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
            List<ProdutoQuantidadeDTO> produtoQuantidade = new();
            foreach (Produto produto in listProduto)
            {
                produtoQuantidade.Add(
                    new ProdutoQuantidadeDTO()
                    {
                        Produto = produto.Id,
                        Quantidade = faker.Random.Int(10)
                    }
                );
            }
            var pedido = await _repository.CreateAsync(cliente.Id, produtoQuantidade);
            pedido.Should().NotBeNull();
        }

        [Fact]
        public async void GetAllPageAsyncTest()
        {
             List<ProdutoQuantidadeDTO> produtoQuantidade = new();
            foreach (Produto produto in listProduto)
            {
                produtoQuantidade.Add(
                    new ProdutoQuantidadeDTO()
                    {
                        Produto = produto.Id,
                        Quantidade = faker.Random.Int(10)
                    }
                );
            }
            var pedido = await _repository.CreateAsync(cliente.Id, produtoQuantidade);
            PedidoConsultaDTO dto = new PedidoConsultaDTO();
            var result1 = await _repository.GetAllPageAsync(dto);
            result1.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async void GetPedidoByIdAsyncTest()
        {
             List<ProdutoQuantidadeDTO> produtoQuantidade = new();
            foreach (Produto produto in listProduto)
            {
                produtoQuantidade.Add(
                    new ProdutoQuantidadeDTO()
                    {
                        Produto = produto.Id,
                        Quantidade = faker.Random.Int(10)
                    }
                );
            }
            var pedido = await _repository.CreateAsync(cliente.Id, produtoQuantidade);
            var result1 = await _repository.GetPedidoByIdAsync(pedido.Id);
            result1.Should().NotBeNull();
            var result2 = await _repository.GetPedidoByIdAsync(Guid.NewGuid());
            result2.Should().BeNull();
        }

        [Fact]
        public async void UpdatePedidoAsyncTest()
        {
             List<ProdutoQuantidadeDTO> produtoQuantidade = new();
            foreach (Produto produto in listProduto)
            {
                produtoQuantidade.Add(
                    new ProdutoQuantidadeDTO()
                    {
                        Produto = produto.Id,
                        Quantidade = faker.Random.Int(10)
                    }
                );
            }
            var pedido = await _repository.CreateAsync(cliente.Id, produtoQuantidade);
            List<ProdutoQuantidadeDTO> dto = new();
            for (int i = 0; i < 10; i++)
            {
                dto.Add(new ProdutoQuantidadeDTO() { Produto = listProduto[i].Id, Quantidade = faker.Random.Int() });
            }
            var result1 = await _repository.UpdatePedidoAsync(pedido.Id, dto);
            result1.Should().NotBeNull();
            var result2 = await _repository.UpdatePedidoAsync(Guid.NewGuid(), dto);
            result2.Should().BeNull();
        }

        [Fact]
        public async void DeletePedidoAsyncTest()
        {
            List<ProdutoQuantidadeDTO> produtoQuantidade = new();
            foreach (Produto produto in listProduto)
            {
                produtoQuantidade.Add(
                    new ProdutoQuantidadeDTO()
                    {
                        Produto = produto.Id,
                        Quantidade = faker.Random.Int(10)
                    }
                );
            }
            var pedido = await _repository.CreateAsync(cliente.Id, produtoQuantidade);
            Guid id = pedido.Id;
            var result1 = await _repository.DeletePedidoAsync(Guid.NewGuid());
            result1.Should().BeFalse();
            var result2 = await _repository.DeletePedidoAsync(pedido.Id);
            result2.Should().BeTrue();
            var result3 = await Task.FromResult(_context.pedidos.FirstOrDefault(p => p.Id == id));
            result3.Should().BeNull();
        }

        [Fact]
        public async void DeletePedidoProdutoByPedido()
        {
            List<ProdutoQuantidadeDTO> produtoQuantidade = new();
            foreach (Produto produto in listProduto)
            {
                produtoQuantidade.Add(
                    new ProdutoQuantidadeDTO()
                    {
                        Produto = produto.Id,
                        Quantidade = faker.Random.Int(10)
                    }
                );
            }
            var pedido = await _repository.CreateAsync(cliente.Id, produtoQuantidade);
            await _repository.DeletePedidoProdutoByPedido(pedido.Id);
            List<PedidoProduto> pedidoProdutosDelete = await _context.pedidoProdutos
                .Include(p => p.Produto)
                .Where(p => p.IdPedido == pedido.Id)
                .ToListAsync();
            pedidoProdutosDelete.Should().BeEmpty();
        }

        [Fact]
        public async void GetPedidoProdutosTest()
        {  
            List<PedidoProduto> pedidoProdutos = new();
            foreach (Produto produto in listProduto)
            {
                pedidoProdutos.Add
                (
                    new PedidoProduto(faker.Random.Int(10), produto)
                );
            }
            Pedido pedido = new Pedido(cliente.Id, pedidoProdutos);
            pedido.Lista = pedidoProdutos;
            _context.pedidos.Add(pedido);
            await _context.SaveChangesAsync();
            List<PedidoProduto> list = await _repository.GetPedidoProdutos(pedido.Id);
            list.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async void CreateListPedidoProdutoAsyncTest()
        {
            List<ProdutoQuantidadeDTO> produtoQuantidade = new();
            foreach (Produto produto in listProduto)
            {
                produtoQuantidade.Add(
                    new ProdutoQuantidadeDTO()
                    {
                        Produto = produto.Id,
                        Quantidade = faker.Random.Int(10)
                    }
                );
            }
            var result1 = await _repository.CreateListPedidoProdutoAsync( produtoQuantidade);
            result1.Should().NotBeNullOrEmpty();
        }
    }
}