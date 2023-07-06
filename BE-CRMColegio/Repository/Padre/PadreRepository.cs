using BE_CRMColegio.Models;
using Dapper;
using Microsoft.OpenApi.Validations;
using MySql.Data.MySqlClient;

namespace BE_CRMColegio.Repository
{
    public class PadreRepository : IPadreRepository
    {
        private readonly MySQLConfiguration _connectionString;

        public PadreRepository(MySQLConfiguration connectionString)
        {
            _connectionString = connectionString;
        }
        protected MySqlConnection dbConnection()
        {
            return new MySqlConnection(_connectionString.ConnectionString);
        }

        public async Task<Padre> GetPadre(int id)
        {
            var db = dbConnection();

            var queryPadre = @"
                                SELECT 
                                    ID_PADRE,
                                    DNI,
                                    NOMBRE,
                                    DIRECCION,
                                    TELEFONO,
                                    OCUPACION,
                                    CONTACTO_EMERGENCIA_NOMBRE,
                                    CONTACTO_EMERGENCIA_TELEFONO,
                                    CONTACTO_EMERGENCIA_RELACION,
                                    METODO_CONTACTO_PREFERIDO
                                FROM
                                    Padres
                                WHERE
                                    ID_PADRE = @ID_PADRE AND ESTADO = 1";

            var dataPadre = await db.QueryFirstOrDefaultAsync<Padre>(queryPadre, new { ID_PADRE = id });

            return dataPadre;

        }

        public async Task<List<Padre>> GetPadres()
        {
            var db = dbConnection();
            var queryPadre = @"
                                SELECT 
                                    ID_PADRE,
                                    DNI,
                                    NOMBRE,
                                    DIRECCION,
                                    TELEFONO,
                                    OCUPACION,
                                    CONTACTO_EMERGENCIA_NOMBRE,
                                    CONTACTO_EMERGENCIA_TELEFONO,
                                    CONTACTO_EMERGENCIA_RELACION,
                                    METODO_CONTACTO_PREFERIDO
                                FROM
                                    Padres
                                WHERE
                                   ESTADO = 1";

            var dataPadre = await db.QueryAsync<Padre>(queryPadre);

            return dataPadre.ToList();
        }

        public async Task<bool> PostPadre(Padre padre)
        {
            var db = dbConnection();

            var queryCredenciales = @"INSERT INTO Credenciales (CORREO,PASSWORD,FECHA_REG) VALUES(@CORREO,@PASSWORD,CONVERT_TZ(NOW(), '+00:00', '-05:00'))";

            var dataCredenciales = await db.ExecuteAsync(queryCredenciales, new
            {
                padre.CORREO,
                PASSWORD = padre.DNI+"-CRM"
            });

            var query = @"SELECT LAST_INSERT_ID() FROM Credenciales limit 1";
            var ID_CREDENCIALES = await db.QueryFirstAsync<int>(query);

            var queryInsert = @"INSERT INTO Padres (DNI,NOMBRE,DIRECCION,TELEFONO,OCUPACION,CONTACTO_EMERGENCIA_NOMBRE,CONTACTO_EMERGENCIA_TELEFONO,CONTACTO_EMERGENCIA_RELACION,METODO_CONTACTO_PREFERIDO,SUSCRIPCION_BOLETIN,FK_CREDENCIALES,FECHA_REG)
                                VALUES(@DNI,@NOMBRE,@DIRECCION,@TELEFONO,@OCUPACION,@CONTACTO_EMERGENCIA_NOMBRE,@CONTACTO_EMERGENCIA_TELEFONO,@CONTACTO_EMERGENCIA_RELACION,@METODO_CONTACTO_PREFERIDO,@SUSCRIPCION_BOLETIN,@FK_CREDENCIALES,CONVERT_TZ(NOW(), '+00:00', '-05:00'))";
            var dataPadre = await db.ExecuteAsync(queryInsert, new { 
                padre.DNI,
                padre.NOMBRE,
                padre.DIRECCION,
                padre.TELEFONO,
                padre.OCUPACION,
                padre.CONTACTO_EMERGENCIA_NOMBRE,
                padre.CONTACTO_EMERGENCIA_TELEFONO,
                padre.CONTACTO_EMERGENCIA_RELACION,
                padre.METODO_CONTACTO_PREFERIDO,
                padre.SUSCRIPCION_BOLETIN,
                FK_CREDENCIALES = ID_CREDENCIALES
            });

            return dataPadre > 0 ;
        }

        public async Task<bool> PutPadre(int id,Padre padre)
        {
            var db = dbConnection();

            if(id != padre.ID_PADRE)
            {
                return false;
            }

            var data = @"UPDATE Padres 
                        SET 
                            DNI = @DNI,
                            NOMBRE = @NOMBRE,
                            DIRECCION = @DIRECCION,
                            TELEFONO = @TELEFONO,
                            OCUPACION = @OCUPACION,
                            CONTACTO_EMERGENCIA_NOMBRE = @CONTACTO_EMERGENCIA_NOMBRE,
                            CONTACTO_EMERGENCIA_TELEFONO = @CONTACTO_EMERGENCIA_TELEFONO,
                            CONTACTO_EMERGENCIA_RELACION = @CONTACTO_EMERGENCIA_RELACION,
                            METODO_CONTACTO_PREFERIDO = @METODO_CONTACTO_PREFERIDO
                        WHERE
                            ID_PADRE = @ID_PADRE AND ESTADO = 1";

            var queryData = await db.ExecuteAsync(data, new
            {
                ID_PADRE = id,
                padre.DNI,
                padre.NOMBRE,
                padre.DIRECCION,
                padre.TELEFONO,
                padre.OCUPACION,
                padre.CONTACTO_EMERGENCIA_NOMBRE,
                padre.CONTACTO_EMERGENCIA_TELEFONO,
                padre.CONTACTO_EMERGENCIA_RELACION,
                padre.METODO_CONTACTO_PREFERIDO
            });

            return queryData > 0;
        }

        public async Task<bool> DeletePadre(int id)
        {
            
            var db = dbConnection();
            var querySelectCredenciales = @"SELECT FK_CREDENCIALES FROM Padres WHERE ID_PADRE = @ID_PADRE AND ESTADO = 1";
            var dataSelectCredenciales = await db.QueryFirstOrDefaultAsync(querySelectCredenciales, new {ID_PADRE= id});

            var queryCredenciales = @"
                            UPDATE Credenciales 
                            SET 
                                ESTADO = 0
                            WHERE
                                ID_USUARIO = @ID_USUARIO AND ESTADO = 1";

            var dataCredenciales = await db.ExecuteAsync(queryCredenciales, new { ID_USUARIO = dataSelectCredenciales.FK_CREDENCIALES });

            var queryPadres = @"
                            UPDATE Padres 
                            SET 
                                ESTADO = 0
                            WHERE
                                ID_PADRE = @ID_PADRE AND ESTADO = 1";

            var dataDelete = await db.ExecuteAsync(queryPadres, new { ID_PADRE = id });

            return dataDelete > 0 && dataCredenciales > 0 ;

        }
    }
}
