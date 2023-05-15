using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_Api_CRUD.Exceptions;
using Web_Api_CRUD.Domain;
using Web_Api_CRUD.Domain.DTO;
using Web_Api_CRUD.Domain.Enums;
using Web_Api_CRUD.Services;
using Web_Api_CRUD.Services.Token;
using API.Domain.DTO;

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
        public async Task<ActionResult<String>> Login([FromBody] LoginDTO dto)
        {
            var token = await _clienteService.Login(dto.Nome, dto.Senha);
            if (token == null)
            {
                return NotFound("Não foi possivel encontrar o usuario.");
            }
            else
            {
                return Ok(token);
            }
        }
        [HttpPost]
        public async Task<ActionResult<Cliente>> AddCliente([FromBody] LoginDTO DTO)
        {
            try
            {
                ClienteDTO clienteDto = _mapper.Map<ClienteDTO>(DTO);
                clienteDto.Role = Policies.USER.ToString();
                Object cadastro = await _clienteService.Create(clienteDto);
                if (cadastro is string)
                {
                    return BadRequest(cadastro);
                }
                return Ok(cadastro);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor: " + e.Message);
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
                Object cadastro = await _clienteService.Create(clienteDto);
                if (cadastro is string)
                {
                    return BadRequest(cadastro);
                }
                return Ok(cadastro);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor: " + e.Message);
            }

        }

        [HttpPost]
        [Route("GetAllPage")]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult<List<ClienteResponseDTO>>> getAllPage([FromBody] ClientePaginationDTO dto)
        {
            try
            {
                var dtoValidation = new ClientePaginationDTOValidation().Validate(dto);
                if (!dtoValidation.IsValid)
                {
                    return BadRequest(dtoValidation.ToString());
                }
                return Ok(await _clienteService.getAllPage(dto.Index, dto.Size, dto.Nome, dto.Role, dto.Id));
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor: " + e.Message);
            }
        }

        [HttpPost]
        [Route("GetOne")]
        public async Task<ActionResult<ClienteResponseDTO>> getOne([FromBody] Guid id)
        {
            try
            {
                Object cliente = await _clienteService.getById(id);
                if (cliente is string)
                {
                    return BadRequest(cliente);
                }

                return Ok(cliente);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor: " + e.Message);
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<Cliente>> updateByUser([FromBody] ClienteDTO dto)
        {
            try
            {
                Guid userId = Guid.Parse(HttpContext.User.FindFirstValue("Id"));
                Object cliente = await _clienteService.UpdateByUser(userId, dto);
                if (cliente is string)
                {
                    return BadRequest(cliente);
                }
                return Ok(cliente);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor: " + e.Message);
            }
        }

        [HttpPut]
        [Route("UpdateByAdmin")]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult<Cliente>> updateByAdmin([FromBody] ClienteUpdateDTO dto)
        {
            try
            {
                ClienteDTO clienteDto = _mapper.Map<ClienteDTO>(dto);
                Object cliente = await _clienteService.Update(dto.Id, clienteDto);
                Console.WriteLine(cliente);
                if (cliente is string)
                {
                    return BadRequest(cliente);
                }
                return Ok(cliente);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor: " + e.Message);
            }
        }
        [HttpDelete]
        [Authorize]
        public async Task<ActionResult<String>> deleteByUser()
        {
            try
            {
                Guid userId = Guid.Parse(HttpContext.User.FindFirstValue("Id"));
                Boolean result = await _clienteService.Delete(userId);
                if (result)
                {
                    return Ok("Usuario deletado");
                }
                return BadRequest("Id invalido");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor: " + e.Message);
            }
        }
        [HttpDelete]
        [Route("DeleteByAdmin")]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult<String>> deleteByUserAdmin([FromBody] Guid id)
        {
            try
            {
                Boolean delete = await _clienteService.Delete(id);
                if (delete)
                {
                    return Ok("Usuario deletado");
                }
                return BadRequest("Id invalido");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor: " + e);
            }
        }

    }
}
