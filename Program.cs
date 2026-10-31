using MesaDeAyuda;

if (args.Contains("--verificar"))
{
    Verificacion.Ejecutar();
    return;
}

var mesa = new MesaAyuda();
bool continuar = true;
while (continuar)
{
    Console.WriteLine("\nSISTEMA DE MESA DE AYUDA");
    MostrarConteos();
    Console.WriteLine("1. Registrar ticket\n2. Listar y filtrar\n3. Consultar detalle\n4. Cerrar ticket\n0. Salir");
    Console.Write("Opción: ");
    string? opcion = Console.ReadLine();
    if (opcion is null) break;
    switch (opcion)
    {
        case "1": Registrar(); break;
        case "2": Listar(); break;
        case "3": Consultar(); break;
        case "4": Cerrar(); break;
        case "0": continuar = false; break;
        default: Console.WriteLine("Opción inválida. Elija de 0 a 4."); break;
    }
}

void MostrarConteos() => Console.WriteLine(
    $"Total: {mesa.Total} | Abiertos: {mesa.Abiertos} | Cerrados: {mesa.Cerrados}");

void Registrar()
{
    Console.Write("Título: ");
    string titulo = Console.ReadLine() ?? "";
    Console.Write("Descripción: ");
    string descripcion = Console.ReadLine() ?? "";
    Console.Write("Prioridad (1 Baja, 2 Media, 3 Alta): ");
    if (!int.TryParse(Console.ReadLine(), out int numero))
    {
        Console.WriteLine("Prioridad inválida: ingrese 1, 2 o 3. No se registró el ticket.");
        return;
    }
    try
    {
        var ticket = mesa.Registrar(titulo, descripcion, (Prioridad)numero);
        Console.WriteLine($"Ticket registrado correctamente. ID: {ticket.Id}");
        MostrarConteos();
    }
    catch (ArgumentException error)
    {
        Console.WriteLine($"{error.Message} No se registró el ticket; vuelva a la opción 1 para corregirlo.");
    }
}

void Listar()
{
    Console.Write("Filtro (0 Todos, 1 Abiertos, 2 Cerrados): ");
    string? filtro = Console.ReadLine();
    if (filtro != "0" && filtro != "1" && filtro != "2")
    {
        Console.WriteLine("Filtro inválido: use 0, 1 o 2.");
        return;
    }
    EstadoTicket? estado = filtro == "0" ? null :
        filtro == "1" ? EstadoTicket.Abierto : EstadoTicket.Cerrado;
    var resultado = mesa.Listar(estado);
    Console.WriteLine("ID | Título | Estado");
    foreach (var ticket in resultado)
        Console.WriteLine($"{ticket.Id} | {ticket.Titulo} | {ticket.Estado}");
    if (resultado.Count == 0) Console.WriteLine("No se encontraron tickets.");
    MostrarConteos();
}

Ticket? Seleccionar()
{
    Console.Write("ID del ticket (consulte los ID en la opción 2): ");
    if (!int.TryParse(Console.ReadLine(), out int id) || id <= 0)
    {
        Console.WriteLine("Ingrese un identificador numérico positivo.");
        return null;
    }
    var ticket = mesa.Buscar(id);
    if (ticket is null) Console.WriteLine("No se encontró el ticket solicitado.");
    return ticket;
}

void MostrarDetalle(Ticket ticket)
{
    Console.WriteLine($"\nID: {ticket.Id}\nTítulo: {ticket.Titulo}\nDescripción: {ticket.Descripcion}");
    Console.WriteLine($"Prioridad: {ticket.Prioridad}\nFecha: {ticket.FechaCreacion:dd/MM/yyyy HH:mm:ss}\nEstado: {ticket.Estado}");
}

void Consultar()
{
    var ticket = Seleccionar();
    if (ticket is not null) MostrarDetalle(ticket);
}

void Cerrar()
{
    var ticket = Seleccionar();
    if (ticket is null) return;
    Console.WriteLine(ticket.Cerrar() ? "Estado actualizado correctamente." : "El ticket ya está cerrado; no se puede cerrar otra vez.");
    MostrarDetalle(ticket);
    Console.WriteLine("Listado actualizado:");
    foreach (var item in mesa.Listar())
        Console.WriteLine($"{item.Id} | {item.Titulo} | {item.Estado}");
    MostrarConteos();
}
