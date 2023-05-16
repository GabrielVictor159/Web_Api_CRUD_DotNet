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

namespace Web_Api_CRUD.Controllers
{
    [ApiController]
    [Route("Pedidos")]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _IPedidoService;
        private readonly IMapper _mapper;
        public PedidoController(IPedidoService iPedidoService, IMapper mapper)
        {
            _IPedidoService = iPedidoService;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Pedido>> AddPedido([FromBody] PedidoDTO pedidoDto)
        {
            try
            {
                var claimId = HttpContext.User.FindFirstValue("Id");
                if (claimId != null)
                {
                    Guid userId = Guid.Parse(claimId);
                    Object pedido = await _IPedidoService.CreateAsync(userId, pedidoDto);
                    if (pedido is string)
                    {
                        return BadRequest(pedido);
                    }
                    return Ok(pedido);
                }
                return BadRequest("O token de usuario é invalido.");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor: " + e.Message);
            }
        }

        [HttpPost]
        [Route("GetAllPage")]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult<Pedido>> GetAllPage([FromBody] PedidoConsultaDTO dto)
        {
            try
            {
                Object pedidos = await _IPedidoService.GetAllPage(dto);
                if (pedidos is string)
                {
                    return BadRequest(pedidos);
                }
                return Ok(pedidos);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor: " + e.Message);
            }
        }

        [HttpPost]
        [Route("GetOne")]
        [Authorize]
        public async Task<ActionResult<Pedido>> GetOne([FromBody] Guid id)
        {
            try
            {
                var claimUserId = HttpContext.User.FindFirstValue("Id");
                if (claimUserId == null)
                {
                    return BadRequest("O token de usuario é invalido.");
                }
                Guid userId = Guid.Parse(claimUserId);
                var claimRole = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (string.IsNullOrEmpty(claimRole))
                 {
                     return BadRequest("O token de usuario é invalido.");
                 }
                Object pedido = await _IPedidoService.GetPedidoByIdAsync(id);
                if (pedido is string)
                {
                    return BadRequest(pedido);
                }
                if (pedido is Pedido p)
                {
                    if (userId != p.idCliente && claimRole != Policies.ADMIN.ToString())
                    {
                        return Unauthorized("Você não tem autorização para buscar esse Pedido.");
                    }
                }
                return Ok(pedido);

            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor: " + e.Message);
            }
        }

        [HttpPut]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult<Pedido>> Update([FromBody] PedidoUpdateDTO dto)
        {
            try
            {
                Object pedido = await _IPedidoService.UpdatePedidoAsync(dto);
                if (pedido is string)
                {
                    return BadRequest(pedido);
                }
                return Ok(pedido);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor: " + e.Message);
            }
        }
        [HttpDelete]
        [Authorize]
        public async Task<ActionResult<String>> Delete([FromBody] Guid id)
        {
            try
            {
                var claimUserId = HttpContext.User.FindFirstValue("Id");
                if (claimUserId == null)
                {
                    return BadRequest("O token de usuario é invalido.");
                }
                Guid userId = Guid.Parse(claimUserId);
                var claimRole = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (string.IsNullOrEmpty(claimRole))
                 {
                     return BadRequest("O token de usuario é invalido.");
                 }
                Object pedido = await _IPedidoService.GetPedidoByIdAsync(id);
                if (pedido is string)
                {
                    return BadRequest(pedido);
                }
                if (pedido is Pedido p)
                {
                    if (userId != p.idCliente && claimRole != Policies.ADMIN.ToString())
                    {
                        return Unauthorized("Você não tem autorização para buscar esse Pedido.");
                    }
                }
                Object delete = await _IPedidoService.DeletePedidoAsync(id);
                if (delete is string)
                {
                    return BadRequest(delete);
                }
                return Ok("Pedido Deletado");

            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor: " + e.Message);
            }
        }


    }
}