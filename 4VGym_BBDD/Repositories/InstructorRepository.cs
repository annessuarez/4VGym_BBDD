using _4VGymEF.Data;
using _4VGymEF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace _4VGymEF.Repositories
{
    public class InstructorRepository
    {
        private readonly GymDbContext _context;
        public InstructorRepository(GymDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Instructor> GetAll()
        {
            return _context.Instructors.ToList();
        }

        public Instructor? GetById(int id)
        {
            return _context.Instructors.Find(id);
        }

        public IEnumerable<Instructor> GetByFirstInitial(string? firstInitial = null)
        {
            if (String.IsNullOrEmpty(firstInitial))
            {
                return GetAll();
            }
            return _context.Instructors.Where(i => i.Name.StartsWith(firstInitial)).ToList();
        }

        public Instructor? Add(Instructor instructorNew, out ErrorResponse? errorResponse)
        {
            errorResponse = null;

            bool exists = _context.Instructors.Any(instructor =>
                instructor.Email == instructorNew.Email || instructor.Name == instructorNew.Name);

            if (exists)
            {
                errorResponse = new ErrorResponse(ErrorCode.InstructorDuplicado, "Ya existe un instructor con ese nombre o email.");
                return null;
            }

            return instructorNew;
        }
    }
}
