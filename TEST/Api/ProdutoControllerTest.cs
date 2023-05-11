using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Web_Api_CRUD.Controllers;
using Web_Api_CRUD.Infraestructure;
using Web_Api_CRUD.Model;
using Web_Api_CRUD.Model.DTO;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace TEST.Api
{
     [UseAutofacTestFramework]
    public class ProdutoControllerTest
    {
        private readonly ApplicationDbContext _context;
        private readonly ProdutoController produtoController;
        public ProdutoControllerTest(ProdutoController controller,ApplicationDbContext applicationDbContext)
        {
        produtoController = controller;
        _context = applicationDbContext;
        } 

        [Fact]
        public async void CadastrarProdutoTest()
        {
          var faker = new Faker("pt_BR");
          ProdutoDTO dto = new ProdutoDTO()
          {
            Nome = faker.Commerce.ProductName(),
            Valor = faker.Random.Decimal()
          };
          var result1 = await produtoController.CadastrarProduto(dto);
          result1.Result.Should().BeOfType<OkObjectResult>();
          var result2 = await produtoController.CadastrarProduto(dto);
          result2.Result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async void GetAllPageTest()
        {
             var faker = new Faker("pt_BR"); 
            List<Produto> listProduto = new ();
            for(int i=0;i<20; i++)
            {
                listProduto.Add(new Produto(){Id = Guid.NewGuid(), Nome = faker.Commerce.ProductName(), Valor=faker.Random.Decimal()});
            }
            _context.produtos.AddRange(listProduto);
            _context.SaveChanges();
            ProdutoConsultaDTO dto = new ProdutoConsultaDTO();
             var result1 = await produtoController.getAllPage(dto);
             result1.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async void GetOneTest()
        {
           var faker = new Faker("pt_BR");
           Produto produto = new Produto()
             {
                Id = Guid.NewGuid(),
                Nome = faker.Commerce.ProductName(),
                Valor=faker.Random.Decimal()
             };
             _context.produtos.Add(produto);
             _context.SaveChanges();
            var result1 = await produtoController.getOne(produto.Id);
            result1.Result.Should().BeOfType<OkObjectResult>();
            var result2 = await produtoController.getOne(Guid.NewGuid());
            result2.Result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async void AlterarProdutoTest()
        {
           var faker = new Faker("pt_BR");
           Produto produto = new Produto()
             {
                Id = Guid.NewGuid(),
                Nome = faker.Commerce.ProductName(),
                Valor=faker.Random.Decimal()
             };
             _context.produtos.Add(produto);
             _context.SaveChanges();
            ProdutoAtualizarDTO dto = new ProdutoAtualizarDTO()
            {
                Id = produto.Id,
                Nome = faker.Commerce.ProductName(),
                Valor=faker.Random.Decimal() 
            };
            var result1 = await produtoController.alterarProduto(dto);
            result1.Result.Should().BeOfType<OkObjectResult>();
            dto.Id = Guid.NewGuid();
            var result2 = await produtoController.alterarProduto(dto);
            result2.Result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async void DeletarProduto()
        {
            var faker = new Faker("pt_BR");
           Produto produto = new Produto()
             {
                Id = Guid.NewGuid(),
                Nome = faker.Commerce.ProductName(),
                Valor=faker.Random.Decimal()
             };
             _context.produtos.Add(produto);
             _context.SaveChanges();
              var result1 = await produtoController.deletarProduto(produto.Id);
            result1.Result.Should().BeOfType<OkObjectResult>();
            var result2 = await produtoController.deletarProduto(Guid.NewGuid());
            result2.Result.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}