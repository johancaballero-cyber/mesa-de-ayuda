namespace MesaDeAyuda;

public enum Prioridad { Baja = 1, Media = 2, Alta = 3 }
public enum EstadoTicket { Abierto, Cerrado }

public class Ticket
{
    public int Id { get; }
    public string Titulo { get; }
    public string Descripcion { get; }
    public Prioridad Prioridad { get; }
    public DateTime FechaCreacion { get; }
    public EstadoTicket Estado { get; private set; }

    public Ticket(int id, string titulo, string descripcion, Prioridad prioridad)
    {
        if (id <= 0) throw new ArgumentException("El identificador debe ser positivo.");
        if (string.IsNullOrWhiteSpace(titulo))
            throw new ArgumentException("Debe ingresar un título.");
        if (string.IsNullOrWhiteSpace(descripcion))
            throw new ArgumentException("Debe ingresar una descripción.");
        if (!Enum.IsDefined(prioridad))
            throw new ArgumentException("Prioridad inválida: use 1 Baja, 2 Media o 3 Alta.");
        Id = id;
        Titulo = titulo.Trim();
        Descripcion = descripcion.Trim();
        Prioridad = prioridad;
        FechaCreacion = DateTime.Now;
        Estado = EstadoTicket.Abierto;
    }

    public bool Cerrar()
    {
        if (Estado == EstadoTicket.Cerrado) return false;
        Estado = EstadoTicket.Cerrado;
        return true;
    }
}
