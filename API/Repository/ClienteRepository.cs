using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Web_Api_CRUD.Exceptions;
using Web_Api_CRUD.Infraestructure;
using Web_Api_CRUD.Model;
using Web_Api_CRUD.Model.Cryptography;
using Web_Api_CRUD.Model.DTO;
using Web_Api_CRUD.Model.Enums;

namespace Web_Api_CRUD.Repository
{
    public interface IClienteRepository
    {
        Task<Cliente> CreateAsync(ClienteDTO clienteDto);
        Task<Cliente> Login(String Nome, String Senha);
        Task<List<ClienteResponseDTO>> GetAllPageAsync(int index = 1, int size = 10, string nome = null, string role = null, string id = null);
        Task<ClienteResponseDTO> GetClienteByIdAsync(Guid id);
        Task<ClienteResponseDTO> UpdateAsync(Guid id, ClienteDTO clienteDto);
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

        public async Task<Cliente> CreateAsync(ClienteDTO clienteDto)
        {
            if (await _context.clientes.AnyAsync(c => c.Nome == clienteDto.Nome))
            {
                throw new ClienteRegisterException("Já existe um cliente com o mesmo nome.");
            }
            Cliente cliente = _mapper.Map<Cliente>(clienteDto);
            _context.clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }

        public async Task<Cliente> Login(String Nome, String Senha)
        {
            String senhaCriptografada = Cryptography.md5Hash(Senha);
            return await Task.Run(() => _context.clientes.Where(b => b.Nome == Nome && b.Senha == senhaCriptografada).FirstOrDefault());
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
                .Skip((index - 1) * size)
                .Take(size)
                .ToListAsync();
            foreach (var cliente in clientes)
            {
                cliente.pedidos = await GetPedidoProdutos(cliente.Id);
            }
            return clientes.Select(c => new ClienteResponseDTO { Id = c.Id, Nome = c.Nome, Role = c.Role, pedidos = c.pedidos }).ToList();
        }


        public async Task<ClienteResponseDTO> GetClienteByIdAsync(Guid id)
        {
            var cliente = await Task.FromResult(_context.clientes.FirstOrDefault(c => c.Id == id));
            cliente.pedidos = await GetPedidoProdutos(cliente.Id);
            ClienteResponseDTO dto = _mapper.Map<ClienteResponseDTO>(cliente);
            return dto;
        }

        public async Task<ClienteResponseDTO> UpdateAsync(Guid id, ClienteDTO clienteDto)
        {


            if (await _context.clientes.AnyAsync(c => c.Nome == clienteDto.Nome))
            {
                throw new ClienteUpdateException("Já existe um cliente com o mesmo nome.");
            }
            var cliente = await Task.FromResult(_context.clientes.FirstOrDefault(c => c.Id == id));

            if (cliente == null)
            {
                throw new ClienteUpdateException($"Cliente com o ID {id} não encontrado");
            }
            cliente = _mapper.Map<Cliente>(clienteDto);
            await _context.SaveChangesAsync();
            cliente.pedidos = await GetPedidoProdutos(cliente.Id);
            ClienteResponseDTO response = _mapper.Map<ClienteResponseDTO>(cliente);
            return response;
        }

        public async Task DeleteAsync(Guid id)
        {
            var cliente = await Task.FromResult(_context.clientes.FirstOrDefault(c => c.Id == id));

            if (cliente == null)
            {
                throw new ClienteDeleteException($"Cliente com o ID {id} não encontrado");
            }

            _context.clientes.Remove(cliente);
            await _context.SaveChangesAsync();
        }

        private async Task<List<Pedido>> GetPedidoProdutos(Guid idUsuario)
        {
            List<Pedido> pedidosProdutos = await _context.pedidos
                .Include(pp => pp.cliente)
                .Where(pp => pp.idCliente == idUsuario)
                .ToListAsync();
            return pedidosProdutos;
        }
    }
}