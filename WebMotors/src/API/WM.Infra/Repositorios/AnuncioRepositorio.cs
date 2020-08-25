using Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WM.Domain.Entidades;
using WM.Domain.Interfaces;
using WM.Infra.Data;

namespace WM.Infra.Repositorios
{
    public class AnuncioRepositorio : IAnuncioRepositorio
    {
        private readonly AnuncioContext _context;

        public AnuncioRepositorio(AnuncioContext context)
        {
            _context = context;
        }

        public async Task<int> Inserir(Anuncio model)
        {
            try
            {
                using (var sqlConn = _context.OpenConn())
                {
                    var id = await sqlConn.ExecuteScalarAsync(
                    @"INSERT INTO [tb_AnuncioWebmotors] VALUES (@marca, @modelo, @versao, @ano, @quilometragem, @observacao)
                    SELECT SCOPE_IDENTITY()",
                        new { @marca = model.Marca, 
                              @modelo = model.Modelo,
                              @versao = model.Versao,
                              @ano = model.Ano,
                              @quilometragem = model.Quilometragem,
                              @observacao = model.Observacao 
                        });

                    return await Task.FromResult(Convert.ToInt32(id));
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task Alterar(Anuncio model)
        {
            try
            {
                using (var sqlConn = _context.OpenConn())
                {
                    await sqlConn.ExecuteAsync(
                    $@"UPDATE tb_AnuncioWebmotors
                            SET marca = @marca,
                                modelo = @modelo,
                                versao = @versao,
                                ano = @ano,
                                quilometragem = @quilometragem,
                                observacao = @observacao
                        WHERE ID = @id",
                        new
                        {
                            @id = model.Id,
                            @marca = model.Marca,
                            @modelo = model.Modelo,
                            @versao = model.Versao,
                            @ano = model.Ano,
                            @quilometragem = model.Quilometragem,
                            @observacao = model.Observacao
                        });

                    await Task.CompletedTask;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task Deletar(int id)
        {
            try
            {
                using (var sqlConn = _context.OpenConn())
                {
                    await sqlConn.ExecuteScalarAsync(
                    @"DELETE [tb_AnuncioWebmotors] WHERE ID = @id",
                        new { @id = id });

                    await Task.CompletedTask;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Anuncio> RetornarAnuncio(int id)
        {
            try
            {
                using (var sqlConn = _context.OpenConn())
                {
                    var anuncio = await sqlConn.QueryFirstOrDefaultAsync<Anuncio>(
                    @"
                        SELECT 
	                        marca AS Marca,
	                        modelo AS Modelo,
	                        versao AS Versao,
                            ano AS Ano,
                            quilometragem AS Quilometragem,
                            observacao AS Observacao
                        FROM tb_AnuncioWebmotors
                        WHERE ID = @id", new { @id = id});

                    return await Task.FromResult(anuncio);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<Anuncio>> RetornarAnuncios()
        {
            try
            {
                using (var sqlConn = _context.OpenConn())
                {
                    var anuncios = await sqlConn.QueryAsync<Anuncio>(
                    @"
                        SELECT 
                            Id AS ID,
	                        marca AS Marca,
	                        modelo AS Modelo,
	                        versao AS Versao,
                            ano AS Ano,
                            quilometragem AS Quilometragem,
                            observacao AS Observacao
                        FROM tb_AnuncioWebmotors");

                    return await Task.FromResult(anuncios);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
