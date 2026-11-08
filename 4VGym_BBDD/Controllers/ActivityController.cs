using _4VGymEF.Models;
using _4VGymEF.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace _4VGymEF.Controllers
{
    [ApiController]
    [Route("activities")]
    public class ActivityController : ControllerBase
    {
        private readonly ActivityRepository _repository;
        public ActivityController(ActivityRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ActivityCreateDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<ActivityCreateDto> CreateActivity([FromBody] ActivityCreateDto activity)
        {
            try
            {
                List<ErrorResponse> errors = new List<ErrorResponse>();
                var activityCreated = _repository.Add(activity, out errors);
                if (activityCreated == null)
                {
                    return BadRequest(errors);
                }

                return StatusCode(StatusCodes.Status201Created, activity);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new ErrorResponse(ErrorCode.ErrorInternoEnServidor, "An internal server error occurred"));
            }
        }
    }
}
