using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_Api_CRUD.Model;
using Web_Api_CRUD.Model.DTO;
using Web_Api_CRUD.Repository;
using Web_Api_CRUD.Services.Token;

namespace Web_Api_CRUD.Services
{
    public interface IClienteService
    {
        Task<String> Login(String Nome, String Senha);
        Task<Cliente> Create(ClienteDTO clienteDTO);
        Task<List<ClienteDTO>> getAllPage(int index, int size);
        Task<Cliente> getById(Guid id);
        Task<Boolean> Update(Guid id, ClienteDTO clienteDTO);
        Task<Boolean> Delete(Guid id);
    }
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _IClienteRepository;
        private readonly ITokenService _ITokenService;
        public ClienteService(IClienteRepository clienteRepository, ITokenService iTokenService)
        {
            _IClienteRepository = clienteRepository;
            _ITokenService = iTokenService;
        }

        public async Task<String> Login(String Nome, String Senha)
        {
            Cliente cliente = await _IClienteRepository.Login(Nome, Senha);
            if (cliente == null)
            {
                return null;
            }
            else
            {
                return _ITokenService.GenerateToken(cliente);
            }
        }

        public async Task<Cliente> Create(ClienteDTO clienteDTO)
        {
            return await _IClienteRepository.CreateAsync(clienteDTO);
        }

        public async Task<List<ClienteDTO>> getAllPage(int index, int size)
        {
            return await _IClienteRepository.GetAllPageAsync(index, size);
        }

        public async Task<Cliente> getById(Guid id)
        {
            return await _IClienteRepository.GetClienteByIdAsync(id);
        }

        public async Task<Boolean> Update(Guid id, ClienteDTO clienteDTO)
        {
            try
            {
                await _IClienteRepository.UpdateAsync(id, clienteDTO);
                return true;
            }
            catch (Exception e)
            {
                return false;
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

    }
}