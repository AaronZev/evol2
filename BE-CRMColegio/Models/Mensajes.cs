namespace BE_CRMColegio.Models
{
    public class Mensajes_
    {
        public int ID_MENSAJE { get; set; }
        public int FK_DOCENTE { get; set; }
        public int FK_PADRE { get; set; }
        public string MENSAJE { get; set; }
        public DateTime FECHA_ENVIO { get; set; }
    }
}
