using MesaDeAyuda;

internal static class Verificacion
{
    public static void Ejecutar()
    {
        int comprobaciones = 0;
        void Exigir(bool condicion, string caso)
        {
            if (!condicion) throw new Exception($"FALLO: {caso}");
            comprobaciones++;
            Console.WriteLine($"OK: {caso}");
        }
        var mesa = new MesaAyuda();
        Exigir(mesa.Total == 0 && mesa.Listar().Count == 0, "Inicio vacío");
        foreach (var datos in new[] { (" ", "Descripción", Prioridad.Baja), ("Título", "", Prioridad.Media), ("Título", "Descripción", (Prioridad)99) })
        {
            bool rechazo = false;
            try { mesa.Registrar(datos.Item1, datos.Item2, datos.Item3); }
            catch (ArgumentException) { rechazo = true; }
            Exigir(rechazo && mesa.Total == 0, "Rechazo de registro inválido sin agregar datos");
        }
        var primero = mesa.Registrar(" Impresora ", "No imprime", Prioridad.Media);
        var segundo = mesa.Registrar("Red", "Sin conexión", Prioridad.Alta);
        Exigir(primero.Id == 1 && segundo.Id == 2 && primero.Titulo == "Impresora", "IDs únicos y limpieza de espacios");
        Exigir(mesa.Total == 2 && mesa.Abiertos == 2 && mesa.Cerrados == 0, "Registro y conteos iniciales");
        Exigir(mesa.Buscar(1) == primero && mesa.Buscar(2) == segundo && mesa.Buscar(999) == null, "Selección de distintos tickets y búsqueda inexistente");
        Exigir(primero.Cerrar() && !primero.Cerrar(), "Cierre y rechazo del segundo cierre");
        Exigir(segundo.Estado == EstadoTicket.Abierto, "El otro ticket conserva su estado");
        Exigir(mesa.Total == 2 && mesa.Abiertos == 1 && mesa.Cerrados == 1, "Conteos actualizados");
        Exigir(mesa.Listar(EstadoTicket.Abierto).Single() == segundo && mesa.Listar(EstadoTicket.Cerrado).Single() == primero, "Filtros por estado");
        mesa.Listar().Clear();
        Exigir(mesa.Total == 2 && mesa.Listar().Count == 2, "Filtrar y modificar la lista de resultados no borra registros");
        Console.WriteLine($"Verificación finalizada: {comprobaciones} comprobaciones correctas.");
    }
}
