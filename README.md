# 4VGym con BBDD Relacional (Entity Framework Core)
API REST para la gestión del gimnasio **4VGym**, unificando la API original con la persistencia en base de datos SQL Server mediante Entity Framework Core.

---

## Ramas del proyecto
* **`main`**: Rama principal con el proyecto final unificado y limpio (`4VGym_BBDD`).
* **`ProyectoCompleto`**: Rama secundaria con los proyectos base originales (`4VGymAPI` y `4VGymEF`) como copia de respaldo.
> [!NOTE]  
> Para la corrección, tomar como referencia únicamente la rama **`main`**.

---

## Decisiones de Diseño
* **Modelos vs DTOs:**  
  Se han separado los modelos de base de datos (`Models`) de los objetos de transferencia (`Dtos`) para mantener el Swagger idéntico al original y evitar bucles infinitos de serialización JSON en las relaciones de Entity Framework.
* **Validaciones:**  
  * En **DTOs**: validaciones de formato mediante Data Annotations (`[Required]`, `[Range]`, fechas).
  * En **Controladores**: condiciones de barrera y formato (Regex de email y teléfono).
  * En **Repositorios**: validación de negocio contra la base de datos (evitar duplicados y comprobar existencia de claves foráneas con `.Any()`).
* **Manejo de Errores:**  
  Se utiliza un enumerado `ErrorCode` para evitar *magic numbers*.
  
---

## Pruebas y Swagger
Puedes ver el diagrama donde se muestran las relaciones de las tablas, es el archivo `diagrama_4Gym_BBDD.drawio`.

Al arrancar el proyecto desde Visual Studio, la documentación interactiva y pruebas de endpoints se abrirán automáticamente en: `/swagger`
