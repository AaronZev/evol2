using BE_CRMColegio.Models;
using BE_CRMColegio.Repository.Mensaje;
using Microsoft.AspNetCore.Mvc;

namespace BE_CRMColegio.Controllers
{
    public class MensajeController : Controller
    {
        
            private readonly IMensajeRepository _mensajeRepository;

            public MensajeController(IMensajeRepository mensajeRepository)
            {
                _mensajeRepository = mensajeRepository;
            }

            public IActionResult Index()
            {
                var mensajes = _mensajeRepository.ObtenerMensajes();
                return View(mensajes);
            }

            [HttpGet]
            public IActionResult Crear()
            {
                return View();
            }

            [HttpPost]
            public IActionResult Crear(Mensajes_ mensaje)
            {
                if (ModelState.IsValid)
                {
                    _mensajeRepository.CrearMensaje(mensaje);
                    return RedirectToAction("Index");
                }
                return View();
            }

            [HttpGet]
            public IActionResult Editar(int id)
            {
                var mensaje = _mensajeRepository.ObtenerMensajePorId(id);
                if (mensaje == null)
                {
                    return NotFound();
                }
                return View(mensaje);
            }

            [HttpPost]
            public IActionResult Editar(Mensajes_ mensaje)
            {
                if (ModelState.IsValid)
                {
                    _mensajeRepository.ActualizarMensaje(mensaje);
                    return RedirectToAction("Index");
                }
                return View();
            }

            [HttpGet]
            public IActionResult Eliminar(int id)
            {
                var mensaje = _mensajeRepository.ObtenerMensajePorId(id);
                if (mensaje == null)
                {
                    return NotFound();
                }
                return View(mensaje);
            }

            [HttpPost]
            public IActionResult EliminarConfirmado(int id)
            {
                _mensajeRepository.EliminarMensaje(id);
                return RedirectToAction("Index");
            }
    }
}
