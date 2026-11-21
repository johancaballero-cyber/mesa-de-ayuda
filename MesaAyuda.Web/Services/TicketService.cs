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
        await using var conexion =
            new MySqlConnection(_connectionString);

        await conexion.OpenAsync();

        const string sql = """
            SELECT t.id, t.titulo, t.descripcion,
                   t.categoria_id, c.nombre AS categoria_nombre,
                   t.estado, t.fecha_creacion, t.fecha_cierre
            FROM tickets t
            INNER JOIN categorias c ON t.categoria_id = c.id
            WHERE t.id = @id;
            """;

        await using var comando = new MySqlCommand(sql, conexion);
        comando.Parameters.AddWithValue("@id", id);

        await using var lector = await comando.ExecuteReaderAsync();

        if (!await lector.ReadAsync())
        {
            return null;
        }

        return new Ticket
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
        };
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

    private static void Validar(Ticket ticket)
    {
        if (string.IsNullOrWhiteSpace(ticket.Titulo))
            throw new ArgumentException("El título no puede estar vacío.");

        if (ticket.Titulo.Trim().Length > 150)
            throw new ArgumentException(
                "El título no puede superar los 150 caracteres.");

        if (string.IsNullOrWhiteSpace(ticket.Descripcion))
            throw new ArgumentException(
                "La descripción no puede estar vacía.");
    }

    private static async Task<bool> CategoriaExisteAsync(
        MySqlConnection conexion, int categoriaId)
    {
        const string sql =
            "SELECT COUNT(*) FROM categorias WHERE id = @id;";

        await using var comando = new MySqlCommand(sql, conexion);
        comando.Parameters.AddWithValue("@id", categoriaId);

        var total = Convert.ToInt32(await comando.ExecuteScalarAsync());
        return total > 0;
    }

    public async Task<int> CrearAsync(Ticket ticket)
    {
        Validar(ticket);

        await using var conexion =
            new MySqlConnection(_connectionString);

        await conexion.OpenAsync();

        if (!await CategoriaExisteAsync(conexion, ticket.CategoriaId))
            throw new ArgumentException("La categoría seleccionada no existe.");

        const string sql = """
            INSERT INTO tickets (titulo, descripcion, categoria_id, estado)
            VALUES (@titulo, @descripcion, @categoriaId, 'Abierto');
            SELECT LAST_INSERT_ID();
            """;

        await using var comando = new MySqlCommand(sql, conexion);
        comando.Parameters.AddWithValue("@titulo", ticket.Titulo.Trim());
        comando.Parameters.AddWithValue(
            "@descripcion", ticket.Descripcion.Trim());
        comando.Parameters.AddWithValue("@categoriaId", ticket.CategoriaId);

        return Convert.ToInt32(await comando.ExecuteScalarAsync());
    }

    public async Task<bool> ActualizarAsync(Ticket ticket)
    {
        Validar(ticket);

        await using var conexion =
            new MySqlConnection(_connectionString);

        await conexion.OpenAsync();

        if (!await CategoriaExisteAsync(conexion, ticket.CategoriaId))
            throw new ArgumentException("La categoría seleccionada no existe.");

        const string sql = """
            UPDATE tickets
            SET titulo = @titulo,
                descripcion = @descripcion,
                categoria_id = @categoriaId
            WHERE id = @id;
            """;

        await using var comando = new MySqlCommand(sql, conexion);
        comando.Parameters.AddWithValue("@titulo", ticket.Titulo.Trim());
        comando.Parameters.AddWithValue(
            "@descripcion", ticket.Descripcion.Trim());
        comando.Parameters.AddWithValue("@categoriaId", ticket.CategoriaId);
        comando.Parameters.AddWithValue("@id", ticket.Id);

        // Se usa "Found rows" implícito: si los valores no cambian MySqlConnector
        // reporta 0 filas, por eso se verifica la existencia aparte.
        var filas = await comando.ExecuteNonQueryAsync();
        if (filas > 0) return true;

        const string existe = "SELECT COUNT(*) FROM tickets WHERE id = @id;";
        await using var consulta = new MySqlCommand(existe, conexion);
        consulta.Parameters.AddWithValue("@id", ticket.Id);
        return Convert.ToInt32(await consulta.ExecuteScalarAsync()) > 0;
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
