
using MesaAyuda.Web.Models;

namespace MesaAyuda.Web.Services;

public interface ITicketService
{
    // Obtener todos los tickets
    Task<List<Ticket>> ObtenerTodosAsync();

    // Obtener un ticket por su identificador
    Task<Ticket?> ObtenerPorIdAsync(int id);

    // Obtener las categorías disponibles
    Task<List<Categoria>> ObtenerCategoriasAsync();

    // Registrar un nuevo ticket
    Task<int> CrearAsync(Ticket ticket);

    // Actualizar un ticket existente
    Task<bool> ActualizarAsync(Ticket ticket);

    // Cerrar un ticket
    Task<bool> CerrarAsync(int id);

    // Eliminar un ticket
    Task<bool> EliminarAsync(int id);
}
