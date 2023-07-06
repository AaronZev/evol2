using BE_CRMColegio.Models;
using Google.Protobuf.WellKnownTypes;

namespace BE_CRMColegio.Repository.Horario
{
    public interface IHorarioRepository
    {
        Task<IEnumerable<Horarios>> GetAllHorarios();
        Task<Horarios> GetHorarioById(int id);
        Task<int> CreateHorario(Horarios horario);
        Task<bool> UpdateHorario(Horarios horario);
        Task<bool> DeleteHorario(int id);
        Task<List<HorarioxClase>> GetHorarioPorClase(string clase);

    }
}
