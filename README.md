# Blog Application

## Descripción

Esta es una aplicación web para la gestión de blogs, donde los usuarios pueden crear, editar, eliminar y visualizar entradas. La aplicación está desarrollada utilizando Angular para el frontend y .NET 8 para el backend.

## Tecnologías Utilizadas

- **Frontend**: Angular, Bootstrap
- **Backend**: .NET 8, ASP.NET Core
- **Base de Datos**: SQL Server (LocalDB)
- **Pruebas Unitarias**: xUnit.

## Instalación
Pasos para clonar el proyecto e instalar dependencias.

1. Clona el repositorio:
   git clone https://github.com/usuario/proyecto.git
   ```

2. **Backend** (.NET):
   - Navega al directorio del proyecto backend:
     cd backend
   - Restaura paquetes NuGet y construye el proyecto:
     dotnet restore
     dotnet build
   - Configura la cadena de conexión a tu base de datos SQL Server en `appsettings.json`.

3. **Frontend** (Angular):
   - Navega al directorio del frontend:
     cd frontend
   - Instala las dependencias de Angular:
     npm install

4. **Base de datos**:
   - Ejecuta las migraciones de la base de datos:
     dotnet ef database update

### Clonación del Repositorio
git clone https://github.com/tuusuario/blog-app.git

## Ejecución
Instrucciones para ejecutar el proyecto.

1. **Backend**:
   - Ejecuta el servidor .NET:
     dotnet run

2. **Frontend**:
   - Inicia el servidor Angular:
     ng serve

La aplicación estará disponible en `http://localhost:4200/` y el backend en `https://localhost:7291/`.

