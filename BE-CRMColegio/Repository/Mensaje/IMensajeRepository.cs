using BE_CRMColegio.Models;

namespace BE_CRMColegio.Repository.Mensaje
{
    public interface IMensajeRepository
    {
        List<Mensajes_> ObtenerMensajes();
        Mensajes_ ObtenerMensajePorId(int idMensaje);
        void CrearMensaje(Mensajes_ mensaje);
        void ActualizarMensaje(Mensajes_ mensaje);
        void EliminarMensaje(int idMensaje);
    }
}
