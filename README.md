# VehicleFinder

## Descripción de la solución

VehicleFinder es una aplicación desarrollada en **C# .NET 8** que permite buscar vehículos disponibles en diferentes ubicaciones y crear reservas de forma segura.  
El proyecto está diseñado con **arquitectura Clean Architecture / CQRS**, separando claramente los **Commands, Queries, Handlers, Domain e Infrastructure**, lo que facilita pruebas unitarias e integración.

**Funcionalidades principales:**

- Buscar vehículos disponibles por ubicación y tipo.
- Crear reservas de vehículos con validación de fechas.
- Eventos de dominio (`VehicleReservedEvent`) que se manejan de forma interna.
- Pruebas unitarias e integración para garantizar calidad del código.

---

## Decisiones técnicas tomadas

### Arquitectura y patrones

- Se usa **CQRS** con **MediatR** para separar lectura y escritura.
- **Builder Pattern** para generar datos de prueba en tests unitarios e integración.
- Eventos de dominio internos (`VehicleReservedEvent`) para desacoplar la lógica de reserva.

### Pruebas

- Se implementaron pruebas unitarias usando **xUnit** y **Moq**.
- Se implementaron pruebas de integración con **CustomWebApplicationFactory** para simular el servidor y la base de datos en memoria.
- Los **Builders** (`VehicleBuilder` y `ReservationCommandBuilder`) ayudan a crear datos consistentes para los tests.

### Persistencia

- Se usa **Entity Framework Core** como ORM.
- Repositorios separados para operaciones de lectura (`IVehicleRepository`) y escritura (`IReservationCommandRepository`).
- **Bases de datos compatibles**: **MySQL** y **MongoDB** (configurable según entorno).

### Endpoints

- `GET /api/vehicles/search` → Buscar vehículos disponibles.
---

## Cómo ejecutar el proyecto localmente

### Requisitos

- **.NET 8 SDK**
- IDE recomendado: **Visual Studio 2022** o **VS Code**
- **Base de datos MySQL y MongoDB** (para producción)
- Se puede usar **InMemory** para pruebas locales

 ### Diseño técnico
https://1drv.ms/w/c/ce2875b2f52c22d0/IQANWUYEagQrQrucBuiB0NH3Ad0Gwf5ZjA-P_1pgxTx5m5k?e=6DBMG2

### Pasos

1. Clonar el repositorio:

```bash
git clone https://github.com/marcas1216/VehicleFinder.git
cd VehicleFinder


