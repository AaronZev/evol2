using System.ComponentModel.DataAnnotations;

namespace BE_CRMColegio.Models
{
    public class Estudiantes
    {
        public int ID_ESTUDIANTES { get; set; }
        public int FK_CREDENCIALES { get; set; }
        public int FK_PADRES { get; set; }
        public String? NOMBRE { get; set; }
        public String? FECHA_NACIMIENTO { get; set; }
        public String? DIRECCION { get; set; }
        public String? GENERO { get; set; }
        public String? GRADO { get; set; }
        public String? DNI { get; set; }
        public String? CORREO { get; set; }
        public String? NUMERO_MATRICULA { get; set; }
        public String? NACIONALIDAD { get; set; }
        public String? TELEFONO { get; set; }

    }
    public class EstudianteVer
    {
        public int ID_ESTUDIANTES { get; set; }
        public String? NOMBRE_PADRE { get; set; }
        public String? NOMBRE_ESTUDIANTE { get; set; }
        public String? FECHA_NACIMIENTO { get; set; }
        public String? DIRECCION { get; set; }
        public String? GENERO { get; set; }
        public String? GRADO { get; set; }
        public String? DNI { get; set; }
        public String? CORREO { get; set; }
        public String? NUMERO_MATRICULA { get; set; }
        public String? NACIONALIDAD { get; set; }
        public String? TELEFONO { get; set; }

    }


    public class EstudiantePut
    {
        public int ID_ESTUDIANTES { get; set; }
        public String? NOMBRE { get; set; }
        public String? FECHA_NACIMIENTO { get; set; }
        public String? DIRECCION { get; set; }
        public String? GENERO { get; set; }
        public String? GRADO { get; set; }
        public String? DNI { get; set; }
        public String? NACIONALIDAD { get; set; }

    }
    public class EstudiantePadres
    {
        public int ID_ESTUDIANTES { get; set; }
        public int FK_CREDENCIALES { get; set; }
        public String? NOMBRE_ESTUDIANTE { get; set; }
        public String? NOMBRE_PADRE { get; set; }
        public String? FECHA_NACIMIENTO { get; set; }
        public String? DIRECCION { get; set; }
        public String? GENERO { get; set; }
        public String? GRADO { get; set; }
        public String? DNI { get; set; }
        public String? CORREO { get; set; }
        public String? NUMERO_MATRICULA { get; set; }
        public String? NACIONALIDAD { get; set; }
        public String? TELEFONO { get; set; }
    }

    public class EstudiantePadre
    {
        public int ID { get; set; }
        public int ID_ESTUDIANTES { get; set; }
        public String? NOMBRE_ESTUDIANTE { get; set; }
        public String? DNI { get; set; }
        public String? GRADO { get; set; }

    }

}