using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WM.Domain.DTOs;
using WM.Domain.Interfaces;

namespace WM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnuncioController : ControllerBase
    {
        private readonly IAnuncioService _anuncioService;
        private readonly IAnuncioRepositorio _anuncioRepositorio;

        public AnuncioController(IAnuncioService anuncioService, IAnuncioRepositorio anuncioRepositorio)
        {
            _anuncioService = anuncioService;
            _anuncioRepositorio = anuncioRepositorio;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAnuncios()
        {
            var result = await _anuncioRepositorio.RetornarAnuncios();
            if (result.Count() <= 0)
                return NotFound();

            var lstAnuncios = new List<AnuncioDTO>();

            foreach (var item in result)
            {
                var anuncio = new AnuncioDTO()
                {
                    AnuncioId = item.Id,
                    Marca = item.Marca,
                    Modelo = item.Modelo,
                    Versão = item.Versao,
                    Ano = item.Ano,
                    Quilometragem = item.Quilometragem,
                    Observacao = item.Observacao
                };

                lstAnuncios.Add(anuncio);
            }
          
            return Ok(lstAnuncios);

        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAnuncio(int id)
        {
            var result = await _anuncioRepositorio.RetornarAnuncio(id);
            if (result == null)
                return NotFound();

            var anuncioDTO = new AnuncioDTO()
            {
                AnuncioId = id,
                Marca = result.Marca,
                Modelo = result.Modelo,
                Versão = result.Versao,
                Ano = result.Ano,
                Quilometragem = result.Quilometragem,
                Observacao = result.Observacao
            };

            return Ok(anuncioDTO);

        }


        [HttpPost]
        [Route("criar")]
        public async Task<IActionResult> Create([FromBody] AnuncioDTO command)
        {
            try
            {
                var result = await _anuncioService.Inserir(command);

                if (command.Notifications.Any())
                    return BadRequest(command.Notifications);

                return Ok(result);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPut]
        [Route("atualizar/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AnuncioDTO command)
        {
            try
            {
                if (id != command.AnuncioId)
                    return BadRequest();

                var result = await _anuncioService.Alterar(command);

                if (command.Notifications.Any())
                    return BadRequest(command.Notifications);

                return Ok(result);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpDelete]
        [Route("deletar/{id}")]
        public async Task<IActionResult> Deletar(int id, [FromBody] AnuncioDTO command)
        {
            try
            {
                if (id != command.AnuncioId)
                    return BadRequest();

                var result = await _anuncioRepositorio.RetornarAnuncio(id);
                if (result == null)
                    return NotFound();

                await _anuncioService.Deletar(id);

                return Ok();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}