using _4VGymEF.Data;
using _4VGymEF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace _4VGymEF.Repositories
{
    public class InstructorRepository
    {
        // En API sería: private Dictionary<long, Instructor> _instructors = new(); + constructor, ahora en 
        // EF hay que crear un objeto de tipo GymDbContext
        // Ahora tampoco necesitamos el SeedData() porq para eso esta el fichero .sql de antes.

        private readonly GymDbContext _context;
        public InstructorRepository(GymDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Instructor> GetAll()
        {
            //Antes era _instructors.Values.ToList(); ahora hay que usar el _context
            return _context.Instructors.ToList();
        }

        public Instructor? GetById(int id)
        {
            return _context.Instructors.Find(id);
        }

        public IEnumerable<Instructor> GetByFirstInitial(string? firstInitial = null)
        {
            //por si firstInitial es nulo o esta vacio, si es TRUE devulve todos, si es FALSE devuelve la lista
            if (String.IsNullOrEmpty(firstInitial))
            {
                return GetAll();
            }
            return _context.Instructors.Where(i => i.Name.StartsWith(firstInitial)).ToList();
        }

        public Instructor? Add(Instructor instructorNew, out ErrorResponse? errorResponse)
        {
            //en este solo hay que comprobar, NO AÑADIR
            errorResponse = null;
            // Comprobación de duplicados (Email o Nombre existente, ignorando mayúsculas/minúsculas)
            bool exists = _context.Instructors.Any(instructor =>
                instructor.Email.Equals(instructorNew.Email, StringComparison.CurrentCultureIgnoreCase) ||
                instructor.Name.Equals(instructorNew.Name, StringComparison.CurrentCultureIgnoreCase));

            if (exists)
            {
                errorResponse = new ErrorResponse(34, "An instructor with the same email or name already exists");
                return null;
            }

            return instructorNew;

            //NO ES NECESARIO SOLO SON APUNTES:
            /* Añade el objeto a la tabla
                _context.Instructors.Add(instructorNew);

               Guarda los cambios en SQL Server (hace el INSERT y le da el ID automático)
                _context.SaveChanges();
                return instructorNew;
             */
        }


    }
}
