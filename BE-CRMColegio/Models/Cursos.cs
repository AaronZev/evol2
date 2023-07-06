namespace BE_CRMColegio.Models
{
    public class Cursos
    {
        public int ID_CURSO { get; set; }
        public string NOMBRE_MATERIA { get; set; }
        public string NOMBRE_DOCENTE { get; set; }
        public int FK_DOCENTE { get; set; }

    }
}
