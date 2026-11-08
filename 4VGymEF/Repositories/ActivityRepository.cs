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

        public Activity? Add(Activity activity, out List<ErrorResponse> errorResponse)
        {
            errorResponse = null;

            if (_context.ActivityTypes.GetById(activity.ActivityTypeId) == null)
            {
                errorResponse.Add(new ErrorResponse(34, "EL tipo de actividad debe de funcionar"));
            }


        }
    }
