using BE_CRMColegio.Models;
using BE_CRMColegio.Repository.Docente;
using BE_CRMColegio.Repository.Estudiante;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE_CRMColegio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocenteController : ControllerBase
    {
        private readonly IDocenteRepository _docenteRepository;

        public DocenteController(IDocenteRepository docenteRepository)
        {
            _docenteRepository = docenteRepository;
        }


        //public Task<bool> PutDocente(Docentes docentete);
        //public Task<bool> DeleteDocente(int id);
        //public Task<bool> PostDocente(Docentes docente);

        [HttpGet]
        public async Task<IActionResult> GetDocentes()
        {

            try
            {
                var result = await _docenteRepository.GetDocentes();

                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDocente(int id)
        {
            try
            {
                var data = await _docenteRepository.GetDocente(id);

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
        public async Task<IActionResult> PostDocente(Docentes docente)
        {
            try
            {

                var result = await _docenteRepository.PostDocente(docente);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocente(int id)
        {
            try
            {
                var padreItem = await _docenteRepository.DeleteDocente(id);

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
        public async Task<IActionResult> PutDocente(Docentes docente)
        {
            try
            {
                var result = await _docenteRepository.PutDocente(docente);

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




    }
}
