using BE_CRMColegio.Models;
using System.Reflection.Metadata;

namespace BE_CRMColegio.Repository.Docente
{
    public interface IDocenteRepository
    {
        public Task<List<Docentes>> GetDocentes();
        public Task<Docentes> GetDocente(int id);
        public Task<bool> PutDocente(Docentes docentete);
        public Task<bool> DeleteDocente(int id);
        public Task<bool> PostDocente(Docentes docente);

    }
}
