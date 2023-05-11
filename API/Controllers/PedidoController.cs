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
        public async Task<ActionResult<Pedido>> AddPedido([FromBody] List<ProdutoQuantidadeDTO> produtosDto)
        {
            try
            {
                Guid userId = Guid.Parse(HttpContext.User.FindFirstValue("Id"));
                Pedido pedido = await _IPedidoService.CriarPedidoAsync(userId, produtosDto);
                return Ok(pedido);
            }
            catch (ClienteGetException e)
            {
                return BadRequest(e.Message);
            }
            catch (PedidoProdutoInvalidProducts e)
            {
                return BadRequest(e.Message);
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
                List<Pedido> pedidos = await _IPedidoService.GetAllPage(dto);
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
                Guid userId = Guid.Parse(HttpContext.User.FindFirstValue("Id"));
                String Role = HttpContext.User.FindFirstValue("Role");
                Pedido pedido = await _IPedidoService.GetPedidoByIdAsync(id);
                if (userId != pedido.idCliente && Role != Policies.ADMIN.ToString())
                {
                    return Unauthorized("Você não tem autorização para buscar esse Pedido.");
                }
                return Ok(pedido);

            }
            catch (PedidoConsultaException e)
            {
                return BadRequest(e.Message);
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
                Pedido pedido = await _IPedidoService.UpdatePedidoAsync(dto);
                return Ok(pedido);
            }
            catch (PedidoProdutoInvalidProducts e)
            {
                return BadRequest(e.Message);
            }
             catch (PedidoConsultaException e)
            {
                return BadRequest(e.Message);
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
                Guid userId = Guid.Parse(HttpContext.User.FindFirstValue("Id"));
                String Role = HttpContext.User.FindFirstValue("Role");
                Pedido pedido = await _IPedidoService.GetPedidoByIdAsync(id);
                if (userId != pedido.idCliente && Role != Policies.ADMIN.ToString())
                {
                    return Unauthorized("Você não tem autorização para buscar esse Pedido.");
                }
                await _IPedidoService.DeletePedidoAsync(id);
                return Ok("Pedido Deletado");

            }
            catch (PedidoConsultaException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor: " + e.Message);
            }
        }


    }
}