namespace MesaDeAyuda;

public class MesaAyuda
{
    private readonly List<Ticket> tickets = new();
    private int siguienteId = 1;

    public int Total => tickets.Count;
    public int Abiertos => tickets.Count(t => t.Estado == EstadoTicket.Abierto);
    public int Cerrados => tickets.Count(t => t.Estado == EstadoTicket.Cerrado);

    public Ticket Registrar(string titulo, string descripcion, Prioridad prioridad)
    {
        // El contador avanza solamente cuando el modelo acepta los datos.
        var ticket = new Ticket(siguienteId, titulo, descripcion, prioridad);
        tickets.Add(ticket);
        siguienteId++;
        return ticket;
    }

    public Ticket? Buscar(int id) => tickets.FirstOrDefault(t => t.Id == id);

    public List<Ticket> Listar(EstadoTicket? estado = null) =>
        tickets.Where(t => estado == null || t.Estado == estado).ToList();
}
