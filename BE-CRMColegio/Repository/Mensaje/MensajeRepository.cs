using BE_CRMColegio.Models;
using BE_CRMColegio.Repository.Mensaje;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Data;

namespace BE_CRMColegio.Repository.Mensajes
{
    public class MensajeRepository : IMensajeRepository
    {
        private readonly MySQLConfiguration _connectionString;
        public MensajeRepository(MySQLConfiguration connectionString)
        {
            _connectionString = connectionString;
        }
        protected MySqlConnection dbConnection()
        {
            return new MySqlConnection(_connectionString.ConnectionString);
        }
        public void ActualizarMensaje(Mensajes_ mensaje)
        {
            throw new NotImplementedException();
        }

        public void CrearMensaje(Mensajes_ mensaje)
        {
            throw new NotImplementedException();
        }

        public void EliminarMensaje(int idMensaje)
        {
            throw new NotImplementedException();
        }

        public Mensajes_ ObtenerMensajePorId(int idMensaje)
        {
            throw new NotImplementedException();
        }

        public List<Mensajes_> ObtenerMensajes()
        {
            throw new NotImplementedException();
        }
    }
}
