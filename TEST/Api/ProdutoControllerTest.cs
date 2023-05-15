using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Web_Api_CRUD.Controllers;
using Web_Api_CRUD.Infraestructure;
using Web_Api_CRUD.Domain;
using Web_Api_CRUD.Domain.DTO;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace TEST.Api
{
    [UseAutofacTestFramework]
    public class ProdutoControllerTest
    {
        private readonly ApplicationDbContext _context;
        private readonly ProdutoController _controller;
        private Faker faker = new Faker("pt_BR");
        public ProdutoControllerTest(ProdutoController controller, ApplicationDbContext applicationDbContext)
        {
            _controller = controller;
            _context = applicationDbContext;
        }

        [Fact]
        public async void CadastrarProdutoTest()
        {
            ProdutoDTO dto = new ProdutoDTO()
            {
                Nome = faker.Commerce.ProductName(),
                Valor = faker.Random.Decimal()
            };
            var result1 = await _controller.CadastrarProduto(dto);
            result1.Result.Should().BeOfType<OkObjectResult>();
            var result2 = await _controller.CadastrarProduto(dto);
            result2.Result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async void GetAllPageTest()
        {
            List<Produto> listProduto = new();
            for (int i = 0; i < 20; i++)
            {
                listProduto.Add(new Produto() { Id = Guid.NewGuid(), Nome = faker.Commerce.ProductName(), Valor = faker.Random.Decimal() });
            }
            _context.produtos.AddRange(listProduto);
            await _context.SaveChangesAsync();
            ProdutoConsultaDTO dto = new ProdutoConsultaDTO();
            var result1 = await _controller.getAllPage(dto);
            result1.Result.Should().BeOfType<OkObjectResult>();
            dto.index = 0;
            var result2 = await _controller.getAllPage(dto);
            result2.Result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async void GetOneTest()
        {
            Produto produto = new Produto()
            {
                Id = Guid.NewGuid(),
                Nome = faker.Commerce.ProductName(),
                Valor = faker.Random.Decimal()
            };
            _context.produtos.Add(produto);
            await _context.SaveChangesAsync();
            var result1 = await _controller.getOne(produto.Id);
            result1.Result.Should().BeOfType<OkObjectResult>();
            var result2 = await _controller.getOne(Guid.NewGuid());
            result2.Result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async void AlterarProdutoTest()
        {
            Produto produto = new Produto()
            {
                Id = Guid.NewGuid(),
                Nome = faker.Commerce.ProductName(),
                Valor = faker.Random.Decimal()
            };
            _context.produtos.Add(produto);
            await _context.SaveChangesAsync();
            ProdutoAtualizarDTO dto = new ProdutoAtualizarDTO()
            {
                Id = produto.Id,
                Nome = faker.Commerce.ProductName(),
                Valor = faker.Random.Decimal()
            };
            var result1 = await _controller.alterarProduto(dto);
            result1.Result.Should().BeOfType<OkObjectResult>();
            dto.Id = Guid.NewGuid();
            var result2 = await _controller.alterarProduto(dto);
            result2.Result.Should().BeOfType<NotFoundObjectResult>();
            dto.Id = produto.Id;
            dto.Nome = "";
            var result3 = await _controller.alterarProduto(dto);
            result3.Result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async void DeletarProduto()
        {
            Produto produto = new Produto()
            {
                Id = Guid.NewGuid(),
                Nome = faker.Commerce.ProductName(),
                Valor = faker.Random.Decimal()
            };
            _context.produtos.Add(produto);
            await _context.SaveChangesAsync();
            var result1 = await _controller.deletarProduto(produto.Id);
            result1.Result.Should().BeOfType<OkObjectResult>();
            var result2 = await _controller.deletarProduto(Guid.NewGuid());
            result2.Result.Should().BeOfType<NotFoundObjectResult>();
        }
    }
}