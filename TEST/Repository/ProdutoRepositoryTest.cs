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
using Web_Api_CRUD.Repository;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace TEST.Repository
{
    [UseAutofacTestFramework]
    public class ProdutoRepositoryTest
    {
        private readonly IProdutoRepository _service;
        private readonly ApplicationDbContext _context;
        public ProdutoRepositoryTest(IProdutoRepository service, ApplicationDbContext context)
        {
            _service = service;
            _context = context;
        }


        [Fact]
        public async void CreateTest()
        {
            var faker = new Faker("pt_BR");
            ProdutoDTO dto = new ProdutoDTO()
            {
                Nome = faker.Commerce.ProductName(),
                Valor = faker.Random.Decimal()
            };
            var result1 = await _service.CreateAsync(dto);
            result1.Should().NotBeNull();
            Func<Task> result2 = async () => { await _service.CreateAsync(dto); };
            await result2.Should().ThrowAsync<ProdutoRegisterException>();
        }

        [Fact]
        public async void GetAllPageTest()
        {
            var faker = new Faker("pt_BR");
            List<Produto> listProduto = new();
            for (int i = 0; i < 20; i++)
            {
                listProduto.Add(new Produto() { Id = Guid.NewGuid(), Nome = faker.Commerce.ProductName(), Valor = faker.Random.Decimal() });
            }
            _context.produtos.AddRange(listProduto);
            _context.SaveChanges();
            List<Produto> result1 = await _service.GetAllPageAsync();
            result1.Should().HaveCountGreaterThan(9);
            List<Produto> result2 = await _service.GetAllPageAsync(1, 15);
            result2.Should().HaveCountGreaterThan(14);
            List<Produto> result3 = await _service.GetAllPageAsync(1, 10, listProduto[0].Nome);
            result3.Should().HaveCountGreaterThan(0);
        }

        [Fact]
        public async void GetClienteByIdTest()
        {
            var faker = new Faker("pt_BR");
            Produto produto = new Produto()
            {
                Id = Guid.NewGuid(),
                Nome = faker.Commerce.ProductName(),
                Valor = faker.Random.Decimal()
            };
            _context.produtos.Add(produto);
            _context.SaveChanges();
            Produto result = await _service.GetProdutoByIdAsync(produto.Id);
            result.Should().NotBeNull();
            Func<Task> result2 = async () => { await _service.GetProdutoByIdAsync(Guid.NewGuid()); };
            await result2.Should().ThrowAsync<ProdutoConsultaException>();
        }

        [Fact]
        public async void UpdateTest()
        {
            var faker = new Faker("pt_BR");
            Produto produto = new Produto()
            {
                Id = Guid.NewGuid(),
                Nome = faker.Commerce.ProductName(),
                Valor = faker.Random.Decimal()
            };
            _context.produtos.Add(produto);
            _context.SaveChanges();
            ProdutoDTO dto = new ProdutoDTO()
            {
                Nome = faker.Commerce.ProductName(),
                Valor = faker.Random.Decimal()
            };
            var result = await _service.UpdateAsync(produto.Id, dto);
            result.Nome.Should().Be(dto.Nome);
            Func<Task> result2 = async () => { await _service.UpdateAsync(Guid.NewGuid(), dto); };
            await result2.Should().ThrowAsync<ProdutoConsultaException>();
        }

        [Fact]
        public async void DeleteTest()
        {
            var faker = new Faker("pt_BR");
            Produto produto = new Produto()
            {
                Id = Guid.NewGuid(),
                Nome = faker.Commerce.ProductName(),
                Valor = faker.Random.Decimal()
            };
            _context.produtos.Add(produto);
            _context.SaveChanges();
            Func<Task> result1 = async () => { await _service.DeleteAsync(produto.Id); };
            await result1.Should().NotThrowAsync();
            Func<Task> result2 = async () => { await _service.DeleteAsync(Guid.NewGuid()); };
            await result2.Should().ThrowAsync<ProdutoConsultaException>();
        }
    }

}