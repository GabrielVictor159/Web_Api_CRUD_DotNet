using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_Api_CRUD.Controllers;
using Web_Api_CRUD.Exceptions;
using Web_Api_CRUD.Infraestructure;
using Web_Api_CRUD.Domain;
using Web_Api_CRUD.Domain.DTO;
using Web_Api_CRUD.Domain.Enums;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace TEST.Api
{
    [UseAutofacTestFramework]
    public class PedidoControlerTest
    {
        private readonly PedidoController pedidoController;
        private readonly ApplicationDbContext _context;
        private Faker faker;
        private Cliente cliente;
        private List<Produto> listProduto = new();
        public PedidoControlerTest(PedidoController controller, ApplicationDbContext context)
        {
            pedidoController = controller;
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

            SetHttpContextWithClaims(cliente.Id.ToString());
        }

        [Fact]
        public async void AddPedidoTest()
        {
            List<ProdutoQuantidadeDTO> dto = new();
            for (int i = 0; i < 10; i++)
            {
                dto.Add(new ProdutoQuantidadeDTO() { Produto = listProduto[i].Id, Quantidade = faker.Random.Int() });
            }
            var result1 = await pedidoController.AddPedido(dto);
            result1.Result.Should().BeOfType<OkObjectResult>();

            dto[0].Produto = Guid.NewGuid();
            var result2 = await pedidoController.AddPedido(dto);
            result2.Result.Should().BeOfType<BadRequestObjectResult>();

            SetHttpContextWithClaims(Guid.NewGuid().ToString());
            var result3 = await pedidoController.AddPedido(dto);
            result3.Result.Should().BeOfType<BadRequestObjectResult>();
            SetHttpContextWithClaims(cliente.Id.ToString());
        }

        [Fact]
        public async void GetAllPageTest()
        {
            PedidoConsultaDTO dto = new PedidoConsultaDTO();
            var result1 = await pedidoController.GetAllPage(dto);
            result1.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async void GetOneTest()
        {
            Cliente cliente2 = new Cliente()
            {
                Id = Guid.NewGuid(),
                Nome = faker.Name.FullName(),
                Senha = faker.Random.String2(10),
                Role = Policies.USER.ToString()
            };
            _context.clientes.Add(cliente2);
            _context.SaveChanges();
            Pedido pedido = new Pedido()
            {
                Id = Guid.NewGuid(),
                idCliente = cliente.Id,
                ValorTotal = faker.Random.Decimal()
            };
            _context.pedidos.Add(pedido);
            _context.SaveChanges();
            var result1 = await pedidoController.GetOne(Guid.NewGuid());
            result1.Result.Should().BeOfType<BadRequestObjectResult>();
            SetHttpContextWithClaims(cliente2.Id.ToString(), false);
            var result2 = await pedidoController.GetOne(pedido.Id);
            result2.Result.Should().BeOfType<UnauthorizedObjectResult>();
            SetHttpContextWithClaims(cliente.Id.ToString());
            var result3 = await pedidoController.GetOne(pedido.Id);
            result3.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async void UpdateTest()
        {
            Pedido pedido = new Pedido()
            {
                Id = Guid.NewGuid(),
                idCliente = cliente.Id,
                ValorTotal = faker.Random.Decimal()
            };
            _context.pedidos.Add(pedido);
            _context.SaveChanges();
            List<ProdutoQuantidadeDTO> produtoQuantidadeDTOs = new();
            for (int i = 0; i < 10; i++)
            {
                produtoQuantidadeDTOs.Add(new ProdutoQuantidadeDTO() { Produto = listProduto[i].Id, Quantidade = faker.Random.Int() });
            };
            PedidoUpdateDTO pedidoUpdateDTO = new PedidoUpdateDTO()
            {
                Id = pedido.Id,
                Produtos = produtoQuantidadeDTOs
            };
            var result1 = await pedidoController.Update(pedidoUpdateDTO);
            result1.Result.Should().BeOfType<OkObjectResult>();
            pedidoUpdateDTO.Id = Guid.NewGuid();
            var result2 = await pedidoController.Update(pedidoUpdateDTO);
            result2.Result.Should().BeOfType<BadRequestObjectResult>();
            pedidoUpdateDTO.Id = pedido.Id;
            pedidoUpdateDTO.Produtos[0].Produto = Guid.NewGuid();
            var result3 = await pedidoController.Update(pedidoUpdateDTO);
            result3.Result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async void DeleteTest()
        {
            Cliente cliente2 = new Cliente()
            {
                Id = Guid.NewGuid(),
                Nome = faker.Name.FullName(),
                Senha = faker.Random.String2(10),
                Role = Policies.USER.ToString()
            };
            _context.clientes.Add(cliente2);
            _context.SaveChanges();
            Pedido pedido = new Pedido()
            {
                Id = Guid.NewGuid(),
                idCliente = cliente.Id,
                ValorTotal = faker.Random.Decimal()
            };
            _context.pedidos.Add(pedido);
            _context.SaveChanges();
            SetHttpContextWithClaims(cliente2.Id.ToString(), false);
            var result1 = await pedidoController.Delete(pedido.Id);
            result1.Result.Should().BeOfType<UnauthorizedObjectResult>();
            SetHttpContextWithClaims(cliente.Id.ToString());
            var result2 = await pedidoController.Delete(Guid.NewGuid());
            result2.Result.Should().BeOfType<BadRequestObjectResult>();
            var result3 = await pedidoController.Delete(pedido.Id);
            result3.Result.Should().BeOfType<OkObjectResult>();
        }

        private void SetHttpContextWithClaims(string id, Boolean admin = true)
        {
            var claims = new[]
            {
            new Claim("Id", id),
            new Claim(ClaimTypes.Role, admin?Policies.ADMIN.ToString():Policies.USER.ToString())
        };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var principal = new ClaimsPrincipal(identity);

            var httpContext = new DefaultHttpContext
            {
                User = principal
            };

            pedidoController.ControllerContext.HttpContext = httpContext;
        }

    }
}