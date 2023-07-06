using BE_CRMColegio.Models;
using BE_CRMColegio.Repository.Curso;
using BE_CRMColegio.Repository.Docente;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE_CRMColegio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursoController : ControllerBase
    {
        private readonly ICursoRepository _cursoRepository;

        public CursoController(ICursoRepository cursoRepository)
        {
            _cursoRepository = cursoRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cursos>>> GetAllCursos()
        {
            var cursos = await _cursoRepository.GetAll();
            return Ok(cursos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cursos>> GetCursoById(int id)
        {
            var curso = await _cursoRepository.GetById(id);
            if (curso == null)
            {
                return NotFound();
            }
            return Ok(curso);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateCurso(Cursos curso)
        {
            var cursoId = await _cursoRepository.Create(curso);
            return Ok(cursoId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCurso(int id, Cursos curso)
        {
            if (id != curso.ID_CURSO)
            {
                return BadRequest();
            }

            var result = await _cursoRepository.Update(curso);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurso(int id)
        {
            var result = await _cursoRepository.Delete(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }



    }
}
