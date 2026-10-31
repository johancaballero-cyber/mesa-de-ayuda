# Hito 1: implementación funcional

## Modalidad y punto de partida

Hito 1 individual de Johan Emilio Caballero. Se conserva hito-0.md sin modificaciones, incluido su contenido histórico sobre integrantes. Al iniciar esta implementación el repositorio tenía documentación, pero no un proyecto C# ejecutable.

## Cambios respecto al Hito 0 y justificación

| Propuesta | Implementación y razón |
|---|---|
| C# y Blazor, formularios y botones | C# de consola, opción admitida por el Hito 1; reduce la complejidad de interfaz y permite explicar el flujo completo. |
| Pendiente, En proceso, Resuelto | Abierto y Cerrado, vocabulario del requisito de filtros; un único avance y sin reapertura. |
| Solicitante | Se omite por no ser obligatorio; evita recopilar nombres y centra el registro en la incidencia. |
| Sin categoría o prioridad | Se agrega prioridad Baja, Media o Alta para cumplir el registro y su validación. |
| Búsqueda por número o título | Selección por ID desde el listado; búsqueda por título queda fuera de esta iteración. |
| Un modelo principal | Ticket mantiene las reglas; MesaAyuda separa colección, búsqueda, IDs y conteos de la interfaz. |

## Reglas y decisiones

Ticket rechaza ID no positivo, título o descripción vacíos o compuestos por espacios y prioridades fuera del enum. El constructor establece Abierto. Estado tiene setter privado y Cerrar devuelve false si ya está cerrado. La interfaz muestra ese resultado al usuario.

MesaAyuda conserva una única lista durante toda la sesión. El ID se incrementa después de un registro exitoso; no es el índice de la lista. Los conteos se calculan desde la colección actual, evitando mantener contadores inconsistentes. Listar crea una lista de resultados sin borrar el origen. Buscar selecciona un objeto por ID o devuelve null.

## Correcciones y comprobaciones

- Se manejan opciones, filtros e identificadores incorrectos mediante mensajes y TryParse.
- La prioridad numérica también se valida en el modelo: convertir a enum no basta para garantizar un valor permitido.
- Se recortan espacios externos de título y descripción.
- Se rechaza un cierre repetido y se muestra la lista, detalle y conteos inmediatamente después del intento.
- Se aclara la modalidad individual en README sin reescribir la propuesta original.

Validación realizada: `dotnet build` sin errores ni advertencias y `dotnet run -- --verificar`, con 12 comprobaciones correctas: colección vacía, tres registros inválidos, IDs y espacios, registro/conteos, consultas diferentes e inexistente, doble cierre, aislamiento del otro ticket, conteos posteriores, filtros y conservación de registros.

## Prueba manual reproducible

1. Listar todos al iniciar: lista vacía, conteos 0/0/0.
2. Registrar Impresora / No imprime / 2 y Red / Sin conexión / 3: IDs 1 y 2, total 2, abiertos 2.
3. Consultar ID 1 y luego ID 2: los detalles cambian.
4. Cerrar ID 1: detalle y lista indican Cerrado; ID 2 sigue Abierto; conteos 2/1/1.
5. Repetir cierre de ID 1: mensaje de rechazo, conteos iguales.
6. Listar abiertos, cerrados y todos: respectivamente ID 2, ID 1, ambos.
7. Intentar título vacío, descripción vacía y prioridad 99: no se agrega ningún ticket.
8. Probar ID abc e ID 999: mensaje de error sin interrupción.

## Trazabilidad

Se organizan tres commits relevantes: modelo y colección, interfaz y verificación, documentación y preparación oral. La identidad Git configurada es Johan29 con el correo institucional de Johan. Para modalidad individual el enunciado no exige PR. La comprobación local no sustituye confirmar que los commits estén publicados en GitHub antes del cierre.
