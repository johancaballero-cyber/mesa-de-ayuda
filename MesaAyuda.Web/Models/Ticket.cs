
namespace MesaAyuda.Web.Models;

public class Ticket
{
    public int Id { get; set; }

    public string Titulo { get; set; } = string.Empty;

    public string Descripcion { get; set; } = string.Empty;

    public int CategoriaId { get; set; }

    public string CategoriaNombre { get; set; } = string.Empty;

    public string Estado { get; set; } = "Abierto";

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaCierre { get; set; }

    public bool EstaCerrado => Estado == "Cerrado";
}
