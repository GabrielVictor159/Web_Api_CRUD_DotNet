using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_Api_CRUD.Exceptions;
using Web_Api_CRUD.Domain;
using Web_Api_CRUD.Domain.DTO;
using Web_Api_CRUD.Domain.Enums;
using Web_Api_CRUD.Repository;
using Web_Api_CRUD.Services.Token;
using Web_Api_CRUD.Domain.Cryptography;
using API.Domain.DTO;
using AutoMapper;

namespace Web_Api_CRUD.Services
{
    public interface IClienteService
    {

        Task<String?> Login(String Nome, String Senha);
        Task<Object> Create(ClienteDTO clienteDTO);
        Task<List<ClienteResponseDTO>> getAllPage(int? index = 1, int? size = 10, string? nome = null, string? role = null, string Id = null);
        Task<Object> getById(Guid id);
        Task<Object> Update(Guid id, ClienteDTO clienteDTO);
        Task<Object> UpdateByUser(Guid id, ClienteDTO clienteDTO);
        Task<Boolean> Delete(Guid id);
    }
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _IClienteRepository;
        private readonly IMapper _mapper;
        public ClienteService(IClienteRepository clienteRepository, IMapper mapper)
        {
            _IClienteRepository = clienteRepository;
            _mapper = mapper;
        }

        public async Task<String?> Login(String Nome, String Senha)
        {
            String senhaCriptografada = Cryptography.md5Hash(Senha);
            var login = await _IClienteRepository.Login(Nome, senhaCriptografada);
            if (login != null)
            {
                return TokenService.GenerateToken(login);
            }
            else
            {
                return null;
            }
        }

        public async Task<Object> Create(ClienteDTO clienteDTO)
        {
            var dtoValidation = new ClienteDTOValidation().Validate(clienteDTO);
            if (dtoValidation.IsValid)
            {
                List<Cliente> clientes = await _IClienteRepository.GetAllByNameAsync(clienteDTO.Nome);
                if (clientes.Count > 0)
                {
                    return "Existem usuarios com o mesmo nome";
                }
                Cliente cliente = _mapper.Map<Cliente>(clienteDTO);
                return await _IClienteRepository.CreateAsync(cliente);
            }
            return dtoValidation.ToString();
        }

        public async Task<List<ClienteResponseDTO>> getAllPage(int? index = 1, int? size = 10, string? nome = "", string? role = "", string? Id = "")
        {
            List<ClienteResponseDTO> clientes = await _IClienteRepository.GetAllPageAsync((int)index, (int)size, nome, role, Id);
            foreach (ClienteResponseDTO cliente in clientes)
            {
                  cliente.pedidos = await _IClienteRepository.GetPedidos(cliente.Id);   
            }
            return clientes;
        }

        public async Task<Object> getById(Guid id)
        {
            var cliente = await _IClienteRepository.GetClienteByIdAsync(id);
            if (cliente == null)
            {
                return "Usuario não encontrado";
            }
            cliente.pedidos = await _IClienteRepository.GetPedidos(cliente.Id);
            return cliente;
        }
        public async Task<Object> Update(Guid id, ClienteDTO clienteDTO)
        {
            var dtoValidate = new ClienteDTOValidation().Validate(clienteDTO);
            if (!dtoValidate.IsValid)
            {
                return dtoValidate.ToString();
            }
            var cliente = await _IClienteRepository.GetClienteByIdAsync(id);
            if (cliente == null)
            {
                return "Usuario não encontrado";
            }
            return await _IClienteRepository.UpdateAsync(id, clienteDTO);
        }
        public async Task<Object> UpdateByUser(Guid id, ClienteDTO clienteDTO)
        {
            var dtoValidate = new ClienteDTOValidation().Validate(clienteDTO);
            if (!dtoValidate.IsValid)
            {
                return dtoValidate.ToString();
            }
            var cliente = await _IClienteRepository.GetClienteByIdAsync(id);
            if (cliente == null)
            {
                return "Usuario não encontrado";
            }
            if (clienteDTO.Role != cliente.Role)
            {
                return "Operação Invalida você não pode alterar o seu nivel de usuario.";
            }
            return await _IClienteRepository.UpdateAsync(id, clienteDTO);
        }

        public async Task<Boolean> Delete(Guid id)
        {
            return await _IClienteRepository.DeleteAsync(id);
        }


    }
}