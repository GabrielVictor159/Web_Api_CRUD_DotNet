using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Web_Api_CRUD.Infraestructure;
using Web_Api_CRUD.Model;
using Web_Api_CRUD.Model.DTO;
using Microsoft.EntityFrameworkCore;

namespace Web_Api_CRUD.Repository
{
    public interface IPedidoRepository
    {
        Task<Pedido> CreateAsync(PedidoDTO pedidoDto);
        Task<List<Pedido>> GetAllPageAsync(int index, int size);
        Task<Pedido> GetPedidoByIdAsync(Guid id);
        Task UpdateAsync(Guid id, PedidoDTO pedidoDto);
        Task DeleteAsync(Guid id);
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

        public async Task<Pedido> CreateAsync(PedidoDTO pedidoDto)
        {
            Pedido pedido = _mapper.Map<Pedido>(pedidoDto);
            List<Produto> produtos = _context.produtos.Where(p => pedidoDto.Lista.Contains(p.Id)).ToList();
            if (produtos.Count == 0)
            {
                throw new Exception($"Por favor, informe IDs válidos para a lista de produtos.");
            }
            decimal valorTotal = produtos.Sum(p => p.Valor);
            pedido.ValorTotal = valorTotal;
            var gruposDeProdutos = pedidoDto.Lista.GroupBy(id => id).Select(g => new { Id = g.Key, Quantidade = g.Count() });
            foreach (var grupo in gruposDeProdutos)
            {
                var produto = produtos.FirstOrDefault(p => p.Id == grupo.Id);

                if (produto == null)
                {
                    throw new Exception($"Produto com o ID {grupo.Id} não encontrado");
                }
                PedidoProduto pedidoProduto = new PedidoProduto
                {
                    Quantidade = grupo.Quantidade,
                    ValorTotalLinha = produto.Valor * grupo.Quantidade,
                    produto = produto,
                    pedido = pedido
                };

                _context.pedidoProdutos.Add(pedidoProduto);
            }

            await _context.SaveChangesAsync();

            return pedido;
        }



        public async Task<List<Pedido>> GetAllPageAsync(int index, int size)
        {
            List<Pedido> pedidos = await Task.FromResult(_context.pedidos
                .Skip((index - 1) * size)
                .Take(size)
                .ToList());
            return pedidos;
        }

        public async Task<Pedido> GetPedidoByIdAsync(Guid id)
        {
            var pedido = await Task.FromResult(_context.pedidos.FirstOrDefault(p => p.Id == id));
            return pedido;
        }

        public async Task UpdateAsync(Guid id, PedidoDTO pedidoDto)
        {
            var pedido = await _context.pedidos
                .Include(p => p.pedidoProduto)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedido == null)
            {
                throw new Exception($"Pedido com o ID {id} não encontrado");
            }
            _context.pedidoProdutos.RemoveRange(pedido.pedidoProduto);
            _mapper.Map(pedidoDto, pedido);
            List<Produto> produtos = _context.produtos.Where(p => pedidoDto.Lista.Contains(p.Id)).ToList();
            if (produtos.Count == 0)
            {
                throw new Exception($"Por favor, informe IDs válidos para a lista de produtos.");
            }
            decimal valorTotal = produtos.Sum(p => p.Valor);
            pedido.ValorTotal = valorTotal;
            var gruposDeProdutos = pedidoDto.Lista.GroupBy(id => id).Select(g => new { Id = g.Key, Quantidade = g.Count() });
            foreach (var grupo in gruposDeProdutos)
            {
                var produto = produtos.FirstOrDefault(p => p.Id == grupo.Id);

                if (produto == null)
                {
                    throw new Exception($"Produto com o ID {grupo.Id} não encontrado");
                }

                PedidoProduto pedidoProduto = new PedidoProduto
                {
                    Quantidade = grupo.Quantidade,
                    ValorTotalLinha = produto.Valor * grupo.Quantidade,
                    produto = produto,
                    pedido = pedido
                };

                _context.pedidoProdutos.Add(pedidoProduto);
            }

            await _context.SaveChangesAsync();
        }


        public async Task DeleteAsync(Guid id)
        {
            var pedido = await _context.pedidos.Include(p => p.pedidoProduto)
                                                 .FirstOrDefaultAsync(p => p.Id == id);
            if (pedido == null)
            {
                throw new Exception($"Pedido com o ID {id} não encontrado");
            }

            _context.pedidoProdutos.RemoveRange(pedido.pedidoProduto);
            _context.pedidos.Remove(pedido);
            await _context.SaveChangesAsync();
        }

    }
}