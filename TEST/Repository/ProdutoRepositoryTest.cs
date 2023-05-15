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
        private readonly IProdutoRepository _repository;
        private readonly ApplicationDbContext _context;
        private Faker faker = new Faker("pt_BR");
        public ProdutoRepositoryTest(IProdutoRepository repository, ApplicationDbContext context)
        {
            _repository = repository;
            _context = context;
        }


        [Fact]
        public async void CreateAsyncTest()
        {
            ProdutoDTO dto = new ProdutoDTO()
            {
                Nome = faker.Commerce.ProductName(),
                Valor = faker.Random.Decimal()
            };
            var result1 = await _repository.CreateAsync(dto);
            result1.Should().NotBeNull();
        }

        [Fact]
        public async void GetProdutoByNameAsyncTest()
        {
            Produto produto = new Produto()
            {
                Id = Guid.NewGuid(),
                Nome = faker.Commerce.ProductName(),
                Valor = faker.Random.Decimal()
            };
            _context.produtos.Add(produto);
            await _context.SaveChangesAsync();
            var result1 = await _repository.GetProdutoByNameAsync(produto.Nome);
            result1.Should().NotBeNull();
            var result2 = await _repository.GetProdutoByNameAsync(faker.Commerce.ProductName());
            result2.Should().BeNull();
        }

        [Fact]
        public async void GetAllPageAsyncTest()
        {
            List<Produto> listProduto = new();
            for (int i = 0; i < 20; i++)
            {
                listProduto.Add(new Produto() { Id = Guid.NewGuid(), Nome = faker.Commerce.ProductName(), Valor = faker.Random.Decimal() });
            }
            _context.produtos.AddRange(listProduto);
            await _context.SaveChangesAsync();
            List<Produto> result1 = await _repository.GetAllPageAsync();
            result1.Should().HaveCountGreaterThan(9);
            List<Produto> result2 = await _repository.GetAllPageAsync(1, 15);
            result2.Should().HaveCountGreaterThan(14);
            List<Produto> result3 = await _repository.GetAllPageAsync(1, 10, listProduto[0].Nome);
            result3.Should().HaveCountGreaterThan(0);
        }

        [Fact]
        public async void GetClienteByIdAyncTest()
        {
            Produto produto = new Produto()
            {
                Id = Guid.NewGuid(),
                Nome = faker.Commerce.ProductName(),
                Valor = faker.Random.Decimal()
            };
            _context.produtos.Add(produto);
            await _context.SaveChangesAsync();
            var result1 = await _repository.GetProdutoByIdAsync(produto.Id);
            result1.Should().NotBeNull();
            var result2 = await _repository.GetProdutoByIdAsync(Guid.NewGuid());
            result2.Should().BeNull();
        }

        [Fact]
        public async void UpdateAsyncTest()
        {
            Produto produto = new Produto()
            {
                Id = Guid.NewGuid(),
                Nome = faker.Commerce.ProductName(),
                Valor = faker.Random.Decimal()
            };
            _context.produtos.Add(produto);
            await _context.SaveChangesAsync();
            ProdutoDTO dto = new ProdutoDTO()
            {
                Nome = faker.Commerce.ProductName(),
                Valor = faker.Random.Decimal()
            };
            var result1 = await _repository.UpdateAsync(produto.Id, dto);
            result1.Should().NotBeNull();
            if (result1 != null)
            {
                result1.Nome.Should().Be(dto.Nome);
            }
            var result2 = await _repository.UpdateAsync(Guid.NewGuid(), dto);
            result2.Should().BeNull();
        }

        [Fact]
        public async void DeleteAsyncTest()
        {
            Produto produto = new Produto()
            {
                Id = Guid.NewGuid(),
                Nome = faker.Commerce.ProductName(),
                Valor = faker.Random.Decimal()
            };
            _context.produtos.Add(produto);
            await _context.SaveChangesAsync();
            var result1 = await _repository.DeleteAsync(produto.Id);
            result1.Should().BeTrue();
            var result2 = await _repository.DeleteAsync(Guid.NewGuid());
            result2.Should().BeFalse();
        }
    }

}