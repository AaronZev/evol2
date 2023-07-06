using BE_CRMColegio.Models;

namespace BE_CRMColegio.Repository
{
    public interface IPadreRepository
    {
        Task<List<Padre>> GetPadres();
        Task<Padre> GetPadre(int id);
        Task<bool> PostPadre(Padre padre);
        Task<bool> PutPadre(int id,Padre padre);
        Task<bool> DeletePadre(int id);
    }
}
