using BE_CRMColegio.Models;
using Dapper;
using MySql.Data.MySqlClient;

namespace BE_CRMColegio.Repository.Docente
{
    public class DocenteRepository : IDocenteRepository
    {
        private readonly MySQLConfiguration _connectionString;

        public DocenteRepository(MySQLConfiguration connectionString)
        {
            _connectionString = connectionString;
        }
        protected MySqlConnection dbConnection()
        {
            return new MySqlConnection(_connectionString.ConnectionString);
        }

        public async Task<Docentes> GetDocente(int id)
        {
            var db = dbConnection();

            var queryDocente = @"SELECT 
                                    d.ID_DOCENTE,
                                    d.NOMBRES,
                                    d.ESPECIALIDAD,
                                    d.TELEFONO,
                                    c.CORREO,
                                    d.DNI,
                                    d.GENERO
                                FROM
                                    Docente d
                                        INNER JOIN
                                    Credenciales c ON d.FK_CREDENCIALES = c.ID_USUARIO
                                WHERE
                                    d.ID_DOCENTE = @ID_DOCENTE
                                        AND d.ESTADO = 1
                                        AND c.ESTADO = 1";

            var dataDocente = await db.QueryFirstAsync<Docentes>(queryDocente, new { ID_DOCENTE = id });
            return dataDocente;
        }

        public async Task<List<Docentes>> GetDocentes()
        {
            var db = dbConnection();

            var queryDocente = @"SELECT 
                            d.ID_DOCENTE,
                            d.NOMBRES,
                            d.ESPECIALIDAD,
                            d.TELEFONO,
                            c.CORREO,
                            d.DNI,
                            d.GENERO
                        FROM
                            Docente d
                                INNER JOIN
                            Credenciales c ON d.FK_CREDENCIALES = c.ID_USUARIO
                        WHERE
                            d.ESTADO = 1 AND c.ESTADO = 1 ";
            
            var dataDocente = await db.QueryAsync<Docentes>(queryDocente);

            return dataDocente.ToList();

        }

        public async Task<bool> PutDocente(Docentes docente)
        {
            var db = dbConnection();

            var queryDocente = @"UPDATE Docente 
                                SET 
                                    NOMBRES = @NOMBRES,
                                    ESPECIALIDAD = @ESPECIALIDAD,
                                    TELEFONO = @TELEFONO,
                                    DNI = @DNI,
                                    GENERO = @GENERO
                                WHERE
                                    ID_DOCENTE = @ID_DOCENTE AND ESTADO = 1";

            var dataDocente = await db.ExecuteAsync(queryDocente, new
            {
                docente.ID_DOCENTE,
                docente.NOMBRES,
                docente.ESPECIALIDAD,
                docente.TELEFONO,
                docente.DNI,
                docente.GENERO
            });

            return dataDocente > 0;
        }

        public async Task<bool> DeleteDocente(int id)
        {
            var db = dbConnection();

            var queryDocente = @"UPDATE Docente
                                  SET ESTADO = 0 
                                  WHERE ID_DOCENTE = @ID_DOCENTE AND ESTADO = 1";

            var dataDocente = await db.ExecuteAsync(queryDocente, new { ID_DOCENTE = id });

            return dataDocente > 0;
        }

        public async Task<bool> PostDocente(Docentes docente)
        {
            var db = dbConnection();

            var queryCredenciales = @"INSERT INTO Credenciales (CORREO,PASSWORD,FECHA_REG) VALUES(@CORREO,@PASSWORD,CONVERT_TZ(NOW(), '+00:00', '-05:00'))";

            var dataCredenciales = await db.ExecuteAsync(queryCredenciales, new
            {
                docente.CORREO,
                PASSWORD = docente.DNI + "-CRM"
            });

            var query = @"SELECT LAST_INSERT_ID() FROM Credenciales limit 1";
            var ID_CREDENCIALES = await db.QueryFirstAsync<int>(query);

            var queryDocente = @"INSERT INTO Docente(NOMBRES,ESPECIALIDAD,TELEFONO,DNI,FK_CREDENCIALES,FECHA_REG,GENERO) 
                                VALUES(@NOMBRES,@ESPECIALIDAD,@TELEFONO,@DNI,@FK_CREDENCIALES,CONVERT_TZ(NOW(), '+00:00', '-05:00'),GENERO)";
            var dataDocente = await db.ExecuteAsync(queryDocente, new
            {
                docente.NOMBRES,
                docente.ESPECIALIDAD,
                docente.TELEFONO,
                docente.DNI,
                docente.GENERO,
                FK_CREDENCIALES = ID_CREDENCIALES
            });

            return dataDocente > 0 && dataCredenciales > 0;
        }
    }
}
