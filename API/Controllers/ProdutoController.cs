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
                Produto produto = await _produtoService.CriarProdutoAsync(dto);
                return Ok(produto);
            }
            catch (ProdutoRegisterException e)
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
        public async Task<ActionResult<List<Produto>>> getAllPage([FromBody] ProdutoConsultaDTO dto)
        {
            try
            {
                List<Produto> produtos = await _produtoService.ObterProdutosPaginadosAsync(dto.index, dto.size, dto.nome, dto.valorMinimo, dto.valorMaximo, dto.id);
                return Ok(produtos);
            }
            catch (ProdutoConsultaException e)
            {
                return BadRequest(e.Message);
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
                Produto produto = await _produtoService.ObterProdutoPorIdAsync(id);
                return Ok(produto);
            }
            catch (ProdutoConsultaException e)
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
        public async Task<ActionResult<Produto>> alterarProduto([FromBody] ProdutoAtualizarDTO dto)
        {
            try
            {
                ProdutoDTO produtoDTO = _mapper.Map<ProdutoDTO>(dto);
                Produto produto = await _produtoService.AtualizarProdutoAsync(dto.Id, produtoDTO);
                return Ok(produto);
            }
            catch (ProdutoConsultaException e)
            {
                return BadRequest(e.Message);
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
                await _produtoService.ExcluirProdutoAsync(id);
                return Ok("Produto Exlcuido");
            }
            catch (ProdutoConsultaException e)
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