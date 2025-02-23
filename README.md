# StoreSample
### Estructura y tecnologías utilizadas
El proyecto está desarrollado en **.NET Core** siguiendo una arquitectura por capas:

- **Aplicación**: Contiene la lógica de negocio y servicios.
- **Dominio**: Define las entidades y contratos.
- **Infraestructura**: Implementa el acceso a datos y dependencias externas.

Se utilizó **Entity Framework** para el mapeo de las entidades de base de datos, aplicando principios **SOLID** para mantener un código modular y escalable.

Por otro lado, el frontend fue desarrollado en **Angular**, utilizando **Angular Material** para la interfaz de usuario y brindando una experiencia moderna y responsive.

### Ejecución de la prueba

#### 1. Backend (.NET Core)
- Cambiar la cadena de conexion de la BD en "\StoreSample\StoreSample\appsettings.json"

- Restaurar paquetes con dotnet restore

- Compilar y ejecutar con dotnet run

#### 2. Frontend (Angular)
- Instalar dependencias con npm install

- Ejecutar el proyecto con ng serve

Este enfoque asegura una estructura limpia, fácil de mantener y escalable.
