using _4VGymEF.Models;
using _4VGymEF.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace _4VGymEF.Controllers
{
        [ApiController]
        [Route("activity-types")]
        public class ActivityTypesController : ControllerBase
        {
            private readonly ActivityTypeRepository _repository;
            public ActivityTypesController(ActivityTypeRepository repository)
            {
                _repository = repository;
            }

            [HttpGet]
            [ProducesResponseType(typeof(IEnumerable<ActivityType>), StatusCodes.Status200OK)]
            [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
            public ActionResult<IEnumerable<ActivityType>> FindActivityTypes()
            {
                try
                {
                    IEnumerable<ActivityType> activityTypes = _repository.GetAll();

                    if (activityTypes == null || activityTypes.Count() == 0)
                    {
                        return BadRequest(new ErrorResponse(ErrorCode.NoEncontrado, "No se han encontrado actividades"));
                    }

                    return Ok(activityTypes);
                }
                catch (Exception)
                {
                    return BadRequest(new ErrorResponse(ErrorCode.ErrorInternoEnServidor, "Any problem in the Server"));
                }
            }

            [HttpPost]
            [ProducesResponseType(typeof(ActivityType), StatusCodes.Status201Created)]
            [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
            public ActionResult<ActivityType> CreateActivityType([FromBody] ActivityType activityType)
            {
                try
                {
                    // 1. Validación básica de la entrada, objeto no nulo y obligatorio nombre de actividad
                    // Esto se llaman condiciones de barrera!!!
                    if (activityType == null)
                    {
                        return BadRequest(new ErrorResponse(ErrorCode.DatoInvalido, "El tipo de actividad no puede ser nulo"));
                    }
                    if (String.IsNullOrEmpty(activityType.Name))
                    {
                        return BadRequest(new ErrorResponse(ErrorCode.ErrorValidacion, "El nombre de la actividad es oblligatorio"));
                    }

                    // No puede haber tipos de actividad con el mismo nombre
                    if (_repository.GetByName(activityType.Name) != null)
                    {
                        return BadRequest(new ErrorResponse(ErrorCode.NombreDuplicado, "El nombre del tipo de actividad es único, ya hay un tipo de actividad con ese nombre"));
                    }


                    // 2. Insertar/Guardar el objeto usando el repositorio
                    ActivityType createdActivityType = _repository.Add(activityType);

                    // 3. Retornar respuesta 201 Created con la ubicación o el objeto creado
                    return StatusCode(StatusCodes.Status201Created, createdActivityType);
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                                      new ErrorResponse(ErrorCode.ErrorInternoEnServidor, "An internal server error occurred"));

                }
            }

        }
}
