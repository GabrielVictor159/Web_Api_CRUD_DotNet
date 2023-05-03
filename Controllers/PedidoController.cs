using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_Api_CRUD.Model;
using Web_Api_CRUD.Model.DTO;
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

        // [HttpPost]
        // [Authorize]
        // public async Task<ActionResult<dynamic>> AddPedido([FromBody] List<ProdutoQuantidadeDTO> produtosDto)
        // {
        //     try
        //     {
        //         Guid userId = Guid.Parse(HttpContext.User.FindFirstValue("Id"));
        //         Pedido pedido = _IPedidoService.
        //     }
        //     catch (Exception e)
        //     {
        //         return StatusCode(500, "Ocorreu um erro interno no servidor: " + e);
        //     }
        // }
    }
}