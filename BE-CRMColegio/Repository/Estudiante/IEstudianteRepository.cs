using BE_CRMColegio.Models;

namespace BE_CRMColegio.Repository.Estudiante
{
    public interface IEstudianteRepository 
    {
        Task<List<Estudiantes>> GetEstudiantes();
        Task<EstudianteVer> GetEstudiante(int id);
        Task<bool> PostEstudiante(Estudiantes estudiante);
        Task<bool> PutEstudiante(EstudiantePut estudiante);
        Task<bool> DeleteEstudiante(int id, Estudiantes estudiante);
        Task<List<EstudiantePadre>> GetEstudiantePadres(int id);
        Task<Models.EstudiantePadres> GetEstudianteyPadre(int id);
        //Task<EstudianteVer> GetEstudiantever(int id);

    }
}
