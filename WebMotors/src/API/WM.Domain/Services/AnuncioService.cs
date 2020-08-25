using FluentValidator;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WM.Domain.DTOs;
using WM.Domain.Entidades;
using WM.Domain.Interfaces;

namespace WM.Domain.Services
{
    public class AnuncioService : IAnuncioService
    {
        private readonly IAnuncioRepositorio _anuncioRepositorio;

        public AnuncioService(IAnuncioRepositorio anuncioRepositorio)
        {
            _anuncioRepositorio = anuncioRepositorio;
        }

        public async Task<AnuncioDTO> Inserir(AnuncioDTO model)
        {
            model.Validate();
            if (model.Invalid)
                return  await Task.FromResult(new AnuncioDTO());

            var anuncio = new Anuncio()
            {
                Marca = model.Marca,
                Modelo = model.Modelo,
                Versao = model.Versão,
                Ano = model.Ano,
                Quilometragem = model.Quilometragem,
                Observacao = model.Observacao
            };

            var result =  await _anuncioRepositorio.Inserir(anuncio);
            if (result > 0)
            {
                model.AnuncioId = result;
                return await Task.FromResult(model);
            }
            else
            {
                return await Task.FromResult(new AnuncioDTO());
            }
        }

        public async Task<AnuncioDTO> Alterar(AnuncioDTO model)
        {
            model.Validate();
            if (model.Invalid)
                return await Task.FromResult(new AnuncioDTO());

            var anuncio = new Anuncio()
            {
                Id = model.AnuncioId,
                Marca = model.Marca,
                Modelo = model.Modelo,
                Versao = model.Versão,
                Ano = model.Ano,
                Quilometragem = model.Quilometragem,
                Observacao = model.Observacao
            };

            await _anuncioRepositorio.Alterar(anuncio);

            return await Task.FromResult(model);
        }

        public Task<Anuncio> RetornarAnuncio(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Anuncio>> RetornarAnuncios()
        {
            throw new NotImplementedException();
        }

        

        public async Task Deletar(int id)
        {
            await _anuncioRepositorio.Deletar(id);
        }

    }
}
