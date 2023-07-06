namespace BE_CRMColegio.Models
{
    public class Horarios
    {
        public int ID_HORARIO { get; set; }
        public string COD_HORARIO { get; set; }
        public int FK_SALON { get; set; }
        public int FK_CURSO { get; set; }
    }

    public class HorarioxClase
    {
        public int ID_HORARIO { get; set; }
        public string COD_HORARIO { get; set; }
        public TimeOnly HORA { get; set; }
        public string CODIGO_SALON { get; set; }
        public string NOMBRE_MATERIA { get; set; }
        public int FK_DOCENTE { get; set; }

    }
}
