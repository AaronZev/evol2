using AutoMapper;
using BE_CRMColegio.Models.DTO;

namespace BE_CRMColegio.Models.Profiles
{
    public class PadreProfile : Profile
    {
        public PadreProfile() {
            CreateMap<Padre, PadreDTO>();
            CreateMap<PadreDTO, Padre>();
        }
    }
}
