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
    public interface IClienteRepository
    {
        Task<Guid> CreateAsync(ClienteDTO clienteDto);
        Task<List<ClienteDTO>> GetAllPageAsync(int index, int size);
        Task<Cliente> GetClienteByIdAsync(Guid id);
        Task UpdateAsync(Guid id, ClienteDTO clienteDto);
        Task DeleteAsync(Guid id);
    }
    public class ClienteRepository : IClienteRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ClienteRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Guid> CreateAsync(ClienteDTO clienteDto)
        {
            Cliente cliente = _mapper.Map<Cliente>(clienteDto);
            _context.clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return cliente.Id;
        }

        public async Task<List<ClienteDTO>> GetAllPageAsync(int index, int size)
        {
            var clientes = await Task.FromResult(_context.clientes
                .Skip((index - 1) * size)
                .Take(size)
                .ToList());

            return clientes.Select(c => new ClienteDTO { Nome = c.Nome }).ToList();
        }

        public async Task<Cliente> GetClienteByIdAsync(Guid id)
        {
            var cliente = await Task.FromResult(_context.clientes.FirstOrDefault(c => c.Id == id));
            return cliente;
        }

        public async Task UpdateAsync(Guid id, ClienteDTO clienteDto)
        {
            var cliente = await Task.FromResult(_context.clientes.FirstOrDefault(c => c.Id == id));

            if (cliente == null)
            {
                throw new Exception($"Cliente com o ID {id} não encontrado");
            }
            cliente = _mapper.Map<Cliente>(clienteDto);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var cliente = await Task.FromResult(_context.clientes.FirstOrDefault(c => c.Id == id));

            if (cliente == null)
            {
                throw new Exception($"Cliente com o ID {id} não encontrado");
            }

            _context.clientes.Remove(cliente);
            await _context.SaveChangesAsync();
        }
    }
}