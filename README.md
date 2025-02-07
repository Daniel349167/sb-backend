# SB-Backend

## Requerimientos Previos

- .NET 8 SDK instalado en tu máquina.
- Un IDE compatible (Visual Studio, Visual Studio Code, JetBrains Rider) o la CLI de .NET.
- Acceso a Git para clonar o descargar el repositorio.

## Estructura del Proyecto

El repositorio sigue el patrón de arquitectura Onion y está organizado de la siguiente manera:

- **SB.Prueba.API**  
  Contiene los controladores y la configuración inicial de la API (Startup, Program, etc.).

- **SB.Prueba.Application**  
  Capa de aplicación, donde se orquestan los casos de uso, servicios y reglas de negocio más cercanas al dominio.

- **SB.Prueba.Domain**  
  Entidades y objetos de dominio, además de las interfaces y contratos principales.

- **SB.Prueba.Infrastructure**  
  Lógica de acceso a datos y cualquier otro recurso externo (loggers, repositorios, etc.).

- **SB.Prueba.sln**  
  Archivo de solución principal para abrir en Visual Studio o similar.

- **dotnet**  
  Archivo o carpeta auxiliar que indica la compatibilidad con .NET.

## Descarga y Ejecución

Clonar o descargar este repositorio:

```bash
git clone https://github.com/Daniel349167/sb-frontend.git
```

Restaurar dependencias y compilar (puedes hacerlo desde la raíz o desde la carpeta `SB.Prueba.API`):

```bash
cd SB.Prueba.API
dotnet restore
dotnet build
```

Ejecutar la aplicación:

```bash
dotnet run
```

De manera predeterminada, la API se habilitará en la URL `https://localhost:5088` o `http://localhost:5088` (dependiendo de la configuración).

## Pruebas y Documentación

Se ha configurado Swagger para la documentación interactiva de la API. Una vez iniciada la aplicación, dirígete a:

```bash
https://localhost:5088/swagger
```

para ver la documentación y realizar pruebas de los endpoints.

## Base de Datos (Archivo de Texto Plano)

La aplicación usa un archivo de texto para la persistencia de datos, ubicado en:

```bash
SB.Prueba.API/Data/governmentEntities.txt
```

En este archivo se almacenan las entidades gubernamentales de la República Dominicana. Se accede a él desde la capa de infraestructura o desde la API siguiendo la arquitectura Onion.

## Autenticación (JWT)

La API implementa JSON Web Tokens (JWT) para proteger los endpoints.

Al iniciar sesión o consumir el endpoint de autenticación (por ejemplo, `/api/auth/login`), recibirás un token JWT.

## Logs

El proyecto está configurado para manejar logs a través de **Serilog**. En el archivo `appsettings.json` (dentro de `SB.Prueba.API`), encontrarás la configuración para la gestión de logs y la creación de archivos de bitácora.

## AppSettings y Cadena de Conexión

Las cadenas de conexión y demás configuraciones importantes se encuentran en el archivo:

```bash
SB.Prueba.API/appsettings.json
```

## Arquitectura Onion

La solución utiliza el estilo de arquitectura Onion:

- **Domain**: Modela las entidades y contratos principales.
- **Application**: Implementa los casos de uso y reglas de negocio principales.
- **Infrastructure**: Brinda las implementaciones de acceso a datos, logging y demás servicios externos.
- **API**: Exposición pública (controladores, mapeo de rutas, inyección de dependencias, etc.).

Esta separación ayuda a mantener una arquitectura limpia y escalable.
