using BE_CRMColegio.Models;
using Dapper;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.Common;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BE_CRMColegio.Repository.SalonClases
{
    public class SalonClasesRepository : ISalonClasesRepository
    {
        private readonly MySQLConfiguration _connectionString;
        public SalonClasesRepository(MySQLConfiguration connectionString)
        {
            _connectionString = connectionString;
        }
        protected MySqlConnection dbConnection()
        {
            return new MySqlConnection(_connectionString.ConnectionString);
        }

        public async Task<IEnumerable<SalonesClases>> GetAll()
        {
            var db = dbConnection();

            var querySalon = @"SELECT * FROM SalonClases WHERE ESTADO = 1";

            var dataSalon = await db.QueryAsync<SalonesClases>(querySalon);

            return dataSalon.ToList();

        }
        public async Task<SalonesClases> GetById(int id)
        {
            var db = dbConnection();
            var query = "SELECT * FROM SalonClases WHERE ID_SALON = @Id";
            var parameters = new { Id = id };
            return await db.QueryFirstOrDefaultAsync<SalonesClases>(query, parameters);
        }

        public async Task<int> Insert(SalonesClases salonClases)
        {
            var db = dbConnection();
            var query = @"INSERT INTO SalonClases (CODIGO_SALON, FECHA_REG)
                          VALUES (@CODIGO_SALON, CONVERT_TZ(NOW(), '+00:00', '-05:00'))";
            return await db.ExecuteScalarAsync<int>(query, new { salonClases.CODIGO_SALON });
        }

        public async Task<bool> Update(SalonesClases salonClases)
        {
            var db = dbConnection();
            var query = @"UPDATE SalonClases SET
                          CODIGO_SALON = @CODIGO_SALON
                          WHERE ID_SALON = @ID_SALON";
            var rowsAffected = await db.ExecuteAsync(query, new { salonClases.ID_SALON, salonClases.CODIGO_SALON });
            return rowsAffected > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var db = dbConnection();
            var query = "UPDATE SalonClases SET ESTADO = 0 WHERE ID_SALON = @Id AND ESTADO = 1";
            var parameters = new { Id = id };
            var rowsAffected = await db.ExecuteAsync(query, parameters);
            return rowsAffected > 0;
        }


    }
}
