# MindFactory.News - Solicitud del Proyecto

Se desarrolló una aplicación de noticias con arquitectura **cliente-servidor**, cumpliendo con los siguientes requerimientos funcionales:

1. Mostrar un **listado de noticias**
2. Visualizar el **detalle de una noticia**
3. Realizar **operaciones CRUD** sobre las noticias

## 🔧 Backend

* Se implementó una **API RESTful** para cubrir los casos de uso mencionados.
* El modelo de datos incluye la entidad `Noticia`, con los siguientes campos:

  * `title`: Título de la noticia
  * `body`: Cuerpo del contenido
  * `image_url`: URL de la imagen (solo se almacena la URL)
  * `author`: Autor de la noticia
  * `date`: Fecha de publicación
* Se utilizó **PostgreSQL** como motor de base de datos relacional.

### Endpoints Implementados

* Listado de noticias
* Detalle de una noticia
* Creación de una noticia
* Edición de una noticia
* Eliminación de una noticia
* Búsqueda de noticias por título y/o autor

### 📌 Criterios de Evaluación

* Arquitectura de la solución
* Diseño de API según estándares REST
* Buenas prácticas de desarrollo (Clean Code, SOLID, DRY)
* Uso de linters y herramientas de análisis estático
* Dockerización del entorno
* Testing automatizado
* Código preparado para producción
* Escalabilidad y mantenibilidad

---

# MindFactory.News - Mejoras Implementadas

Además de los requerimientos solicitados, se aplicaron las siguientes mejoras:

* ✔️ Se creó la entidad **Author** para la gestión de autores de noticias
* ✔️ Se implementó el **CRUD completo de autores**
* ✔️ Se desarrollaron **entidades base** para permitir la futura implementación de autenticación con distintos niveles de permisos y **auditoría de operaciones**
* ✔️ Se agregó la entidad **Editorial** (aún no implementada completamente), con el objetivo de permitir la **agrupación de noticias por editorial** en futuras versiones

---

# MindFactory.News - Arquitectura del Proyecto

Este proyecto sigue una arquitectura limpia por capas, basada en principios de separación de responsabilidades y escalabilidad. A continuación se describe la estructura lógica y física de la aplicación.

---

## ✨ Arquitectura General

```text
+----------------------------------------------------+
|                    CLIENTE (React, etc.)           |
|   - Se comunica con la API REST vía HTTP/JSON      |
+--------------------------▲-------------------------+
                           |
                           v
+----------------------------------------------------+
|               MindFactory.News.Api                 |
|   - Controladores (Controllers)                    |
|   - Middlewares                                    |
|   - Configuración general (Swagger, Serilog, etc.) |
+--------------------------▲-------------------------+
                           |
                           v
+----------------------------------------------------+
|          MindFactory.News.Application              |
|   - Casos de uso (UseCases)                        |
|   - Servicios de aplicación                        |
|   - Reglas de negocio específicas                  |
+--------------------------▲-------------------------+
                           |
                           v
+----------------------------------------------------+
|            MindFactory.News.Domain                 |
|   - Entidades (Entities)                           |
|   - Interfaces (Repositories, Services)            |
|   - Value Objects / Enums                          |
+--------------------------▲-------------------------+
                           |
                           v
+----------------------------------------------------+
|         MindFactory.News.Infrastructure            |
|   - EF Core DbContext                              |
|   - Repositorios / implementación de interfaces    |
|   - Conexión a PostgreSQL                          |
|   - Integraciones externas                         |
+--------------------------▲-------------------------+
                           |
                           v
+----------------------------------------------------+
|                     PostgreSQL                     |
|    - Base de datos relacional                      |
|    - Tablas, relaciones, tsvector (full-text)      |
+----------------------------------------------------+
```

---

## 🐳 Dockerización del Proyecto

```text
+-----------------------------------------------------+
|                    Docker Host                      |
|  +----------------------+   +--------------------+  |
|  |   Container: API     |   | Container: Postgres|  |
|  |  ASP.NET Core API    |<->|  PostgreSQL 15     |  |
|  +----------------------+   +--------------------+  |
+-----------------------------------------------------+
```

* La API se ejecuta en un contenedor de ASP.NET Core.
* PostgreSQL corre en un contenedor independiente.
* La comunicación entre servicios se realiza a través de una red interna Docker definida en `docker-compose.yml`.

---

## 🎓 Buenas Prácticas

* Uso de **EF Core** con `DbContext` por proyecto.
* Repositorios desacoplados por interfaces en la capa `Domain`.
* Inyección de dependencias vía constructor.
* Linter y analizadores Roslyn para chequeo automático.
* Migraciones automatizadas desde `Infrastructure`.
* Logging centralizado con **Serilog**.
* Búsqueda con **tsvector** usando `to_tsvector()` y `plainto_tsquery()` para full-text search.

