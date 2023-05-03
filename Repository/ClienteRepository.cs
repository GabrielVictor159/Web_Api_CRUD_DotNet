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
        Task<Cliente> GetClienteByIdAsync(Guid id);
        Task<ClienteDTO> UpdateAsync(Guid id, ClienteDTO clienteDto);
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
            if (clienteDto.Nome.Length < 8)
            {
                throw new ClienteRegisterException("Nome de usuario muito curto, por favor forneça um nome com pelo menos 8 digitos.");
            }
            if (clienteDto.Senha.Length < 8)
            {
                throw new ClienteRegisterException("Senha de usuario muito curta, por favor forneça uma senha com pelo menos 8 digitos.");
            }
            if (!Enum.IsDefined(typeof(Policies), clienteDto.Role))
            {
                throw new ClienteRegisterException("Role inválida, por favor verifique as Politicas de usuarios e adicione uma valida.");
            }

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

            return clientes.Select(c => new ClienteResponseDTO { Id = c.Id, Nome = c.Nome, Role = c.Role }).ToList();
        }


        public async Task<Cliente> GetClienteByIdAsync(Guid id)
        {
            var cliente = await Task.FromResult(_context.clientes.FirstOrDefault(c => c.Id == id));
            return cliente;
        }

        public async Task<ClienteDTO> UpdateAsync(Guid id, ClienteDTO clienteDto)
        {
            if (clienteDto.Nome.Length < 8)
            {
                throw new ClienteUpdateException("Nome de usuario muito curto, por favor forneça um nome com pelo menos 8 digitos.");
            }
            if (clienteDto.Senha.Length < 8)
            {
                throw new ClienteUpdateException("Senha de usuario muito curta, por favor forneça uma senha com pelo menos 8 digitos.");
            }
            if (!Enum.IsDefined(typeof(Policies), clienteDto.Role))
            {
                throw new ClienteUpdateException("Role inválida, por favor verifique as Politicas de usuarios e adicione uma valida.");
            }

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
            ClienteDTO response = _mapper.Map<ClienteDTO>(cliente);
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
    }
}