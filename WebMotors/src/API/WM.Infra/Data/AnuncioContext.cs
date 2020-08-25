using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using WM.Domain.Entidades;

namespace WM.Infra.Data
{
    public class AnuncioContext : IDisposable
    {
        private readonly IConfiguration _configuration;
        private SqlConnection Connection { get; set; }

        public AnuncioContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public SqlConnection OpenConn()
        {
            Connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            Connection.Open();

            return Connection;
        }

        public void Dispose()
        {
            if (Connection != null)
                if (Connection.State != ConnectionState.Closed)
                    Connection.Close();
        }
    }
}
