using System.ComponentModel.DataAnnotations;

namespace BE_CRMColegio.Models
{
    public class Padre
    {
        public int ID_PADRE { get; set; }
        public String? DNI { get; set; }
        public String? NOMBRE { get; set; }
        public String? DIRECCION { get; set; }
        public String? TELEFONO { get; set; }
        public String? OCUPACION { get; set; }
        public String? CONTACTO_EMERGENCIA_NOMBRE { get; set; }
        public String? CONTACTO_EMERGENCIA_TELEFONO { get; set; }
        public String? CONTACTO_EMERGENCIA_RELACION { get; set; }
        public String? METODO_CONTACTO_PREFERIDO { get; set; }
        public bool SUSCRIPCION_BOLETIN { get; set; }
        public String? CORREO { get; set; }
        public String? PASSWORD { get; set; }


    }
}
