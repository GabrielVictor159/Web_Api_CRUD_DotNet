using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_Api_CRUD.Exceptions;
using Web_Api_CRUD.Model;
using Web_Api_CRUD.Model.DTO;
using Web_Api_CRUD.Repository;
using Web_Api_CRUD.Services.Token;

namespace Web_Api_CRUD.Services
{
    public interface IClienteService
    {
        Task<Cliente> Login(String Nome, String Senha);
        Task<Cliente> Create(ClienteDTO clienteDTO);
        Task<List<ClienteResponseDTO>> getAllPage(int? index = 1, int? size = 10, string? nome = null, string? role = null, string Id = null);
        Task<Cliente> getById(Guid id);
        Task<ClienteDTO> Update(Guid id, ClienteDTO clienteDTO);
        Task<ClienteDTO> UpdateByUser(Guid id, ClienteDTO clienteDTO);
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
            return await _IClienteRepository.CreateAsync(clienteDTO);
        }

        public async Task<List<ClienteResponseDTO>> getAllPage(int? index = 1, int? size = 10, string? nome = null, string? role = null, string? Id = null)
        {
            return await _IClienteRepository.GetAllPageAsync((int)index, (int)size, nome, role, Id);
        }

        public async Task<Cliente> getById(Guid id)
        {
            return await _IClienteRepository.GetClienteByIdAsync(id);
        }

        public async Task<ClienteDTO> Update(Guid id, ClienteDTO clienteDTO)
        {
            try
            {
                return await _IClienteRepository.UpdateAsync(id, clienteDTO);

            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task<ClienteDTO> UpdateByUser(Guid id, ClienteDTO clienteDTO)
        {
            Cliente cliente = await _IClienteRepository.GetClienteByIdAsync(id);
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

        public async Task<Boolean> Delete(Guid id)
        {
            try
            {
                await _IClienteRepository.DeleteAsync(id);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        Task IClienteService.Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}