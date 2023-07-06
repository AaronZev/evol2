using BE_CRMColegio.Models;
using BE_CRMColegio.Repository;
using BE_CRMColegio.Repository.Estudiante;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE_CRMColegio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudianteController : ControllerBase
    {
        private readonly IEstudianteRepository _estudianteRepository;

        public EstudianteController(IEstudianteRepository estudianteRepository)
        {
            _estudianteRepository = estudianteRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {

            try
            {
                var padre = await _estudianteRepository.GetEstudiantes();

                return Ok(padre);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var data = await _estudianteRepository.GetEstudiante(id);

                if (data == null)
                {
                    return NotFound();
                }
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("Obtener/{id}")]
        public async Task<IActionResult> GetEstudiantesyPadres(int id)
        {
            try
            {
                var data = await _estudianteRepository.GetEstudianteyPadre(id);

                if (data == null)
                {
                    return NotFound();
                }
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Estudiantes estudiante)
        {
            try
            {

                var result = await _estudianteRepository.PostEstudiante(estudiante);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, Estudiantes estudiante)
        {
            try
            {
                var padreItem = await _estudianteRepository.DeleteEstudiante(id, estudiante);

                if (padreItem == false)
                {
                    return NotFound();
                }


                return Ok(padreItem);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(EstudiantePut estudiante)
        {
            try
            {
                var result = await _estudianteRepository.PutEstudiante(estudiante);

                if (result == false)
                {
                    return NotFound();
                }

                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("Padre/{id}")]
        public async Task<IActionResult> GetEstudiantePadres(int id)
        {
            try
            {
                var data = await _estudianteRepository.GetEstudiantePadres(id);

                if (data == null)
                {
                    return NotFound();
                }
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}
