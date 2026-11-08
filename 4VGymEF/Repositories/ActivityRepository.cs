using _4VGymEF.Data;
using _4VGymEF.Models;

namespace _4VGymEF.Repositories
{
    public class ActivityRepository
    {
        //Solo es necesario declarar el context porq ya tiene todas las tablas,
        //no es como en la API que habia que poner el repoActivityType y repoInstructors
        private readonly GymDbContext _context;
        public ActivityRepository(GymDbContext context)
        {
            _context = context;
        }

        //Hay que poner ActivityCreateDto porq es el que tiene ActivityTypeId e InstructorsId 
        public ActivityCreateDto? Add(ActivityCreateDto activity, out List<ErrorResponse> errorResponse)
        {
            errorResponse = new List<ErrorResponse>();

            if (!_context.ActivityTypes.Any(at => at.Id == activity.ActivityTypeId))
            {
                errorResponse.Add(new ErrorResponse(ErrorCode.ActivityTypeNoEncontrado, "El tipo de actividad debe existir"));
            }

            foreach (var idInstructor in activity.InstructorsId)
            {
                if (!_context.Instructors.Any(i => i.Id == idInstructor))
                {
                    errorResponse.Add(new ErrorResponse(ErrorCode.InstructorNoEncontrado, "No se ha encontrado instructor con ID " + idInstructor));
                }
            }

            if (errorResponse.Count > 0)
            {
                return null;
            }

            return activity;

        }
    }
}
