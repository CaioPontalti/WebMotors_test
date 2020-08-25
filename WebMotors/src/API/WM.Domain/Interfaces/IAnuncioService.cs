using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WM.Domain.DTOs;
using WM.Domain.Entidades;

namespace WM.Domain.Interfaces
{
    public interface IAnuncioService
    {
        Task<Anuncio> RetornarAnuncio(int Id);
        Task<IEnumerable<Anuncio>> RetornarAnuncios();
        Task<AnuncioDTO> Inserir(AnuncioDTO model);
        Task<AnuncioDTO> Alterar(AnuncioDTO model);
        Task Deletar(int id);

    }
}
