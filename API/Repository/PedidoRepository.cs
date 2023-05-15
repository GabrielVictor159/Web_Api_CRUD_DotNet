using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Web_Api_CRUD.Infraestructure;
using Web_Api_CRUD.Domain;
using Web_Api_CRUD.Domain.DTO;
using Microsoft.EntityFrameworkCore;
using Web_Api_CRUD.Exceptions;

namespace Web_Api_CRUD.Repository
{
    public interface IPedidoRepository
    {
        Task<Pedido> CreateAsync(Guid idCliente);
        Task<List<Pedido>> GetAllPageAsync(PedidoConsultaDTO filtro);
        Task<Pedido?> GetPedidoByIdAsync(Guid id);
        Task<Pedido?> UpdatePedidoAsync(Guid id, List<ProdutoQuantidadeDTO> listaProdutos);
        Task<Boolean> DeletePedidoAsync(Guid id);
        Task DeletePedidoProdutoByPedido(Guid id);
        Task<List<PedidoProduto>> GetPedidoProdutos(Guid idPedido);
        Task<List<PedidoProduto>?> CreateListPedidoProdutoAsync(Guid idPedido, List<ProdutoQuantidadeDTO> listaProdutos);
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

        public async Task<Pedido> CreateAsync(Guid idCliente)
        {
            Pedido pedido = new Pedido()
            {
                idCliente = idCliente,
                ValorTotal = 0
            };
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

            return pedidos;
        }
        public async Task<Pedido?> GetPedidoByIdAsync(Guid id)
        {
            return await Task.FromResult(_context.pedidos.FirstOrDefault(p => p.Id == id));
        }
        public async Task<Pedido?> UpdatePedidoAsync(Guid id, List<ProdutoQuantidadeDTO> listaProdutos)
        {
            var pedido = await Task.FromResult(_context.pedidos.FirstOrDefault(p => p.Id == id));
            if (pedido == null)
            {
                return null;
            }
            List<PedidoProduto> pedidoProdutos = await _context.pedidoProdutos
                   .Include(p => p.Produto)
                   .Where(p => p.IdPedido == id)
                   .ToListAsync();
            _context.pedidoProdutos.RemoveRange(pedidoProdutos);
            await _context.SaveChangesAsync();
            var listPedidoProdutos = await CreateListPedidoProdutoAsync(id, listaProdutos);
            return pedido;
        }
        public async Task<Boolean> DeletePedidoAsync(Guid id)
        {
            var pedido = await Task.FromResult(_context.pedidos.FirstOrDefault(c => c.Id == id));
            if (pedido == null)
            {
                return false;
            }
            await DeletePedidoProdutoByPedido(id);
            _context.pedidos.Remove(pedido);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task DeletePedidoProdutoByPedido(Guid id)
        {
            List<PedidoProduto> pedidoProdutos = await GetPedidoProdutos(id);
            _context.pedidoProdutos.RemoveRange(pedidoProdutos);
            await _context.SaveChangesAsync();
        }
        public async Task<List<PedidoProduto>> GetPedidoProdutos(Guid idPedido)
        {
            List<PedidoProduto> pedidoProdutos = await _context.pedidoProdutos
                .Include(p => p.Produto)
                .Where(p => p.IdPedido == idPedido)
                .ToListAsync();
            return pedidoProdutos;
        }
        public async Task<List<PedidoProduto>?> CreateListPedidoProdutoAsync(Guid idPedido, List<ProdutoQuantidadeDTO> listaProdutos)
        {
            var pedido = await Task.FromResult(_context.pedidos.FirstOrDefault(c => c.Id == idPedido));
            if (pedido == null)
            {
                return null;
            }
            List<Guid> listaProdutosIds = listaProdutos.Select(p => p.Produto).ToList();
            List<Produto> produtos = await _context.produtos.Where(p => listaProdutosIds.Contains(p.Id)).ToListAsync();
            List<PedidoProduto> listPedidoProduto = new List<PedidoProduto>();
            decimal valorTotal = 0;
            foreach (Produto produto in produtos)
            {
                int quantidade = listaProdutos.Where(p => p.Produto == produto.Id).First().Quantidade;
                PedidoProduto pedidoProduto = new PedidoProduto();
                pedidoProduto.Produto = produto;
                pedidoProduto.IdPedido = pedido.Id;
                pedidoProduto.Quantidade = quantidade;
                pedidoProduto.ValorTotalLinha = produto.Valor * quantidade;
                listPedidoProduto.Add(pedidoProduto);
                valorTotal += pedidoProduto.ValorTotalLinha;
            }
            _context.pedidoProdutos.AddRange(listPedidoProduto);
            pedido.ValorTotal = valorTotal;
            pedido.Lista = listPedidoProduto;
            await _context.SaveChangesAsync();
            return listPedidoProduto;
        }



    }
}