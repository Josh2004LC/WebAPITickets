# WebAPITickets — API REST para Gestión de Tickets

API REST desarrollada con **ASP.NET Core Web API** para administrar tickets de soporte, usuarios y catálogos relacionados.

Este proyecto funciona como backend del sistema WebAppTickets y utiliza SQL Server para almacenar la información.

## Tecnologías utilizadas

- C# y .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Swagger / OpenAPI

## Funcionalidades

- Registro, consulta, actualización y eliminación de tickets.
- Búsqueda de tickets por estado.
- Consulta de tickets por usuario asignado.
- Filtrado por urgencia.
- Actualización del estado de un ticket.
- Gestión de usuarios, roles, categorías, urgencias e importancias.
- Persistencia de datos mediante Entity Framework Core.

## Endpoints principales

| Método | Endpoint | Función |
|---|---|---|
| GET | `/api/Tiquetes` | Consultar tickets |
| GET | `/api/Tiquetes/{id}` | Consultar un ticket |
| POST | `/api/Tiquetes` | Crear un ticket |
| PUT | `/api/Tiquetes/{id}` | Actualizar un ticket |
| DELETE | `/api/Tiquetes/{id}` | Eliminar un ticket |
| GET | `/api/Tiquetes/Estado/{estado}` | Filtrar por estado |
| PATCH | `/api/Tiquetes/{id}/CambiarEstado` | Cambiar el estado |

## Estructura del proyecto

- `Controllers`: endpoints de la API.
- `Models`: entidades del sistema.
- `Database`: contexto de Entity Framework Core y script SQL.
- `Program.cs`: configuración de servicios y aplicación.

## Instalación

1. Clonar el repositorio.
2. Instalar .NET 8 y SQL Server.
3. Revisar y ejecutar el script `Database/Base_Datos_Scripts.sql`.
4. Configurar `ConnectionStrings:Conexion` para la instancia local de SQL Server, sin publicar credenciales.
5. Ejecutar:

```bash
dotnet restore
dotnet run
```

6. Acceder a `/swagger` en el entorno de desarrollo para explorar los endpoints.

## Mejoras futuras

- Implementar autenticación y autorización en la API.
- Agregar pruebas de integración.
- Fortalecer las validaciones.
- Documentar las respuestas y errores de los endpoints.

## Frontend

[WebAppTickets](https://github.com/Josh2004LC/WebAppTickets)

## Estado del proyecto

API desarrollada como proyecto académico y en proceso de mejoras para su presentación en un portafolio profesional.
