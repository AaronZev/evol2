using BE_CRMColegio.Models;
using BE_CRMColegio.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BE_CRMColegio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PadreController : ControllerBase
    {
        private readonly IPadreRepository _padreRepository;

        public PadreController(IPadreRepository padreRepositorycs)
        {
            _padreRepository = padreRepositorycs;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {

            try
            {
                var padre = await _padreRepository.GetPadres();

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
                var data = await _padreRepository.GetPadre(id);

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

        [HttpPost()]
        public async Task<IActionResult> Post(Padre padre)
        {
            try
            {

                var result = await _padreRepository.PostPadre(padre);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var padreItem = await _padreRepository.DeletePadre(id);

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

        [HttpPut()]
        public async Task<IActionResult> Put(int id, Padre padre)
        {
            try
            {
                var result = await _padreRepository.PutPadre(id, padre);

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
