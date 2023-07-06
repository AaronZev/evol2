using BE_CRMColegio.Models;

namespace BE_CRMColegio.Repository.SalonClases
{
    public interface ISalonClasesRepository
    {
        Task<IEnumerable<SalonesClases>> GetAll();
        Task<SalonesClases> GetById(int id);
        Task<int> Insert(SalonesClases salonClases);
        Task<bool> Update(SalonesClases salonClases);
        Task<bool> Delete(int id);
    }
}
