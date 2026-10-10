# Hito 0: Propuesta y modelo inicial

**Proyecto:** Sistema de Mesa de Ayuda  
**Estudiante:** Johan Emilio Caballero, Luis Quintero  
**Fecha:** 9 de octubre de 2026

## 1. Situación y usuario

Actualmente, en muchas oficinas y pequeños negocios se utilizan computadoras, impresoras y conexiones a internet para realizar las actividades diarias. Sin embargo, estos equipos pueden presentar problemas que necesitan ser atendidos por una persona encargada del soporte técnico.

Normalmente, cuando ocurre un problema, los trabajadores lo comunican mediante llamadas telefónicas, mensajes de WhatsApp o directamente al encargado. Aunque estas formas de comunicación pueden ser útiles, también pueden causar desorden cuando varias personas necesitan ayuda al mismo tiempo.

Por ejemplo, imaginemos una oficina donde un trabajador reporta que su computadora está lenta, otro informa que la impresora no funciona y otra persona tiene problemas con la conexión a internet. Si todas estas solicitudes llegan por diferentes medios, puede ser complicado recordar cuáles se atendieron y cuáles siguen pendientes.

Uno de los principales problemas es que no existe un lugar donde se guarde toda la información de las solicitudes. Esto provoca que algunas puedan olvidarse o que el encargado tenga que preguntar nuevamente cuál era el problema. Además, cuando alguien quiere saber cómo va su solicitud, debe comunicarse otra vez con el personal de soporte.

También puede suceder que una misma computadora presente el mismo problema varias veces. Si no se guarda un registro, resulta difícil saber qué ocurrió anteriormente o qué solución se utilizó.

Para mejorar esta situación, propongo desarrollar un sistema de mesa de ayuda que permita registrar y organizar las solicitudes de soporte técnico mediante tickets. Cada ticket representará un problema reportado por un usuario y tendrá información como un número de identificación, título, descripción, fecha de creación y estado.

El sistema estaría dirigido principalmente al personal encargado de soporte técnico de una oficina o pequeño negocio. Su función sería facilitar el registro de problemas y permitir que el encargado consulte las solicitudes pendientes sin necesidad de revisar mensajes o anotaciones.

También sería útil para las personas que reportan los problemas, porque podrían conocer si una solicitud está pendiente, en proceso o resuelta.

La aplicación tendría una interfaz sencilla y fácil de entender. En la pantalla principal se mostraría una lista de los tickets registrados, donde se podría identificar rápidamente cada problema y su estado actual.

Cuando se necesite registrar una nueva solicitud, el usuario completaría un formulario con la información básica del problema. Después, el encargado podría seleccionar el ticket para consultar sus detalles o actualizar su estado.

Con este proyecto busco que las solicitudes de soporte estén mejor organizadas, evitar que se olviden problemas reportados y facilitar el seguimiento de cada situación.

En esta primera etapa solamente se realizará la propuesta y el modelo inicial. Más adelante se comenzará a desarrollar la aplicación utilizando C# y Blazor, aplicando los conocimientos adquiridos durante el curso.

## 2. Acciones y resultados

El sistema permitirá realizar cuatro acciones principales relacionadas con los tickets.

### Registrar un ticket

El usuario ingresará el título del problema, una descripción y el nombre de quien realiza la solicitud. Al presionar el botón "Guardar", el sistema verificará que los campos estén completos.

Si los datos son correctos, el ticket se registrará con un número único, la fecha actual y el estado "Pendiente".

**Resultado:** Se mostrará el mensaje "Ticket registrado correctamente" y el nuevo ticket aparecerá en la lista.

### Encontrar un ticket en la lista

En la pantalla principal se mostrarán los tickets registrados mediante una tabla con su número, título, fecha y estado.

También se podrá utilizar un buscador para encontrar un ticket por su número o título.

**Resultado:** Aparecerán los tickets que coincidan con la búsqueda. Si no existe ninguno, se mostrará "No se encontraron tickets".

### Consultar un ticket

El usuario seleccionará un ticket de la lista mediante el botón "Ver detalles".

**Resultado:** El sistema mostrará el número, título, descripción, solicitante, fecha de creación y estado del ticket seleccionado.

### Cambiar el estado de un ticket

El encargado podrá cambiar el estado de un ticket dependiendo del avance de la solicitud.

Los estados serán:

- Pendiente
- En proceso
- Resuelto

**Resultado:** El sistema guardará el cambio y mostrará "Estado actualizado correctamente". También actualizará el estado que aparece en la lista.

## 3. Clases iniciales

Para comenzar, utilizaré una clase principal llamada `Ticket`, que representará cada solicitud registrada.

### Clase Ticket

**Atributos:**

- `Id`: número único que identifica el ticket.
- `Titulo`: nombre corto del problema.
- `Descripcion`: explicación del problema reportado.
- `Solicitante`: nombre de la persona que reporta.
- `FechaCreacion`: fecha en que se creó el ticket.
- `Estado`: situación actual de la solicitud.

**Métodos:**

- `ValidarDatos()`: comprueba que el título, la descripción y el solicitante estén completos.
- `CambiarEstado()`: permite actualizar el estado del ticket cuando el cambio es válido.

### Diagrama de la clase

```text
+-----------------------------+
|           Ticket            |
+-----------------------------+
| Id                          |
| Titulo                      |
| Descripcion                 |
| Solicitante                 |
| FechaCreacion               |
| Estado                      |
+-----------------------------+
| ValidarDatos()              |
| CambiarEstado()             |
+-----------------------------+
```

### Responsabilidad

La clase Ticket tendrá la responsabilidad de guardar la información de cada solicitud y validar los cambios que se realicen.

Un ticket nuevo comenzará con el estado "Pendiente". Después podrá pasar a "En proceso" y finalmente a "Resuelto".

Por el momento no considero necesario agregar otra clase, ya que el objetivo es mantener el modelo sencillo durante esta primera etapa.

## 4. Reglas del sistema

### Regla 1: El título es obligatorio

No se podrá registrar un ticket sin un título.

**Entrada válida:**  
Título: "La impresora no funciona".

**Respuesta:**  
"Ticket registrado correctamente", siempre que los demás campos también estén completos.

**Entrada inválida:**  
Título: vacío.

**Respuesta:**  
"Debe ingresar un título para el ticket".

### Regla 2: La descripción es obligatoria

Cada ticket debe tener una descripción que explique el problema.

**Entrada válida:**  
Descripción: "La computadora enciende, pero no muestra imagen".

**Respuesta:**  
"Ticket registrado correctamente", si los demás campos son válidos.

**Entrada inválida:**  
Descripción: vacío.

**Respuesta:**  
"Debe ingresar una descripción del problema".

### Regla 3: El estado debe ser válido

Solo se aceptarán los estados Pendiente, En proceso y Resuelto.

**Entrada válida:**  
Cambiar de "Pendiente" a "En proceso".

**Respuesta:**  
"Estado actualizado correctamente".

**Entrada inválida:**  
Cambiar el estado a "Cancelado".

**Respuesta:**  
"El estado seleccionado no es válido".

### Regla 4: El ticket debe existir

Para consultar o modificar un ticket, este debe estar registrado.

**Entrada válida:**  
Buscar el ticket número 3, que existe en la lista.

**Respuesta:**  
El sistema mostrará la información del ticket.

**Entrada inválida:**  
Buscar el ticket número 999, que no está registrado.

**Respuesta:**  
"No se encontró el ticket solicitado".

## 5. Boceto de interfaz

La aplicación tendrá tres pantallas principales.

### Pantalla principal

```text
+-------------------------------------------+
|          SISTEMA DE MESA DE AYUDA          |
+-------------------------------------------+
|                                           |
| [Nuevo Ticket]                            |
|                                           |
| Buscar: [____________] [Buscar]           |
|                                           |
| ID | Título       | Estado    | Acción    |
|----|--------------|-----------|-----------|
| 1  | Impresora    | Pendiente | [Ver]     |
| 2  | Internet     | En proceso| [Ver]     |
| 3  | Computadora  | Resuelto  | [Ver]     |
|                                           |
+-------------------------------------------+
```

### Formulario de registro

```text
+-------------------------------------------+
|             NUEVO TICKET                  |
+-------------------------------------------+
|                                           |
| Título:                                   |
| [________________________________]        |
|                                           |
| Solicitante:                              |
| [________________________________]        |
|                                           |
| Descripción:                              |
| [________________________________]        |
| [________________________________]        |
|                                           |
| [Guardar]          [Cancelar]             |
|                                           |
| Mensaje:                                  |
| Debe completar los campos obligatorios.   |
+-------------------------------------------+
```

### Pantalla de detalles

```text
+-------------------------------------------+
|             DETALLE DEL TICKET            |
+-------------------------------------------+
|                                           |
| ID: 1                                     |
| Título: Impresora no funciona             |
| Solicitante: Usuario de prueba            |
| Fecha: 09/10/2026                         |
|                                           |
| Descripción:                              |
| La impresora no responde al imprimir.     |
|                                           |
| Estado actual: Pendiente                  |
|                                           |
| Cambiar estado:                           |
| [En proceso             v]                |
|                                           |
| [Actualizar estado]     [Volver]          |
|                                           |
| Mensaje: Estado actualizado correctamente |
+-------------------------------------------+
```

Estos bocetos muestran cómo estarán organizados los elementos de la aplicación. Durante el desarrollo se podrá mejorar su diseño.

## 6. Pregunta y próxima acción

**Pregunta para el docente:**

¿En la primera versión podemos guardar los tickets temporalmente en una lista dentro del programa o será necesario conectarlos desde el inicio a una base de datos?

**Próxima acción:**

Crear el proyecto inicial utilizando C# y Blazor. Después definir la clase Ticket y desarrollar una pantalla que muestre una lista con solicitudes de ejemplo.

## 7. Organización del trabajo

Como realizaré este proyecto de manera individual, organizaré el tiempo de trabajo en tres partes.

### Modelo del sistema (40%)

Definiré la clase Ticket, sus atributos, métodos y reglas de validación.

**Evidencia prevista:** Estructura de la clase Ticket y ejemplos de validación.

### Diseño de interfaz (35%)

Prepararé los bocetos de las pantallas principales, el formulario para registrar tickets y la pantalla de detalles.

**Evidencia prevista:** Bocetos de las pantallas y distribución de botones, campos y mensajes.

### Revisión y pruebas (25%)

Revisaré que la documentación esté completa y comprobaré mediante ejemplos que las reglas propuestas tengan resultados correctos.

**Evidencia prevista:** Ejemplos de entradas válidas e inválidas y sus respuestas esperadas.

## Conclusión

Este proyecto busca mejorar la organización de las solicitudes de soporte técnico mediante una aplicación sencilla que permita registrar, consultar y actualizar tickets.

La propuesta servirá como base para desarrollar la primera versión del sistema utilizando C# y Blazor.
