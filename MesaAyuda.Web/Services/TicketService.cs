using MesaAyuda.Web.Models;
using MySqlConnector;

namespace MesaAyuda.Web.Services;

public class TicketService : ITicketService
{
    private readonly string _connectionString;

    public TicketService(IConfiguration configuration)
    {
        _connectionString = configuration
            .GetConnectionString("MySqlConnection")
            ?? throw new InvalidOperationException(
                "No se configuró la conexión a MySQL.");
    }

    public async Task<List<Ticket>> ObtenerTodosAsync()
    {
        var tickets = new List<Ticket>();

        await using var conexion =
            new MySqlConnection(_connectionString);

        await conexion.OpenAsync();

        const string sql = """
            SELECT t.id, t.titulo, t.descripcion,
                   t.categoria_id, c.nombre AS categoria_nombre,
                   t.estado, t.fecha_creacion, t.fecha_cierre
            FROM tickets t
            INNER JOIN categorias c ON t.categoria_id = c.id
            ORDER BY t.id DESC;
            """;

        await using var comando = new MySqlCommand(sql, conexion);
        await using var lector = await comando.ExecuteReaderAsync();

        while (await lector.ReadAsync())
        {
            tickets.Add(new Ticket
            {
                Id = lector.GetInt32("id"),
                Titulo = lector.GetString("titulo"),
                Descripcion = lector.GetString("descripcion"),
                CategoriaId = lector.GetInt32("categoria_id"),
                CategoriaNombre = lector.GetString("categoria_nombre"),
                Estado = lector.GetString("estado"),
                FechaCreacion = lector.GetDateTime("fecha_creacion"),
                FechaCierre = lector.IsDBNull(
                    lector.GetOrdinal("fecha_cierre"))
                    ? null
                    : lector.GetDateTime("fecha_cierre")
            });
        }

        return tickets;
    }

    public async Task<Ticket?> ObtenerPorIdAsync(int id)
    {
        var tickets = await ObtenerTodosAsync();
        return tickets.FirstOrDefault(t => t.Id == id);
    }

    public async Task<List<Categoria>> ObtenerCategoriasAsync()
    {
        var categorias = new List<Categoria>();

        await using var conexion =
            new MySqlConnection(_connectionString);

        await conexion.OpenAsync();

        const string sql =
            "SELECT id, nombre FROM categorias ORDER BY nombre;";

        await using var comando = new MySqlCommand(sql, conexion);
        await using var lector = await comando.ExecuteReaderAsync();

        while (await lector.ReadAsync())
        {
            categorias.Add(new Categoria
            {
                Id = lector.GetInt32("id"),
                Nombre = lector.GetString("nombre")
            });
        }

        return categorias;
    }

    public Task<int> CrearAsync(Ticket ticket)
    {
        throw new NotImplementedException(
            "La creación de tickets todavía no está implementada.");
    }

    public Task<bool> ActualizarAsync(Ticket ticket)
    {
        throw new NotImplementedException(
            "La actualización todavía no está implementada.");
    }

    public Task<bool> CerrarAsync(int id)
    {
        throw new NotImplementedException(
            "El cierre todavía no está implementado.");
    }

    public Task<bool> EliminarAsync(int id)
    {
        throw new NotImplementedException(
            "La eliminación todavía no está implementada.");
    }
}
