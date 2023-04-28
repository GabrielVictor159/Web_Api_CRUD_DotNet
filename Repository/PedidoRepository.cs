using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_Api_CRUD.Infraestructure;
using Web_Api_CRUD.Model;
using Web_Api_CRUD.Model.DTO;

namespace Web_Api_CRUD.Repository
{
    public interface IPedidoRepository
    {
        Task<Guid> CreateAsync(PedidoDTO pedidoDto);
        Task<List<PedidoDTO>> GetAllPageAsync(int index, int size);
        Task<Pedido> GetPedidoByIdAsync(Guid id);
        Task UpdateAsync(Guid id, PedidoDTO pedidoDto);
        Task DeleteAsync(Guid id);
    }

    public class PedidoRepository : IPedidoRepository
    {
        private readonly ApplicationDbContext _context;

        public PedidoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateAsync(PedidoDTO pedidoDto)
        {
            var pedido = new Pedido
            {
                ValorTotal = pedidoDto.ValorTotal,
                Lista = pedidoDto.Lista
            };

            _context.pedidos.Add(pedido);
            await _context.SaveChangesAsync();

            return pedido.Id;
        }

        public async Task<List<PedidoDTO>> GetAllPageAsync(int index, int size)
        {
            var pedidos = await Task.FromResult(_context.pedidos
                .Skip((index - 1) * size)
                .Take(size)
                .ToList());

            return pedidos.Select(p => new PedidoDTO { ValorTotal = p.ValorTotal, Lista = p.Lista }).ToList();
        }

        public async Task<Pedido> GetPedidoByIdAsync(Guid id)
        {
            var pedido = await Task.FromResult(_context.pedidos.FirstOrDefault(p => p.Id == id));
            return pedido;
        }

        public async Task UpdateAsync(Guid id, PedidoDTO pedidoDto)
        {
            var pedido = await Task.FromResult(_context.pedidos.FirstOrDefault(p => p.Id == id));

            if (pedido == null)
            {
                throw new Exception($"Pedido com o ID {id} não encontrado");
            }

            pedido.ValorTotal = pedidoDto.ValorTotal;
            pedido.Lista = pedidoDto.Lista;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var pedido = await Task.FromResult(_context.pedidos.FirstOrDefault(p => p.Id == id));

            if (pedido == null)
            {
                throw new Exception($"Pedido com o ID {id} não encontrado");
            }

            _context.pedidos.Remove(pedido);
            await _context.SaveChangesAsync();
        }
    }
}