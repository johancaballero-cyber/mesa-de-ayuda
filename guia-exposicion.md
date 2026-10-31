# Guía para comprender y exponer el proyecto

## Paso 1. Abre y ejecuta antes de estudiar

Abre mesa-de-ayuda.sln en Visual Studio Community. En el Explorador de soluciones están los cuatro archivos C#. Compila con Ctrl+Mayús+B y ejecuta con Ctrl+F5. No ejecutes el archivo cs suelto: abre la solución. Sigue primero la prueba manual de hito-1.md para reconocer lo que hace cada opción.

## Paso 2. Comprende Ticket.cs

Una clase es un molde; cada objeto Ticket representa una incidencia distinta. Sus propiedades son los datos de esa incidencia. `int` guarda números enteros, `string` texto y `DateTime` una fecha con hora.

`enum` define nombres para las opciones permitidas. Prioridad tiene Baja=1, Media=2, Alta=3. EstadoTicket tiene Abierto y Cerrado. No son textos escritos libremente por el usuario.

El constructor `public Ticket(...)` se ejecuta al crear un objeto con `new Ticket(...)`. Recibe los datos, los valida y asigna las propiedades. `string.IsNullOrWhiteSpace` detecta null, texto vacío y solo espacios. `throw new ArgumentException` impide construir un ticket inválido y comunica la razón. `Enum.IsDefined` comprueba que la prioridad exista, porque una conversión como `(Prioridad)99` es posible aunque 99 no esté definido.

`get` permite leer. Las propiedades sin setter no pueden reasignarse después del constructor. `Estado { get; private set; }` permite leer el estado desde fuera, pero modificarlo solamente dentro de Ticket. Esto obliga a pasar por Cerrar().

Cerrar devuelve un bool: false si ya estaba cerrado; true después de cambiarlo. `return` termina el método. La regla vive en el modelo para que también proteja el ticket si más adelante se cambia la interfaz.

## Paso 3. Comprende MesaAyuda.cs

`List<Ticket>` es una colección de objetos Ticket. La lista se crea una sola vez y permanece en MesaAyuda durante la sesión. `private` evita que la interfaz acceda directamente a ella. `readonly` impide reemplazar la referencia de la lista; todavía permite agregar objetos.

Registrar construye un ticket válido, lo agrega y aumenta siguienteId con `++`. Si el constructor falla, no se agrega ni se aumenta el ID. El identificador es un dato estable del objeto, no su posición en una lista.

Buscar usa `FirstOrDefault`: encuentra la primera coincidencia por ID o devuelve null. El signo `?` en `Ticket?` avisa que puede no existir un resultado.

`t => t.Id == id` significa «para cada ticket t, compara su ID con el solicitado». `==` compara y `=` asigna. Where selecciona los que cumplen una condición. ToList crea una colección de resultados; no elimina elementos de la colección original. Los objetos dentro de ambas listas siguen siendo los mismos tickets.

`EstadoTicket? estado = null` permite pedir todos sin especificar estado. `||` significa «o»: el ticket pasa si no se pidió filtro o si coincide con él.

Total, Abiertos y Cerrados se calculan cada vez que se consultan. Count cuenta elementos; Count con condición cuenta solo las coincidencias. La expresión `=>` de una propiedad devuelve el resultado indicado.

## Paso 4. Comprende Program.cs

Las instrucciones de nivel superior son el punto de entrada del programa. Se crea una sola MesaAyuda antes del while; crearla dentro del ciclo perdería los tickets en cada vuelta.

El while repite el menú mientras continuar sea true. switch elige una acción según el número escrito. Console.ReadLine lee una línea; Console.WriteLine muestra texto. Si termina la entrada (null), el programa sale del ciclo.

Registrar recoge datos. TryParse intenta convertir texto a entero sin lanzar una excepción; out entrega el número convertido. El try intenta registrar y catch muestra el mensaje de validación del modelo. `?? ""` usa texto vacío si la lectura devolvió null.

Seleccionar centraliza la lectura del ID para que Consultar y Cerrar no dupliquen esa lógica. Si no existe un ticket devuelve null; los métodos comprueban el resultado antes de usarlo.

MostrarDetalle recibe el ticket elegido. No guarda una selección anterior: por eso consultar otro ID muestra otro objeto. Cerrar modifica ese objeto y vuelve a mostrar detalle, listado y conteos.

`$"...{valor}..."` inserta un valor dentro de texto. `condicion ? valorSiCierto : valorSiFalso` elige uno de dos resultados. No cambia por sí mismo los datos.

## Paso 5. Sigue un ticket de principio a fin

Opción 1 -> Registrar de Program -> Registrar de MesaAyuda -> constructor Ticket -> Add a la lista -> mostrar ID.

Opción 3 -> Seleccionar -> Buscar por ID -> MostrarDetalle.

Opción 4 -> Seleccionar -> Buscar -> Ticket.Cerrar -> mostrar resultado -> MostrarDetalle -> Listar y conteos calculados.

Coloca un punto de interrupción haciendo clic en el margen junto a `var ticket = new Ticket(...)` en MesaAyuda.cs. Ejecuta con F5 y registra un ticket. F11 entra al constructor; F10 avanza una instrucción y F5 continúa. Observa las variables en la ventana de depuración. Practica esto antes de la exposición; no necesitas depurar en vivo si ya puedes explicarlo.

## Guion de 15 minutos

### Minutos 0 a 5: demostración

- 0:00-0:30: muestra lista vacía y conteos.
- 0:30-1:30: registra Impresora / No imprime / 2 y Red / Sin conexión / 3.
- 1:30-2:15: lista ambos y consulta los dos IDs por separado.
- 2:15-3:15: cierra el primero, muestra lista y detalle actualizados y rechaza otro cierre.
- 3:15-4:00: filtra abiertos, cerrados y todos.
- 4:00-5:00: intenta título vacío y prioridad 99; muestra que no aumentó el total. Si hay tiempo prueba un ID inexistente.

### Minutos 5 a 9: explicación en palabras propias

- Un minuto en Ticket: datos, constructor, validaciones y setter privado.
- Un minuto en MesaAyuda: colección única, IDs y búsqueda.
- Un minuto en Program: menú, registro, selección y cierre.
- Un minuto en filtros, conteos y cambios respecto al Hito 0. Explica que consola está permitida y que no hay persistencia tras salir.

### Minutos 9 a 15: preguntas individuales

Responde mostrando el método correspondiente. Practica explicar la idea sin recitar los nombres solamente.

## Preguntas probables y respuestas

1. **¿Qué diferencia hay entre clase y objeto?** Ticket define la estructura; cada `new Ticket` crea una incidencia con datos propios.
2. **¿Dónde se valida?** En el constructor del modelo; la interfaz también comprueba que la entrada numérica se pueda interpretar.
3. **¿Por qué validar en el modelo?** Para impedir objetos inválidos aunque se utilice otra interfaz en el futuro.
4. **¿Cómo evitas cerrar dos veces?** Cerrar comprueba Estado y devuelve false antes de modificarlo si ya está Cerrado.
5. **¿Por qué no escribes Estado desde Program?** Su setter es privado: el cambio pasa por la regla del modelo.
6. **¿Cómo sabes que no cambia otro ticket?** Buscar devuelve solo el objeto cuyo ID coincide y Cerrar actúa sobre ese objeto.
7. **¿Dónde están guardados los datos?** En una List dentro de MesaAyuda, en memoria. Al terminar el proceso se pierden.
8. **¿Por qué la lista no se reinicia al volver al menú?** MesaAyuda se crea antes del while y se reutiliza en cada vuelta.
9. **¿Filtrar elimina tickets?** No; Where y ToList producen resultados a partir de la lista original sin borrarla.
10. **¿Cómo se actualizan los conteos?** Se calculan con Count al mostrarlos, usando el estado actual de los tickets.
11. **¿Qué pasa con un ID inexistente?** Buscar devuelve null, se informa al usuario y no se intenta modificar nada.
12. **¿Por qué TryParse en vez de Parse?** La entrada incorrecta es esperable; TryParse devuelve false y permite dar un mensaje sin interrumpir el programa.
13. **¿Un enum impide por sí solo el valor 99?** No. Una conversión numérica puede producirlo; Enum.IsDefined lo rechaza.
14. **¿Qué es null?** La ausencia de un objeto o valor; aquí representa una búsqueda sin resultado o un filtro sin estado.
15. **¿Qué pasa si el registro falla?** Se captura ArgumentException, se muestra qué corregir y no se agrega ningún ticket ni avanza el ID.
16. **¿Qué cambió desde el Hito 0?** Consola en lugar de Blazor, Abierto/Cerrado en lugar de tres estados, prioridad obligatoria y sin solicitante ni búsqueda por título. Está justificado en hito-1.md.
17. **¿Por qué hito-0 menciona a otra persona?** Se conserva el documento histórico; el Hito 1 es individual, como aclaran README y hito-1.
18. **¿Cómo probaste el programa?** Compilación, verificación automática de reglas y recorrido del menú. Verificacion.cs crea una mesa de prueba independiente.
19. **¿Qué mejorarías después?** Persistencia, interfaz web y búsqueda por título, manteniendo las reglas dentro de Ticket.
20. **¿Qué demuestra Git?** Los commits registran cambios relevantes con autor y fecha. En individual se exigen al menos tres; no se exige PR.

## Antes de entregar

Comprueba que los tres commits nuevos están en GitHub, que hito-0.md sigue presente y que los enlaces de README funcionan. Entrega solamente el enlace al repositorio en Moodle antes del mediodía del 31 de octubre de 2026. Ensaya una vez sin leer esta guía y dedica el tiempo restante a las partes que no puedas explicar todavía.
