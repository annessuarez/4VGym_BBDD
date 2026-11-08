using _4VGymEF.Data;
using _4VGymEF.Models;

namespace _4VGymEF.Repositories
{
    public class ActivityRepository
    {
        private readonly GymDbContext _context;
        public ActivityRepository(GymDbContext context)
        {
            _context = context;
        }

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
