# Sistema de Mesa de Ayuda - Hito 1

Autor del Hito 1: Johan Emilio Caballero. Modalidad individual.

## Requisitos y ejecución

- Windows con SDK .NET 10.
- Visual Studio Community compatible con .NET 10 y las herramientas de desarrollo .NET.
- No requiere base de datos, paquetes externos ni conexión a internet durante el uso.

1. Descarga o clona este repositorio y abre `mesa-de-ayuda.sln` en Visual Studio Community.
2. Si Visual Studio solicita instalar el SDK o actualizarse para .NET 10, completa esa instalación.
3. En el Explorador de soluciones, selecciona el proyecto mesa-de-ayuda como proyecto de inicio.
4. Presiona Ctrl+Mayús+B para compilar y Ctrl+F5 para ejecutar sin depurar.
5. Usa los números del menú y Enter; elige 0 para salir.

Desde una terminal en esta carpeta: `dotnet run`.
Para las comprobaciones automáticas: `dotnet run -- --verificar`.

## Funciones

1. Registrar: título y descripción obligatorios; prioridad 1 Baja, 2 Media o 3 Alta. ID automático y estado Abierto.
2. Listar: muestra ID, título y estado. Filtra 0 Todos, 1 Abiertos o 2 Cerrados.
3. Consultar: escribe el ID observado en la lista para ver toda la información del ticket.
4. Cerrar: escribe el ID; muestra inmediatamente detalle, lista y conteos actualizados. Rechaza un segundo cierre.

El encabezado muestra total, abiertos y cerrados. Los filtros no alteran los datos.

## Organización

- `Ticket.cs`: modelo, prioridades, estados, validación y regla de cierre.
- `MesaAyuda.cs`: lista en memoria, IDs, búsquedas, filtros y conteos.
- `Program.cs`: interacción con el usuario.
- `Verificacion.cs`: comprobaciones reproducibles sin librerías adicionales.

## Limitaciones conocidas

Los datos se conservan solamente mientras el programa está abierto. Al salir se pierden; es el alcance en memoria del Hito 1. No hay autenticación, base de datos, edición, eliminación, reapertura ni estado intermedio. La interfaz es de consola y no incluye búsqueda por título ni solicitante. La fecha usa la hora local del equipo. Un registro rechazado debe ingresarse nuevamente desde la opción 1.

## Documentación

- [Hito 0: propuesta original conservada](hito-0.md)
- [Hito 1: cambios, correcciones y validación](hito-1.md)
- [Guía paso a paso y preparación oral](guia-exposicion.md)

La propuesta original menciona a Luis Quintero. Se conserva como antecedente; el Hito 1 se realiza individualmente por Johan.
