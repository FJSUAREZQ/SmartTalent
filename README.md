# Proyecto Clean Architecture - .NET Core 8

Una aplicación desarrollada con .NET Core 8 siguiendo los principios de Clean Architecture, SOLID y mejores prácticas de desarrollo.

## Arquitectura

Este proyecto implementa **Clean Architecture** con la siguiente estructura de capas:

```
📦 CleanArchitecture
 ┣ 📂 Application          # Casos de uso y lógica de aplicación
 ┃ ┣ 📂 Interfaces         # Contratos de la capa de aplicación
 ┃ ┣ 📂 Products           # Casos de uso relacionados con productos
 ┃ ┗ 📂 DTOs              # Objetos de transferencia de datos
 ┣ 📂 Domain              # Entidades y reglas de negocio
 ┃ ┣ 📂 Entities          # Entidades del dominio
 ┃ ┗ 📂 Interfaces        # Contratos del dominio
 ┣ 📂 Infrastructure      # Acceso a datos y servicios externos
 ┃ ┣ 📂 DataContext       # Contexto de Entity Framework
 ┃ ┗ 📂 Repositories      # Implementación de repositorios
 ┗ 📂 WebAPI             # Capa de presentación - API REST
   ┣ 📂 Controllers       # Controladores de la API
   ┗ 📂 Properties        # Configuración de la aplicación
```

## Tecnologías Utilizadas

- **.NET Core 8** - Framework principal
- **Entity Framework Core** - ORM para acceso a datos
- **Clean Architecture** - Patrón arquitectónico
- **SOLID Principles** - Principios de diseño de software
- **Repository Pattern** - Patrón para acceso a datos
- **Dependency Injection** - Inyección de dependencias nativa
- **RESTful API** - Servicios web REST

## Prerrequisitos

Antes de ejecutar el proyecto, asegúrate de tener instalado:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/sql-server) o [SQL Server LocalDB](https://docs.microsoft.com/sql/database-engine/configure-windows/sql-server-express-localdb)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) o [Visual Studio Code](https://code.visualstudio.com/)
- [Git](https://git-scm.com/)

### Verificar la instalación de .NET 8

```bash
dotnet --version
```

Deberías ver una versión 8.x.x

## Instalación y Configuración

### 1. Clonar el repositorio

```bash
git clone <url-del-repositorio>
cd CleanArchitecture
```

### 2. Restaurar dependencias

```bash
dotnet restore
```

### 3. Configurar la base de datos
#### SQL Server

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=CleanArchitectureDB;Integrated Security=true;TrustServerCertificate=true;"
  }
}
```

### 4. Crear la base de datos y tabla
#### Ejecutar script SQL manualmente

Si Ejecuta este script en tu base de datos:

```sql
CREATE TABLE Products (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255) NULL,
    Price DECIMAL(10,2) NOT NULL CHECK (Price > 0),
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    ModifiedAt DATETIME NULL
);
```

### 5. Ejecutar la aplicación

```bash
# Desde la carpeta raíz del proyecto
cd WebAPI
dotnet run
```

La aplicación estará disponible en:
- **HTTPS**: https://localhost:5203
- **HTTP**: http://localhost:5203


## Ejecución en Desarrollo

### Usando .NET CLI

```bash
# Ejecutar en modo desarrollo
dotnet run --project WebAPI
```

### Usando Visual Studio

1. Abrir la solución en Visual Studio 2022
2. Establecer `WebAPI` como proyecto de inicio
3. Presionar `F5` para ejecutar con debugging o `Ctrl+F5` sin debugging

## Estructura del Proyecto

### Domain Layer (Dominio)
- **Entities**: Entidades del dominio con lógica de negocio
- **Interfaces**: Contratos que definen repositorios y servicios del dominio

### Application Layer (Aplicación)
- **Use Cases**: Casos de uso específicos (CreateProduct, GetProduct, etc.)
- **DTOs**: Objetos de transferencia de datos
- **Interfaces**: Contratos de servicios de aplicación

### Infrastructure Layer (Infraestructura)
- **DataContext**: Configuración de Entity Framework
- **Repositories**: Implementación concreta de los repositorios
- **External Services**: Integración con servicios externos

### WebAPI Layer (Presentación)
- **Controllers**: Controladores REST API
- **Middleware**: Middleware personalizado
- **Configuration**: Configuración de la aplicación

## Modelo de Datos

### Entidad Product

La aplicación maneja productos con la siguiente estructura:

| Campo | Tipo | Descripción | Restricciones |
|-------|------|-------------|---------------|
| `Id` | INT | Identificador único | Primary Key, Identity(1,1) |
| `Name` | NVARCHAR(100) | Nombre del producto | NOT NULL, máximo 100 caracteres |
| `Description` | NVARCHAR(255) | Descripción del producto | Opcional, máximo 255 caracteres |
| `Price` | DECIMAL(10,2) | Precio del producto | NOT NULL, debe ser mayor a 0 |
| `CreatedAt` | DATETIME | Fecha de creación | NOT NULL, valor por defecto GETDATE() |
| `ModifiedAt` | DATETIME | Fecha de última modificación | Opcional, se actualiza automáticamente |

## Casos de Uso Disponibles

### Ejemplo de JSON para Product

```json
{
  "id": 1,
  "name": "Smartphone Samsung Galaxy S24",
  "description": "Smartphone con pantalla de 6.1 pulgadas, cámara de 108MP y 256GB de almacenamiento",
  "price": 799.99,
  "createdAt": "2024-12-15T10:30:00",
  "modifiedAt": "2024-12-15T14:45:00"
}
```

### Productos
- **CreateProductUseCase**: Crear un nuevo producto
- **GetAllProductsUseCase**: Obtener todos los productos
- **GetProductByIdUseCase**: Obtener producto por ID
- **GetProductPagedUseCase**: Obtener productos paginados
- **UpdateProductUseCase**: Actualizar un producto
- **DeleteProductUseCase**: Eliminar un producto

## Endpoints de la API

### Productos

```
GET    /api/product/GetById/{id}         # Obtener producto por ID
GET    /api/product/GetAllPag?pageNumber=2&pageSize=3        # Obtener productos paginados
POST   /api/product/Create              # Crear nuevo producto
PUT    /api/product/Update         # Actualizar producto
DELETE /api/product/Delete/{id}         # Eliminar producto
```

### Ejemplo de uso con curl

```bash
# Obtener todos los productos con paginación
curl -X GET "https://localhost:5203/api/product/GetAllPag?pageNumber=1&pageSize=3" -H "accept: application/json"
```

## Validaciones de Negocio

El modelo de productos incluye las siguientes validaciones:

### Validaciones de Base de Datos
- **Name**: Campo requerido, máximo 100 caracteres
- **Price**: Debe ser mayor a 0 (CHECK constraint)
- **CreatedAt**: Se asigna automáticamente al crear el registro
- **ModifiedAt**: Se actualiza automáticamente en cada modificación

### Validaciones de Aplicación
- **Name**: No puede estar vacío o contener solo espacios
- **Price**: Debe ser un número decimal positivo
- **Description**: Opcional, pero si se proporciona, máximo 255 caracteres


## Configuración Adicional

### Variables de Entorno

Puedes configurar las siguientes variables de entorno:

```bash
export ConnectionStrings__DefaultConnection="tu-cadena-de-conexion"
export ASPNETCORE_ENVIRONMENT="Development"
```

## Principios SOLID Aplicados

- **S**: Single Responsibility - Cada clase tiene una única responsabilidad
- **O**: Open/Closed - Abierto para extensión, cerrado para modificación
- **L**: Liskov Substitution - Las clases derivadas deben ser sustituibles por sus clases base
- **I**: Interface Segregation - Interfaces específicas mejor que una interfaz general
- **D**: Dependency Inversion - Depender de abstracciones, no de implementaciones concretas


## Características

- ✅ Clean Architecture
- ✅ SOLID Principles
- ✅ Entity Framework Core
- ✅ Repository Pattern
- ✅ Dependency Injection
- ✅ RESTful API
- ✅ Error Handling
- ✅ Data Transfer Objects (DTOs)
- ✅ Paginación
- ✅ Validación de modelos

## Solución de Problemas

### Error de conexión a la base de datos

1. Verifica que SQL Server esté ejecutándose
2. Comprueba la cadena de conexión en `appsettings.json`
3. Ejecuta `dotnet ef database update`

### Error al restaurar paquetes

```bash
# Limpiar y restaurar
dotnet clean
dotnet restore --force
```

------------------------------------------------------------------------------------
------------------------------------------------------------------------------------

# Product CRUD - Angular 20 Standalone

Aplicación web para gestión de productos desarrollada con Angular 20 Standalone Components, Bootstrap, Signals y Observables.

## Tecnologías

- **Angular 20** - Standalone Components
- **Bootstrap 5** - Framework CSS
- **RxJS** - Observables para programación reactiva
- **Signals** - Nuevo sistema de reactividad de Angular
- **TypeScript** - Lenguaje principal

## Prerrequisitos

- [Node.js](https://nodejs.org/) (v18 o superior)
- [Angular CLI](https://angular.io/cli) 20+

```bash
# Verificar versiones
node --version
npm --version
ng version
```

## Instalación

### 1. Instalar dependencias

```bash
npm install
```

### 2. Configurar API URL

Edita el archivo `src/environments/environment.ts`:

```typescript
export const environment = {
  apiProductsBaseUrl: 'http://localhost:5203/api',
  defaultPageSize: 5 // Default page size for pagination
};
```

## Ejecución

### Modo desarrollo

```bash
ng serve
```

La aplicación estará disponible en: `http://localhost:4200`

### Modo producción

```bash
ng build --prod
ng serve --prod
```

## Arquitectura

```
📦 src/app
 ┣ 📂 core
 ┃ ┣ 📂 interceptors    # HTTP interceptors
 ┃ ┗ 📂 tokens          # Injection tokens
 ┣ 📂 features
 ┃ ┗ 📂 product         # Funcionalidad de productos
 ┃   ┣ 📂 components    # Componentes standalone
 ┃   ┣ 📂 pages         # Páginas (create, getAllPaged, update)
 ┃   ┣ 📂 services      # Servicios
 ┃	 ┃  ┗ 📂 api       # Servicios Http
 ┃	 ┃  ┗ 📂 repositories       # Repositorio interface y su implement.  
 ┃   ┣ 📂 models        # Modelos de interfaces
 ┃   ┣ 📂 usesCases     # Casos de uso
```

## Funcionalidades

- ✅ **Listar productos** - Con paginación
- ✅ **Crear producto** - Formulario reactivo
- ✅ **Actualizar producto** - Edición inline
- ✅ **Eliminar producto** - Confirmación modal
- ✅ **Paginación** - Bootstrap pagination

## Características Técnicas

### Signals
```typescript
// Estado reactivo con signals
readonly products = signal<Product[]>([]);
readonly loading = signal<boolean>(false);
```

### Observables
```typescript
// HTTP con observables
getProducts(): Observable<Product[]> {
  return this.http.get<Product[]>(`${this.apiUrl}/products`);
}
```

### Standalone Components
```typescript
@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule],
  templateUrl: './product-list.component.html'
})
export class ProductListComponent { }
```

## UI/UX

- **Bootstrap 5** para estilos
- **Componentes responsivos**
- **Loading states**

## Variables de Entorno

### Development (`environment.ts`)
```typescript
export const environment = {
  production: false,
  apiProductsBaseUrl: 'http://localhost:5203/api',
  defaultPageSize: 5 // Default page size for pagination
};
```

### Production (`environment.prod.ts`)
```typescript
export const environment = {
  production: true,
  apiProductsBaseUrl: 'https://your-api-domain.com/api'
  defaultPageSize: 5 // Default page size for pagination
};
```

## 📦 Dependencias Principales

```json
{
  "@angular/core": "^20.0.0",
  "@angular/common": "^20.0.0",
  "@angular/router": "^20.0.0",
  "@angular/forms": "^20.0.0",
  "bootstrap": "^5.3.0",
  "rxjs": "^7.8.0"
}
```


## Solución de Problemas

### Error de CORS
Configurar en tu API .NET:
```csharp
builder.Services.AddCors(options => {
    options.AddPolicy("AllowAngular", builder => {
        builder.WithOrigins("http://localhost:4200")
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
```

### Clear cache
```bash
npm cache clean --force
rm -rf node_modules package-lock.json
npm install
```

---
