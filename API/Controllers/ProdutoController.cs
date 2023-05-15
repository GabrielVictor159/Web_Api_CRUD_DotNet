using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_Api_CRUD.Exceptions;
using Web_Api_CRUD.Domain;
using Web_Api_CRUD.Domain.DTO;
using Web_Api_CRUD.Services;
using API.Domain.DTO;

namespace Web_Api_CRUD.Controllers
{
    [ApiController]
    [Route("Produtos")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;
        private readonly IMapper _mapper;
        public ProdutoController(IProdutoService produtoService, IMapper mapper)
        {
            _produtoService = produtoService;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult<Produto>> CadastrarProduto([FromBody] ProdutoDTO dto)
        {
            try
            {
                Object produto = await _produtoService.CriarProdutoAsync(dto);
                if (produto is string)
                {
                    return BadRequest(produto);
                }
                return Ok(produto);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor: " + e.Message);
            }
        }
        [HttpPost]
        [Route("GetAllPage")]
        public async Task<ActionResult<List<Produto>>> getAllPage([FromBody] ProdutoConsultaDTO dto)
        {
            try
            {
                var dtoValidate = new ProdutoConsultaDTOValidation().Validate(dto);
                if (!dtoValidate.IsValid)
                {
                    return BadRequest(dtoValidate.ToString());
                }
                List<Produto> produtos = await _produtoService.ObterProdutosPaginadosAsync(dto.index, dto.size, dto.nome, dto.valorMinimo, dto.valorMaximo, dto.id);
                return Ok(produtos);
            }
            catch (Exception e)
            {
                return BadRequest("Operação invalida");
            }
        }
        [HttpPost]
        [Route("GetOne")]
        public async Task<ActionResult<Produto>> getOne([FromBody] Guid id)
        {
            try
            {
                Object produto = await _produtoService.ObterProdutoPorIdAsync(id);
                if (produto is string)
                {
                    return NotFound(produto);
                }
                return Ok(produto);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor: " + e.Message);
            }
        }
        [HttpPut]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult<Produto>> alterarProduto([FromBody] ProdutoAtualizarDTO dto)
        {
            try
            {
                var dtoValidate = new ProdutoAtualizarDTOValidation().Validate(dto);
                if (!dtoValidate.IsValid)
                {
                    return BadRequest(dtoValidate.ToString());
                }
                ProdutoDTO produtoDTO = _mapper.Map<ProdutoDTO>(dto);
                Object produto = await _produtoService.AtualizarProdutoAsync(dto.Id, produtoDTO);
                if (produto is string)
                {
                    return NotFound(produto);
                }
                return Ok(produto);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor: " + e.Message);
            }
        }

        [HttpDelete]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult<String>> deletarProduto([FromBody] Guid id)
        {
            try
            {
                Object delete = await _produtoService.ExcluirProdutoAsync(id);
                if (delete is string)
                {
                    return NotFound(delete);
                }
                return Ok("Produto Exlcuido");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor: " + e.Message);
            }
        }

    }
}