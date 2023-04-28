using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Web_Api_CRUD.Infraestructure;
using Web_Api_CRUD.Model;
using Web_Api_CRUD.Model.DTO;

namespace Web_Api_CRUD.Repository
{
    public interface IPedidoRepository
    {
        Task<Guid> CreateAsync(PedidoDTO pedidoDto);
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

        public async Task<Guid> CreateAsync(PedidoDTO pedidoDto)
        {
            Pedido pedido = _mapper.Map<Pedido>(pedidoDto);
            _context.pedidos.Add(pedido);
            await _context.SaveChangesAsync();
            return pedido.Id;
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
            var pedido = await Task.FromResult(_context.pedidos.FirstOrDefault(p => p.Id == id));

            if (pedido == null)
            {
                throw new Exception($"Pedido com o ID {id} não encontrado");
            }
            pedido = _mapper.Map<Pedido>(pedidoDto);
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