using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using Web_Api_CRUD.Domain;
using Web_Api_CRUD.Domain.DTO;
using Web_Api_CRUD.Domain.Enums;
using Web_Api_CRUD.Infraestructure;
using Web_Api_CRUD.Services;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace TEST.Service
{
    [UseAutofacTestFramework]
    public class PedidoServiceTest
    {
         private readonly IPedidoService _service;
        private readonly ApplicationDbContext _context;
        private Faker faker = new Faker("pt_BR");
        private Cliente cliente = new Cliente();
        private List<Produto> listProduto = new();
        private List<Pedido> listPedido = new ();
        public PedidoServiceTest(IPedidoService service, ApplicationDbContext context)
        {
            _service = service;
            _context = context;
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
            List<PedidoProduto> pedidoProdutos = new ();
            foreach(Produto produto in listProduto)
            {
                pedidoProdutos.Add(new PedidoProduto(faker.Random.Int(),produto));
            }
            List<Pedido> pedidos = new ();
            for(int i=0; i<12; i++)
            {
                List<PedidoProduto> pedidoProdutos1 = new ();
                foreach(Produto produto in listProduto)
                {
                    pedidoProdutos1.Add(new PedidoProduto(faker.Random.Int(),produto));
                }
                pedidos.Add(new Pedido(cliente.Id, pedidoProdutos1));
            }
            _context.pedidos.AddRange(pedidos);
            _context.SaveChanges();
            listPedido = pedidos;
        }

        [Fact]
        public async void CreateAsyncTest()
        {
            List<ProdutoQuantidadeDTO> pedidoProdutos = new ();
            foreach(Produto produto in listProduto)
            {
                pedidoProdutos.Add(new ProdutoQuantidadeDTO(){Produto= produto.Id, Quantidade= faker.Random.Int(10)});
            }
           PedidoDTO pedidoDTO = new PedidoDTO()
           {
            listaProdutos = pedidoProdutos
           };
           var result1 = await _service.CreateAsync(cliente.Id, pedidoDTO);
           result1.Should().NotBeNull();
           result1.Should().BeOfType<Pedido>();
           var result2 = await _service.CreateAsync(Guid.NewGuid(), pedidoDTO);
           result2.Should().BeOfType<string>();
           pedidoDTO.listaProdutos.Add(new ProdutoQuantidadeDTO(){Produto = Guid.NewGuid(), Quantidade = faker.Random.Int(10)});
           var result3 = await _service.CreateAsync(cliente.Id, pedidoDTO);
           result3.Should().BeOfType<string>();
        }

        [Fact]
        public async void GetAllPageTest()
        {
            PedidoConsultaDTO dto = new PedidoConsultaDTO();
            var result1 = await _service.GetAllPage(dto);
            result1.Should().BeOfType<List<Pedido>>();
            dto.index = -1;
            var result2 = await _service.GetAllPage(dto);
            result2.Should().BeOfType<string>();
        }

        [Fact]
        public async void GetPedidoByIdAsyncTest()
        {
            var result1 = await _service.GetPedidoByIdAsync(listPedido[0].Id);
            result1.Should().BeOfType<Pedido>();
            var result2 = await _service.GetPedidoByIdAsync(Guid.NewGuid());
            result2.Should().BeOfType<string>();
        }

        [Fact]
        public async void UpdatePedidoAsyncTest()
        {
            List<ProdutoQuantidadeDTO> produtoQuantidadeDTOs = new();
            foreach(Produto produto in listProduto)
            {
                produtoQuantidadeDTOs.Add(new ProdutoQuantidadeDTO(){Produto = produto.Id, Quantidade = faker.Random.Int(10)});
            }
            PedidoUpdateDTO dto = new PedidoUpdateDTO()
            {
                Id = listPedido[0].Id,
            };
            var result1 = await _service.UpdatePedidoAsync(dto);
            result1.Should().BeOfType<string>();
            dto.Produtos = produtoQuantidadeDTOs;
            dto.Id = Guid.NewGuid();
            var result2 = await _service.UpdatePedidoAsync(dto);
            result2.Should().BeOfType<string>();
            dto.Id = listPedido[0].Id;
            var result3 = await _service.UpdatePedidoAsync(dto);
            result3.Should().BeOfType<Pedido>();
        }

        [Fact]
        public async void DeletePedidoAsyncTest()
        {
            var result1 = await _service.DeletePedidoAsync(Guid.NewGuid());
            result1.Should().BeOfType<string>();
            var result2 = await _service.DeletePedidoAsync(listPedido[0].Id);
            result2.Should().BeOfType<Boolean>();
        }
    }
}