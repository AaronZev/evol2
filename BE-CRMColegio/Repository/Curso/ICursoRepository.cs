using BE_CRMColegio.Models;

namespace BE_CRMColegio.Repository.Curso
{
    public interface ICursoRepository
    {
        Task<IEnumerable<Cursos>> GetAll();
        Task<Cursos> GetById(int id);
        Task<int> Create(Cursos curso);
        Task<bool> Update(Cursos curso);
        Task<bool> Delete(int id);
    }
}
