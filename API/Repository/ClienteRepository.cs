using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Web_Api_CRUD.Exceptions;
using Web_Api_CRUD.Infraestructure;
using Web_Api_CRUD.Domain;
using Web_Api_CRUD.Domain.Cryptography;
using Web_Api_CRUD.Domain.DTO;
using Web_Api_CRUD.Domain.Enums;

namespace Web_Api_CRUD.Repository
{
    public interface IClienteRepository
    {
        Task<Cliente> CreateAsync(Cliente cliente);
        Task<List<Cliente>> GetAllByNameAsync(String Name);
        Task<Cliente> Login(String Nome, String Senha);
        Task<List<ClienteResponseDTO>> GetAllPageAsync(int index = 1, int size = 10, string nome = null, string role = null, string id = null);
        Task<ClienteResponseDTO> GetClienteByIdAsync(Guid id);
        Task<ClienteResponseDTO> UpdateAsync(Guid id, ClienteDTO clienteDto);
        Task<List<Pedido>> GetPedidos(Guid idUsuario);
        Task<Boolean> DeleteAsync(Guid id);
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

        public async Task<Cliente> CreateAsync(Cliente cliente)
        {
            _context.clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }
        public async Task<List<Cliente>> GetAllByNameAsync(String Name)
        {
            return await _context.clientes.Where(a => a.Nome == Name).ToListAsync();
        }

        public async Task<Cliente?> Login(String Nome, String Senha)
        {
            var user = await Task.FromResult(_context.clientes.Where(b => b.Nome == Nome && b.Senha == Senha).FirstOrDefault());
            return user;
        }
        public async Task<List<ClienteResponseDTO>> GetAllPageAsync(int index = 1, int size = 10, string nome = null, string role = null, string id = null)
        {
            var query = _context.clientes.AsQueryable();

              if (!string.IsNullOrEmpty(id))
            {
                query = query.Where(c => c.Id.ToString().ToLower().Contains(id.ToLower()));
            }

            if (!string.IsNullOrEmpty(nome))
            {
                query = query.Where(c => c.Nome.ToLower().Contains(nome.ToLower()));
            }

            if (!string.IsNullOrEmpty(role))
            {
                query = query.Where(c => c.Role.ToLower() == role.ToLower());
            }

            var clientes = await query
                .Skip(((index - 1) * size))
                .Take(size)
                .ToListAsync();

            return clientes.Select(c => new ClienteResponseDTO { Id = c.Id, Nome = c.Nome, Role = c.Role}).ToList();
        }


        public async Task<ClienteResponseDTO?> GetClienteByIdAsync(Guid id)
        {
            var cliente = await Task.FromResult(_context.clientes.FirstOrDefault(c => c.Id == id));
            if (cliente == null)
            {
                return null;
            }
            ClienteResponseDTO dto = _mapper.Map<ClienteResponseDTO>(cliente);
            return dto;
        }

        public async Task<ClienteResponseDTO?> UpdateAsync(Guid id, ClienteDTO clienteDto)
        {
            var cliente = await Task.FromResult(_context.clientes.FirstOrDefault(c => c.Id == id));
            if(cliente !=null)
            {
            cliente = _mapper.Map<Cliente>(clienteDto);
            cliente.Id = id;
            await _context.SaveChangesAsync();
            cliente.pedidos = await GetPedidos(cliente.Id);
            ClienteResponseDTO response = _mapper.Map<ClienteResponseDTO>(cliente);
            return response;
            }
            else
            {
                return null;
            }
        }

        public async Task<Boolean> DeleteAsync(Guid id)
        {
            var cliente = await Task.FromResult(_context.clientes.FirstOrDefault(c => c.Id == id));
            if (cliente == null)
            {
                return false;
            }
            _context.clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<List<Pedido>> GetPedidos(Guid idUsuario)
        {
            List<Pedido> pedidos = await _context.pedidos
                .Include(pp => pp.cliente)
                .Where(pp => pp.idCliente == idUsuario)
                .ToListAsync();
            return pedidos;
        }
    }
}