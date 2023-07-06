using BE_CRMColegio.Models;
using BE_CRMColegio.Repository.Horario;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE_CRMColegio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HorarioController : ControllerBase
    {
        private readonly IHorarioRepository _horarioRepository;

        public HorarioController(IHorarioRepository horarioRepository)
        {
            _horarioRepository = horarioRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Horarios>>> GetAllHorarios()
        {
            var horarios = await _horarioRepository.GetAllHorarios();
            return Ok(horarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Horarios>> GetHorarioById(int id)
        {
            var horario = await _horarioRepository.GetHorarioById(id);
            if (horario == null)
            {
                return NotFound();
            }
            return Ok(horario);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateHorario(Horarios horario)
        {
            var horarioId = await _horarioRepository.CreateHorario(horario);
            return Ok(horarioId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHorario(int id, Horarios horario)
        {
            if (id != horario.ID_HORARIO)
            {
                return BadRequest();
            }

            var result = await _horarioRepository.UpdateHorario(horario);
            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHorario(int id)
        {
            var result = await _horarioRepository.DeleteHorario(id);
            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }
        [HttpGet("clase/{id}")]
        public async Task<ActionResult<Horarios>> GetHorarioPorClase(string id)
        {
            var horario = await _horarioRepository.GetHorarioPorClase(id);
            if (horario == null)
            {
                return NotFound();
            }
            return Ok(horario);
        }

    }
}
