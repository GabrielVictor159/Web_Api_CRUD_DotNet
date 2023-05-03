using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_Api_CRUD.Exceptions;
using Web_Api_CRUD.Model;
using Web_Api_CRUD.Model.DTO;
using Web_Api_CRUD.Model.Enums;
using Web_Api_CRUD.Services;
using Web_Api_CRUD.Services.Token;

namespace Web_Api_CRUD.Controllers
{
    [ApiController]
    [Route("Clientes")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        private readonly IMapper _mapper;
        public ClienteController(IClienteService iClienteService, IMapper mapper)
        {
            _clienteService = iClienteService;
            _mapper = mapper;
        }
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<dynamic>> Login([FromBody] LoginDTO dto)
        {
            Cliente cliente = await _clienteService.Login(dto.Nome, dto.Senha);
            if (cliente == null)
            {
                return NotFound();
            }
            else
            {
                var Token = TokenService.GenerateToken(cliente);
                return Ok(
                    new
                    {
                        User = cliente,
                        Token = Token
                    }
                );
            }
        }
        [HttpPost]
        public async Task<ActionResult<Cliente>> AddCliente([FromBody] LoginDTO DTO)
        {
            try
            {
                ClienteDTO clienteDto = _mapper.Map<ClienteDTO>(DTO);
                clienteDto.Role = Policies.USER.ToString();
                Cliente cadastro = await _clienteService.Create(clienteDto);
                return Ok(cadastro);
            }
            catch (ClienteRegisterException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor: " + e);
            }

        }

        [HttpPost]
        [Route("RegisterAdmin")]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult<Cliente>> AddAdmin([FromBody] LoginDTO DTO)
        {
            try
            {
                ClienteDTO clienteDto = _mapper.Map<ClienteDTO>(DTO);
                clienteDto.Role = Policies.ADMIN.ToString();
                Cliente cadastro = await _clienteService.Create(clienteDto);
                return Ok(cadastro);
            }
            catch (ClienteRegisterException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor: " + e);
            }

        }

        [HttpPost]
        [Route("GetAllPage")]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult<dynamic>> getAllPage([FromBody] ClientePaginationDTO dto)
        {
            try
            {
                return Ok(await _clienteService.getAllPage(dto.Index, dto.Size, dto.Nome, dto.Role, dto.Id));
            }
            catch (Exception e)
            {
                return BadRequest("Operação invalida");
            }
        }
        [HttpPost]
        [Route("GetOne")]
        public async Task<ActionResult<dynamic>> getOne([FromBody] Guid id)
        {
            try
            {
                return Ok(await _clienteService.getById(id));
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor: " + e);
            }
        }
        [HttpPut]
        [Authorize]
        public async Task<ActionResult<dynamic>> updateByUser([FromBody] ClienteDTO dto)
        {

            try
            {
                Guid userId = Guid.Parse(HttpContext.User.FindFirstValue("Id"));
                ClienteDTO cliente = await _clienteService.UpdateByUser(userId, dto);
                return Ok(cliente);
            }
            catch (ClienteUpdateException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor: " + e);
            }
        }
        [HttpPut]
        [Route("UpdateByAdmin")]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult<dynamic>> updateByAdmin([FromBody] ClienteUpdateDTO dto)
        {
            try
            {
                ClienteDTO clienteDto = _mapper.Map<ClienteDTO>(dto);
                ClienteDTO cliente = await _clienteService.Update(dto.Id, clienteDto);
                return Ok(cliente);
            }
            catch (ClienteUpdateException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor: " + e);
            }
        }
        [HttpDelete]
        [Authorize]
        public async Task<ActionResult<dynamic>> deleteByUser()
        {
            try
            {
                Guid userId = Guid.Parse(HttpContext.User.FindFirstValue("Id"));
                _clienteService.Delete(userId);
                return Ok("Usuario deletado");
            }
            catch (ClienteDeleteException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor: " + e);
            }
        }
        [HttpDelete]
        [Route("DeleteByAdmin")]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult<dynamic>> deleteByUser([FromBody] Guid id)
        {
            try
            {
                _clienteService.Delete(id);
                return Ok("Usuario deletado");
            }
            catch (ClienteDeleteException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor: " + e);
            }
        }

    }
}
