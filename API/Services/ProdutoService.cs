using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Domain.DTO;
using Web_Api_CRUD.Domain;
using Web_Api_CRUD.Domain.DTO;
using Web_Api_CRUD.Repository;

namespace Web_Api_CRUD.Services
{
    public interface IProdutoService
    {
        Task<Object> CriarProdutoAsync(ProdutoDTO produtoDto);
        Task<List<Produto>> ObterProdutosPaginadosAsync(int? index = 1, int? size = 10, string? nome = null, decimal? valorMinimo = null, decimal? valorMaximo = null, string? id = null);
        Task<Object> ObterProdutoPorIdAsync(Guid id);
        Task<Object> AtualizarProdutoAsync(Guid id, ProdutoDTO produtoDto);
        Task<Object> ExcluirProdutoAsync(Guid id);
    }
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _IProdutoRepository;
        public ProdutoService(IProdutoRepository iProdutoRepository)
        {
            _IProdutoRepository = iProdutoRepository;
        }

        public async Task<Object> CriarProdutoAsync(ProdutoDTO produtoDto)
        {
            var dtoValidate = new ProdutoDTOValidation().Validate(produtoDto);
            var produtoConsulta = await _IProdutoRepository.GetProdutoByNameAsync(produtoDto.Nome);
            if (produtoConsulta != null)
            {
                return "Ja existe um produto com o mesmo nome. ";
            }
            if (dtoValidate.IsValid)
            {
                return await _IProdutoRepository.CreateAsync(produtoDto);
            }
            else
            {
                return dtoValidate.ToString();
            }
        }
        public async Task<List<Produto>> ObterProdutosPaginadosAsync(int? index = 1, int? size = 10, string? nome = null, decimal? valorMinimo = null, decimal? valorMaximo = null, string? id = null)
        {
            List<Produto> produtos = await _IProdutoRepository.GetAllPageAsync(index, size, nome, valorMinimo, valorMaximo, id);
            foreach (Produto produto in produtos)
            {
                if (produto.Id != null)
                {
                    produto.Lista = await _IProdutoRepository.GetPedidoProdutos(produto.Id);
                }
            }
            return produtos;
        }

        public async Task<Object> ObterProdutoPorIdAsync(Guid id)
        {
            var produto = await _IProdutoRepository.GetProdutoByIdAsync(id);
            if (produto != null)
            {
                produto.Lista = await _IProdutoRepository.GetPedidoProdutos(id);
                return produto;
            }
            return "Produto não encontrado.";
        }

        public async Task<Object> AtualizarProdutoAsync(Guid id, ProdutoDTO produtoDto)
        {
            var dtoValidate = new ProdutoDTOValidation().Validate(produtoDto);
            if (!dtoValidate.IsValid)
            {
                return dtoValidate.ToString();
            }
            var produto = await _IProdutoRepository.UpdateAsync(id, produtoDto);
            if (produto == null)
            {
                return "Produto não encontrado.";
            }
            produto.Lista = await _IProdutoRepository.GetPedidoProdutos(id);
            return produto;
        }

        public async Task<Object> ExcluirProdutoAsync(Guid id)
        {
            Boolean delete = await _IProdutoRepository.DeleteAsync(id);
            if (delete)
            {
                return true;
            }
            return "Produto não encontrado.";
        }
    }
}