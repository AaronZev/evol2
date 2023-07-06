using BE_CRMColegio.Models;
using Dapper;
using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using System.Collections;

namespace BE_CRMColegio.Repository.Horario
{
    public class HorarioRepository : IHorarioRepository
    {

        private readonly MySQLConfiguration _connectionString;
        public HorarioRepository(MySQLConfiguration connectionString)
        {
            _connectionString = connectionString;
        }
        protected MySqlConnection dbConnection()
        {
            return new MySqlConnection(_connectionString.ConnectionString);
        }

        public async Task<IEnumerable<Horarios>> GetAllHorarios()
        {
            var connection = dbConnection();
            var query = "SELECT * FROM Horario WHERE ESTADO = 1";
            return await connection.QueryAsync<Horarios>(query);
        }

        public async Task<Horarios> GetHorarioById(int id)
        {
            var connection = dbConnection();
            var query = "SELECT * FROM Horario WHERE ID_HORARIO = @Id AND ESTADO = 1";
            return await connection.QuerySingleOrDefaultAsync<Horarios>(query, new { Id = id });
        }

        public async Task<int> CreateHorario(Horarios horario)
        {
            var connection = dbConnection();
            var query = @"INSERT INTO Horario (COD_HORARIO, FK_SALON, FK_CURSO, FECHA_REG)
                      VALUES (@COD_HORARIO, @FK_SALON, @FK_CURSO, CONVERT_TZ(NOW(), '+00:00', '-05:00') )";
            return await connection.ExecuteScalarAsync<int>(query, new
            {
                horario.COD_HORARIO,
                horario.FK_SALON,
                horario.FK_CURSO
            });
        }

        public async Task<bool> UpdateHorario(Horarios horario)
        {
            var connection = dbConnection();
            var query = @"UPDATE Horario SET
                      COD_HORARIO = @CodHorario,
                      FK_SALON = @FkSalon,
                      FK_CURSO = @FkCurso
                      WHERE ID_HORARIO = @Id AND ESTADO = 1";
            var rowsAffected = await connection.ExecuteAsync(query, new
            {
                horario.ID_HORARIO,
                horario.COD_HORARIO,
                horario.FK_SALON,
                horario.FK_CURSO
            });
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteHorario(int id)
        {
            var connection = dbConnection();
            var query = "UPDATE Horario SET ESTADO = 0 WHERE ID_HORARIO = @Id WHERE ESTADO = 1";
            var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });
            return rowsAffected > 0;
        }


        public async Task<List<HorarioxClase>> GetHorarioPorClase(string clase)
        {
            var db = dbConnection();

            var query = @"
                            SELECT
                                h.ID_HORARIO,
                                h.COD_HORARIO,
                                h.HORA,
                                s.CODIGO_SALON,
                                c.NOMBRE_MATERIA,
                                c.FK_DOCENTE
                            FROM
                                Horario h
                            INNER JOIN 
                                SalonClases s ON s.ID_SALON = h.FK_SALON
                            INNER JOIN 
                                Cursos c ON c.ID_CURSO = h.FK_CURSO
                            WHERE 
                                s.CODIGO_SALON = @CODIGO_SALON AND h.ESTADO = 1 AND c.ESTADO = 1 AND s.ESTADO = 1
                            ORDER BY
                                h.HORA ASC";

            var data = await db.QueryAsync<HorarioxClase>(query, new
            {
                CODIGO_SALON = clase
            });

            return data.ToList();
        }
    }
}
