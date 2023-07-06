using BE_CRMColegio.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace BE_CRMColegio.Repository.Estudiante
{
    public class EstudianteRepository : IEstudianteRepository
    {
        private MySQLConfiguration _connectionString;

        public EstudianteRepository(MySQLConfiguration connectionString)
        {
            _connectionString = connectionString;
        }
        protected MySqlConnection dbConnection()
        {
            return new MySqlConnection(_connectionString.ConnectionString);
        }

        public async Task<List<Models.Estudiantes>> GetEstudiantes()
        {
            var db = dbConnection();

            var queryEstudiante = @"SELECT 
                            ID_ESTUDIANTES,
                            FK_PADRES,
                            NOMBRE,
                            FECHA_NACIMIENTO,
                            GENERO,
                            DIRECCION,
                            TELEFONO,
                            GRADO,
                            DNI,
                            NUMERO_MATRICULA,
                            NACIONALIDAD
                        FROM
                            Estudiantes
                        WHERE
                            ESTADO = 1 ";

            var dataEstudiante = await db.QueryAsync<Models.Estudiantes>(queryEstudiante);

            return dataEstudiante.ToList();

        }

        public async Task<EstudianteVer> GetEstudiante(int id)
        {
            var db = dbConnection();

            var queryEstudiante = @" 
                            SELECT 
                                e.ID_ESTUDIANTES,
                                e.NOMBRE AS NOMBRE_ESTUDIANTE,
                                e.FECHA_NACIMIENTO,
                                e.GENERO,
                                e.DIRECCION,
                                e.TELEFONO,
                                e.GRADO,
                                e.DNI,
                                e.NUMERO_MATRICULA,
                                e.NACIONALIDAD,
                                c.CORREO,
                                p.NOMBRE AS NOMBRE_PADRE
                            FROM
                                Estudiantes e
                                    INNER JOIN
                                Padres p ON e.FK_PADRES = p.ID_PADRE
                                    INNER JOIN
                                Credenciales c ON e.FK_CREDENCIALES = c.ID_USUARIO
                            WHERE
                                e.ID_ESTUDIANTES = @ID_ESTUDIANTES AND e.ESTADO = 1
                                    AND p.ESTADO
                                    AND c.ESTADO = 1";

            var dataEstudiante = await db.QueryFirstAsync<EstudianteVer>(queryEstudiante, new { ID_ESTUDIANTES = id});

            return dataEstudiante;
        }

        public async Task<Models.EstudiantePadres> GetEstudianteyPadre(int id)
        {
            var db = dbConnection();

            var queryEstudiante = @"
                        SELECT 
                            e.ID_ESTUDIANTES,
                            e.NOMBRE AS NOMBRE_ESTUDIANTE,
                            e.FECHA_NACIMIENTO,
                            e.GENERO,
                            e.DIRECCION,
                            e.TELEFONO,
                            e.GRADO,
                            e.DNI,
                            e.NUMERO_MATRICULA,
                            e.NACIONALIDAD,
                            p.NOMBRE AS NOMBRE_PADRE
                        FROM
                            Estudiantes e
                        INNER JOIN 
	                        Padres p ON e.FK_PADRES = p.ID_PADRE    
                        WHERE
                            e.ID_ESTUDIANTES = @ID_ESTUDIANTES";

            var dataEstudiante = await db.QueryFirstAsync<Models.EstudiantePadres>(queryEstudiante, new { ID_ESTUDIANTES = id });

            return dataEstudiante;
        }

        public async Task<bool> PostEstudiante(Models.Estudiantes estudiante)
        {
            var db = dbConnection();

            var queryCredenciales = @"INSERT INTO Credenciales (CORREO,PASSWORD,FECHA_REG) VALUES(@CORREO,@PASSWORD,CONVERT_TZ(NOW(), '+00:00', '-05:00'))";

            var dataCredenciales = await db.ExecuteAsync(queryCredenciales, new
            {
                estudiante.CORREO,
                PASSWORD = estudiante.DNI + "-CRM"
            });

            var query = @"SELECT LAST_INSERT_ID() FROM Credenciales limit 1";
            var ID_CREDENCIALES = await db.QueryFirstAsync<int>(query);

            var queryEstudiante = @"INSERT INTO Estudiantes (FK_CREDENCIALES,FK_PADRES,NOMBRE,FECHA_NACIMIENTO,GENERO,DIRECCION,TELEFONO,GRADO,DNI,NUMERO_MATRICULA,NACIONALIDAD,FECHA_REG) 
                                    VALUES (@FK_CREDENCIALES,@FK_PADRES,@NOMBRE,@FECHA_NACIMIENTO,@GENERO,@DIRECCION,@TELEFONO,@GRADO,@DNI,@NUMERO_MATRICULA,@NACIONALIDAD,CONVERT_TZ(NOW(), '+00:00', '-05:00'))";

            var dataEstudiante = await db.ExecuteAsync(queryEstudiante, new
            {
                FK_CREDENCIALES = ID_CREDENCIALES,
                estudiante.FK_PADRES,
                estudiante.NOMBRE,
                estudiante.FECHA_NACIMIENTO,
                estudiante.GENERO,
                estudiante.DIRECCION,
                estudiante.TELEFONO,
                estudiante.GRADO,
                estudiante.DNI,
                estudiante.NUMERO_MATRICULA,
                estudiante.NACIONALIDAD
            });

            return dataEstudiante > 0;
        }

        public async Task<bool> DeleteEstudiante(int id, Models.Estudiantes estudiante)
        {
            var db = dbConnection();

            if(estudiante.ID_ESTUDIANTES != id)
            {
                return false;
            }

            var queryEstudiante = @"UPDATE Estudiantes
                                    SET ESTADO = 0
                                    WHERE ID_ESTUDIANTES = @ID_ESTUDIANTE AND ESTADO = 1";

            var dataEstudiante = await db.ExecuteAsync(queryEstudiante, new { ID_ESTUDIANTE = id });

            return dataEstudiante > 0;

        }

        public async Task<bool> PutEstudiante(Models.EstudiantePut estudiante)
        {
            var db = dbConnection();

            var queryEstudiante = @"UPDATE Estudiantes
                                    SET 
                                        NOMBRE = @NOMBRE,
                                        FECHA_NACIMIENTO = @FECHA_NACIMIENTO,
                                        GENERO = @GENERO,
                                        DIRECCION =@DIRECCION,
                                        GRADO = @GRADO,
                                        DNI = @DNI,
                                        NACIONALIDAD = @NACIONALIDAD
                                    WHERE ID_ESTUDIANTES = @ID_ESTUDIANTES AND ESTADO = 1";

            var dataEstudiante = await db.ExecuteAsync(queryEstudiante, new 
            {
                estudiante.ID_ESTUDIANTES,
                estudiante.NOMBRE,
                estudiante.FECHA_NACIMIENTO,
                estudiante.GENERO,
                estudiante.DIRECCION,
                estudiante.GRADO,
                estudiante.DNI,
                estudiante.NACIONALIDAD,
            });

            return dataEstudiante > 0;
        }

        public async Task<List<EstudiantePadre>> GetEstudiantePadres(int id)
        {
            var db = dbConnection();

            var queryEstudiante = @"
                                    SELECT 
                                        e.ID_ESTUDIANTES,e.GRADO,e.NOMBRE as NOMBRE_ESTUDIANTE,e.DNI
                                    FROM
                                        Estudiantes e
                                            INNER JOIN
                                        Padres p ON e.FK_PADRES = p.ID_PADRE
                                    WHERE
                                        p.ID_PADRE = @ID_PADRE AND e.ESTADO = 1
                                            AND P.ESTADO = 1";

            var dataEstudiante = await db.QueryAsync<EstudiantePadre>(queryEstudiante, new { ID_PADRE = id });

            int i = 1;
            foreach ( var e in dataEstudiante)
            {
                e.ID = i;
                i++;
            }
            
            return dataEstudiante.ToList() ;
        }


    }
}
