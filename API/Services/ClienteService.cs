using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_Api_CRUD.Exceptions;
using Web_Api_CRUD.Model;
using Web_Api_CRUD.Model.DTO;
using Web_Api_CRUD.Model.Enums;
using Web_Api_CRUD.Repository;
using Web_Api_CRUD.Services.Token;

namespace Web_Api_CRUD.Services
{
    public interface IClienteService
    {
        Task<Cliente> Login(String Nome, String Senha);
        Task<Cliente> Create(ClienteDTO clienteDTO);
        Task<List<ClienteResponseDTO>> getAllPage(int? index = 1, int? size = 10, string? nome = null, string? role = null, string Id = null);
        Task<ClienteResponseDTO> getById(Guid id);
        Task<ClienteResponseDTO> Update(Guid id, ClienteDTO clienteDTO);
        Task<ClienteResponseDTO> UpdateByUser(Guid id, ClienteDTO clienteDTO);
        Task Delete(Guid id);
    }
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _IClienteRepository;
        public ClienteService(IClienteRepository clienteRepository)
        {
            _IClienteRepository = clienteRepository;
        }

        public async Task<Cliente> Login(String Nome, String Senha)
        {
            return await _IClienteRepository.Login(Nome, Senha);

        }

        public async Task<Cliente> Create(ClienteDTO clienteDTO)
        {
            if (clienteDTO.Nome.Length < 8)
            {
                throw new ClienteRegisterException("Nome de usuario muito curto, por favor forneça um nome com pelo menos 8 digitos.");
            }
            if (clienteDTO.Senha.Length < 8)
            {
                throw new ClienteRegisterException("Senha de usuario muito curta, por favor forneça uma senha com pelo menos 8 digitos.");
            }
            if (!Enum.IsDefined(typeof(Policies), clienteDTO.Role))
            {
                throw new ClienteRegisterException("Role inválida, por favor verifique as Politicas de usuarios e adicione uma valida.");
            }


            return await _IClienteRepository.CreateAsync(clienteDTO);
        }

        public async Task<List<ClienteResponseDTO>> getAllPage(int? index = 1, int? size = 10, string? nome = null, string? role = null, string? Id = null)
        {
            return await _IClienteRepository.GetAllPageAsync((int)index, (int)size, nome, role, Id);
        }

        public async Task<ClienteResponseDTO> getById(Guid id)
        {
            return await _IClienteRepository.GetClienteByIdAsync(id);
        }

        public async Task<ClienteResponseDTO> Update(Guid id, ClienteDTO clienteDTO)
        {
            if (clienteDTO.Nome.Length < 8)
            {
                throw new ClienteUpdateException("Nome de usuario muito curto, por favor forneça um nome com pelo menos 8 digitos.");
            }
            if (clienteDTO.Senha.Length < 8)
            {
                throw new ClienteUpdateException("Senha de usuario muito curta, por favor forneça uma senha com pelo menos 8 digitos.");
            }
            if (!Enum.IsDefined(typeof(Policies), clienteDTO.Role))
            {
                throw new ClienteUpdateException("Role inválida, por favor verifique as Politicas de usuarios e adicione uma valida.");
            }

            try
            {
                return await _IClienteRepository.UpdateAsync(id, clienteDTO);

            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task<ClienteResponseDTO> UpdateByUser(Guid id, ClienteDTO clienteDTO)
        {
            if (clienteDTO.Nome.Length < 8)
            {
                throw new ClienteUpdateException("Nome de usuario muito curto, por favor forneça um nome com pelo menos 8 digitos.");
            }
            if (clienteDTO.Senha.Length < 8)
            {
                throw new ClienteUpdateException("Senha de usuario muito curta, por favor forneça uma senha com pelo menos 8 digitos.");
            }
            if (!Enum.IsDefined(typeof(Policies), clienteDTO.Role))
            {
                throw new ClienteUpdateException("Role inválida, por favor verifique as Politicas de usuarios e adicione uma valida.");
            }
            ClienteResponseDTO cliente = await _IClienteRepository.GetClienteByIdAsync(id);
            if (clienteDTO.Role != cliente.Role)
            {
                throw new ClienteUpdateException("Operação Invalida você não pode alterar o seu nivel de usuario.");
            }
            try
            {
                return await _IClienteRepository.UpdateAsync(id, clienteDTO);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task Delete(Guid id)
        {

            await _IClienteRepository.DeleteAsync(id);
        }


    }
}