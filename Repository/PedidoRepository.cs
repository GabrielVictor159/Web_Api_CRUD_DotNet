using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Web_Api_CRUD.Infraestructure;
using Web_Api_CRUD.Model;
using Web_Api_CRUD.Model.DTO;
using Microsoft.EntityFrameworkCore;
using Web_Api_CRUD.Exceptions;

namespace Web_Api_CRUD.Repository
{
    public interface IPedidoRepository
    {
        Task<Pedido> CreateAsync(Guid idCliente, List<ProdutoQuantidadeDTO> listaProdutos);
        Task<List<Pedido>> GetAllPageAsync(PedidoConsultaDTO filtro);
        Task<Pedido> GetPedidoByIdAsync(Guid id);
        Task<Pedido> UpdatePedidoAsync(Guid id, List<ProdutoQuantidadeDTO> listaProdutos);
        Task DeletePedidoAsync(Guid id);
    }

    public class PedidoRepository : IPedidoRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PedidoRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Pedido> CreateAsync(Guid idCliente, List<ProdutoQuantidadeDTO> listaProdutos)
        {
            var cliente = _context.clientes.FirstOrDefault(c => c.Id == idCliente);
            if (cliente == null)
            {
                throw new ClienteGetException("Id de usuario invalido");
            }
            var produtoIds = new HashSet<Guid>();
            foreach (var produto in listaProdutos)
            {
                if (!produtoIds.Add(produto.Produto))
                {
                    throw new PedidoProdutoInvalidProducts($"O produto com o ID {produto.Produto} esta duplicado na lista.");
                }
            }
            List<Guid> listaProdutosIds = listaProdutos.Select(p => p.Produto).ToList();
            List<Produto> produtosQueEstaoNoBanco = await _context.produtos.Where(p => listaProdutosIds.Contains(p.Id)).ToListAsync();
            List<Guid> produtosNaoEncontradosIds = listaProdutosIds.Except(produtosQueEstaoNoBanco.Select(p => p.Id)).ToList();
            if (produtosNaoEncontradosIds.Any())
            {
                throw new PedidoProdutoInvalidProducts($"Os seguintes produtos não foram encontrados no banco de dados: {string.Join(", ", produtosNaoEncontradosIds)}");
            }
            List<Produto> listaProdutosFinal = await _context.produtos.Where(p => listaProdutosIds.Contains(p.Id)).ToListAsync();
            Pedido pedido = new Pedido();
            pedido.Id = Guid.NewGuid();
            await _context.SaveChangesAsync();
            List<PedidoProduto> listPedidoProduto = new List<PedidoProduto>();
            decimal valorTotal = 0;
            foreach (Produto produto in listaProdutosFinal)
            {
                int quantidade = listaProdutos.Where(p => p.Produto == produto.Id).First().Quantidade;
                PedidoProduto pedidoProduto = new PedidoProduto();
                pedidoProduto.Pedido = pedido;
                pedidoProduto.Produto = produto;
                pedidoProduto.Quantidade = quantidade;
                pedidoProduto.ValorTotalLinha = produto.Valor * quantidade;
                listPedidoProduto.Add(pedidoProduto);
                valorTotal += pedidoProduto.ValorTotalLinha;

            }
            pedido.ValorTotal = valorTotal;
            pedido.idCliente = cliente.Id;
            pedido.cliente = cliente;
            pedido.Lista = listPedidoProduto;
            _context.pedidos.Add(pedido);
            await _context.SaveChangesAsync();
            return pedido;
        }
        public async Task<List<Pedido>> GetAllPageAsync(PedidoConsultaDTO filtro)
        {
            var pedidosQuery = _context.pedidos.AsQueryable();

            if (filtro.Id.HasValue)
            {
                var idString = filtro.Id.ToString();
                pedidosQuery = pedidosQuery.Where(p => p.Id.ToString().Contains(idString.ToLower()));
            }

            if (filtro.idCliente.HasValue)
            {
                var idClienteString = filtro.idCliente.ToString();
                pedidosQuery = pedidosQuery.Where(p => p.idCliente.ToString().Contains(idClienteString.ToLower()));
            }

            if (filtro.valorMinimo.HasValue)
            {
                pedidosQuery = pedidosQuery.Where(p => p.ValorTotal >= filtro.valorMinimo);
            }

            if (filtro.valorMaximo.HasValue)
            {
                pedidosQuery = pedidosQuery.Where(p => p.ValorTotal <= filtro.valorMaximo);
            }

            if (filtro.listaProdutos != null)
            {
                foreach (var produto in filtro.listaProdutos)
                {
                    var produtoIdString = produto.Produto.ToString();
                    pedidosQuery = pedidosQuery.Where(p =>
                        p.Lista.Any(pp => pp.IdProduto.ToString().Contains(produtoIdString.ToLower())
                        && pp.Quantidade >= produto.Quantidade));
                }
            }

            var pedidos = await pedidosQuery
                .Skip((int)((filtro.index - 1) * filtro.size))
                .Take((int)filtro.size)
                .ToListAsync();

            foreach (var pedido in pedidos)
            {
                pedido.Lista = await GetPedidoProdutos(pedido.Id);
            }

            return pedidos;
        }
        public async Task<Pedido> GetPedidoByIdAsync(Guid id)
        {
            var pedido = await Task.FromResult(_context.pedidos.FirstOrDefault(c => c.Id == id));
            if (pedido == null)
            {
                throw new PedidoConsultaException($"O Pedido com o ID {id} não foi encontrado");
            }
            pedido.Lista = await GetPedidoProdutos(pedido.Id);
            return pedido;
        }
        public async Task<Pedido> UpdatePedidoAsync(Guid id, List<ProdutoQuantidadeDTO> listaProdutos)
        {
            var produtoIds = new HashSet<Guid>();
            foreach (var produto in listaProdutos)
            {
                if (!produtoIds.Add(produto.Produto))
                {
                    throw new PedidoProdutoInvalidProducts($"O produto com o ID {produto.Produto} esta duplicado na lista.");
                }
            }
            List<Guid> listaProdutosIds = listaProdutos.Select(p => p.Produto).ToList();
            List<Produto> produtosQueEstaoNoBanco = await _context.produtos.Where(p => listaProdutosIds.Contains(p.Id)).ToListAsync();
            List<Guid> produtosNaoEncontradosIds = listaProdutosIds.Except(produtosQueEstaoNoBanco.Select(p => p.Id)).ToList();
            if (produtosNaoEncontradosIds.Any())
            {
                throw new PedidoProdutoInvalidProducts($"Os seguintes produtos não foram encontrados no banco de dados: {string.Join(", ", produtosNaoEncontradosIds)}");
            }
            List<Produto> listaProdutosFinal = await _context.produtos.Where(p => listaProdutosIds.Contains(p.Id)).ToListAsync();
            Pedido pedido = await GetPedidoByIdAsync(id);
            _context.pedidoProdutos.RemoveRange(pedido.Lista);
            await _context.SaveChangesAsync();
            List<PedidoProduto> listPedidoProduto = new List<PedidoProduto>();
            decimal valorTotal = 0;
            foreach (Produto produto in listaProdutosFinal)
            {
                int quantidade = listaProdutos.Where(p => p.Produto == produto.Id).First().Quantidade;
                PedidoProduto pedidoProduto = new PedidoProduto();
                pedidoProduto.Pedido = pedido;
                pedidoProduto.Produto = produto;
                pedidoProduto.Quantidade = quantidade;
                pedidoProduto.ValorTotalLinha = produto.Valor * quantidade;
                listPedidoProduto.Add(pedidoProduto);
                valorTotal += pedidoProduto.ValorTotalLinha;

            }
            pedido.ValorTotal = valorTotal;
            pedido.Lista = listPedidoProduto;
            await _context.SaveChangesAsync();
            return pedido;
        }
        public async Task DeletePedidoAsync(Guid id)
        {
            var pedido = await Task.FromResult(_context.pedidos.FirstOrDefault(c => c.Id == id));

            if (pedido == null)
            {
                throw new PedidoConsultaException($"Pedido com o ID {id} não encontrado");
            }
            _context.pedidos.Remove(pedido);
            await _context.SaveChangesAsync();
        }
        private async Task<List<PedidoProduto>> GetPedidoProdutos(Guid idPedido)
        {
            List<PedidoProduto> pedidoProdutos = await _context.pedidoProdutos
                .Include(p => p.Produto)
                .Where(p => p.IdPedido == idPedido)
                .ToListAsync();
            return pedidoProdutos;
        }



    }
}