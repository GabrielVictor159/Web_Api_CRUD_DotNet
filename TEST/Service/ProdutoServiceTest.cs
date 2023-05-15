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
    public class ProdutoServiceTest
    {
        private readonly IProdutoService _service;
        private readonly ApplicationDbContext _context;
        private Faker faker = new Faker("pt_BR");
        public ProdutoServiceTest(IProdutoService service, ApplicationDbContext context)
        {
            _service = service;
            _context = context;
        }

        [Fact]
        public async void CriarProdutoAsyncTest()
        {
            ProdutoDTO dto = new ProdutoDTO()
            {
                Nome = faker.Commerce.ProductName(),
                Valor = faker.Random.Decimal()
            };
            var result1 = await _service.CriarProdutoAsync(dto);
            result1.Should().BeOfType<Produto>();
            var result2 = await _service.CriarProdutoAsync(dto);
            result2.Should().BeOfType<string>();
            dto.Valor = 0;
            var result3 = await _service.CriarProdutoAsync(dto);
            result2.Should().BeOfType<string>();
        }

        [Fact]
        public async void ObterProdutosPaginadosAsyncTest()
        {
            Produto produto = new Produto()
            {
                Id = Guid.NewGuid(),
                Nome = faker.Commerce.ProductName(),
                Valor = faker.Random.Decimal()
            };
            _context.produtos.Add(produto);
            await _context.SaveChangesAsync();
            var result1 = await _service.ObterProdutosPaginadosAsync();
            result1.Should().BeOfType<List<Produto>>();
        }

        [Fact]
        public async void ObterProdutoPorIdAsyncTest()
        {
            Produto produto = new Produto()
            {
                Id = Guid.NewGuid(),
                Nome = faker.Commerce.ProductName(),
                Valor = faker.Random.Decimal()
            };
            _context.produtos.Add(produto);
            await _context.SaveChangesAsync();
            var result1 = await _service.ObterProdutoPorIdAsync(produto.Id);
            result1.Should().BeOfType<Produto>();
            var result2 = await _service.ObterProdutoPorIdAsync(Guid.NewGuid());
            result2.Should().BeOfType<string>();
        }

        [Fact]
        public async void AtualizarProdutoAsyncTest()
        {
            Produto produto = new Produto()
            {
                Id = Guid.NewGuid(),
                Nome = faker.Commerce.ProductName(),
                Valor = faker.Random.Decimal(10)
            };
            _context.produtos.Add(produto);
            await _context.SaveChangesAsync();
            ProdutoDTO produtoDTO = new ProdutoDTO()
            {
                Nome = faker.Commerce.ProductName(),
                Valor = faker.Random.Decimal(10)
            };
            var result1 = await _service.AtualizarProdutoAsync(produto.Id, produtoDTO);
            result1.Should().BeOfType<Produto>();
            var result2 = await _service.AtualizarProdutoAsync(Guid.NewGuid(), produtoDTO);
            result2.Should().BeOfType<string>();
            produtoDTO.Valor = 0;
            var result3 = await _service.AtualizarProdutoAsync(produto.Id, produtoDTO);
            result3.Should().BeOfType<string>();
        }

        [Fact]
        public async void ExcluirProdutoAsync()
        {
            Produto produto = new Produto()
            {
                Id = Guid.NewGuid(),
                Nome = faker.Commerce.ProductName(),
                Valor = faker.Random.Decimal(10)
            };
            _context.produtos.Add(produto);
            await _context.SaveChangesAsync();
            var result1 = await _service.ExcluirProdutoAsync(produto.Id);
            result1.Should().BeOfType<Boolean>();
            var result2 = await _service.ExcluirProdutoAsync(produto.Id);
            result2.Should().BeOfType<string>();
        }
    }
}