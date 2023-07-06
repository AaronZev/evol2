using BE_CRMColegio.Models;
using Dapper;
using MySql.Data.MySqlClient;

namespace BE_CRMColegio.Repository.Curso
{
    public class CursoRepository : ICursoRepository
    {
        private readonly MySQLConfiguration _connectionString;

        public CursoRepository(MySQLConfiguration connectionString)
        {
            _connectionString = connectionString;
        }
        protected MySqlConnection dbConnection()
        {
            return new MySqlConnection(_connectionString.ConnectionString);
        }

        public async Task<IEnumerable<Cursos>> GetAll()
        {
            using (var db = dbConnection())
            {
                var query = @"
                            SELECT 
                                c.*, d.NOMBRES AS NOMBRE_DOCENTE
                            FROM
                                Cursos c
                                    INNER JOIN
                                Docente d ON c.FK_DOCENTE = d.ID_DOCENTE
                            WHERE
                                c.ESTADO AND d.ESTADO = 1;
                            ";
                return await db.QueryAsync<Cursos>(query);
            }
        }

        public async Task<Cursos> GetById(int id)
        {
            using (var db = dbConnection())
            {
                var query = @"SELECT * FROM Cursos WHERE ID_CURSO = @Id";
                return await db.QueryFirstOrDefaultAsync<Cursos>(query, new { Id = id });
            }
        }

        public async Task<int> Create(Cursos curso)
        {
            using (var db = dbConnection())
            {
                var query = @"INSERT INTO Cursos (NOMBRE_MATERIA, FK_DOCENTE, FECHA_REG)
                          VALUES (@NOMBRE_MATERIA, @FK_DOCENTE, CONVERT_TZ(NOW(), '+00:00', '-05:00'))";
                return await db.ExecuteScalarAsync<int>(query, new { curso.NOMBRE_MATERIA, curso.FK_DOCENTE });
            }
        }

        public async Task<bool> Update(Cursos curso)
        {
            using (var db = dbConnection())
            {
                var query = @"UPDATE Cursos SET NOMBRE_MATERIA = @NOMBRE_MATERIA, FK_DOCENTE = @FK_DOCENTE           
                                WHERE ID_CURSO = @ID_CURSO AND ESTADO = 1";
                var rowsAffected = await db.ExecuteAsync(query, new { curso.NOMBRE_MATERIA, curso.FK_DOCENTE, curso.ID_CURSO});
                return rowsAffected > 0;
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (var db = dbConnection())
            {
                var query = @"UPDATE Cursos SET ESTADO = 0 WHERE ID_CURSO = @Id AND ESTADO = 1";
                var rowsAffected = await db.ExecuteAsync(query, new { Id = id });
                return rowsAffected > 0;
            }
        }













    }
}
