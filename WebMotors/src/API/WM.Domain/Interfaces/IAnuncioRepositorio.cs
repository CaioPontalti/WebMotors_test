using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WM.Domain.Entidades;

namespace WM.Domain.Interfaces
{
    public interface IAnuncioRepositorio
    {
        Task<Anuncio> RetornarAnuncio(int Id);
        Task<IEnumerable<Anuncio>> RetornarAnuncios();
        Task<int> Inserir(Anuncio model);
        Task Alterar(Anuncio model);
        Task Deletar(int id);
    }
}
