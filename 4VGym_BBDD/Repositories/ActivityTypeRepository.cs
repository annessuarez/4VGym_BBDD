using _4VGymEF.Data;
using _4VGymEF.Models;

namespace _4VGymEF.Repositories
{
    public class ActivityTypeRepository
    {
        private readonly GymDbContext _context;
        public ActivityTypeRepository(GymDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ActivityType> GetAll()
        {
            return _context.ActivityTypes;
        }

        public ActivityType? GetByName(string name)
        {
            return _context.ActivityTypes.SingleOrDefault(at => at.Name.ToLower() == name.ToLower());
        }

        public ActivityType? GetById(int? id)
        {
            if (id == null)
                return null;

            return _context.ActivityTypes.SingleOrDefault(at => at.Id == id);
        }

        public ActivityType Add(ActivityType activityType)
        {
            int maxID = 0;
            //busca en la bbdd si hay algun AT y si hay busca el id más alto
            if (_context.ActivityTypes.Any())
            {
                maxID = _context.ActivityTypes.Max(at => at.Id);
            }

            activityType.Id = maxID + 1;

            return activityType;

        }

    }
}
