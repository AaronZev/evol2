using BE_CRMColegio.Models;
using BE_CRMColegio.Repository.Estudiante;
using BE_CRMColegio.Repository.SalonClases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE_CRMColegio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalonClasesController : ControllerBase
    {
        private readonly ISalonClasesRepository _salonClasesRepository;

        public SalonClasesController(ISalonClasesRepository salonClasesRepository)
        {
            _salonClasesRepository = salonClasesRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalonesClases>>> GetAll()
        {
            try
            {
                var salonClases = await _salonClasesRepository.GetAll();
                return Ok(salonClases);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SalonesClases>> GetById(int id)
        {
            try
            {
                var salonClases = await _salonClasesRepository.GetById(id);
                if (salonClases == null)
                {
                    return NotFound();
                }
                return Ok(salonClases);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<int>> Insert(SalonesClases salonClases)
        {
            try
            {
                var insertedId = await _salonClasesRepository.Insert(salonClases);
                return Ok(insertedId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Update(int id, SalonesClases salonClases)
        {
            try
            {
                if (id != salonClases.ID_SALON)
                {
                    return BadRequest("ID mismatch between parameter and object.");
                }

                var result = await _salonClasesRepository.Update(salonClases);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            try
            {
                var result = await _salonClasesRepository.Delete(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }



    }
}
