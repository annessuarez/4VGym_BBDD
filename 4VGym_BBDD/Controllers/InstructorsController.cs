using _4VGymEF.Data;
using _4VGymEF.Models;
using _4VGymEF.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace _4VGymEF.Controllers
{
    [ApiController]
    [Route("instructors")]
    public class InstructorsController : ControllerBase
    {
        private readonly InstructorRepository _repository;
        public InstructorsController(InstructorRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Instructor>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<Instructor>> GetInstructors([FromQuery] int? id, [FromQuery(Name = "first_initial")] string? firstInitial)
        {
            try
            {
                if (id.HasValue)
                {
                    Instructor? instructor = _repository.GetById(id.Value);
                    if (instructor == null)
                    {
                        return NotFound(new ErrorResponse(ErrorCode.NoEncontrado, $"No existe el instructor con ID {id.Value}"));
                    }
                    return Ok(new[] { instructor });
                }

                if (!string.IsNullOrWhiteSpace(firstInitial))
                {
                    IEnumerable<Instructor> instructorsByInitial = _repository.GetByFirstInitial(firstInitial);
                    return Ok(instructorsByInitial);
                }

                IEnumerable<Instructor> allInstructors = _repository.GetAll();
                return Ok(allInstructors);
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ErrorResponse(ErrorCode.ErrorInternoEnServidor, $"Error interno: {ex.Message}")
                );
            }
        }

        private static readonly Regex SoloNumerosRegex = new Regex(@"^[0-9]+$", RegexOptions.Compiled);
        private static readonly Regex EmailRegex = new Regex(@"^[^@\s]+@gmail.com", RegexOptions.Compiled);

        [HttpPost]
        [ProducesResponseType(typeof(Instructor), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public ActionResult<Instructor> CreateInstructor([FromBody] Instructor instructorNew)
        {
            try
            {
                if (instructorNew.isEmpty())
                {
                    return BadRequest(new ErrorResponse(ErrorCode.DatoInvalido, "El instructor no puede estar vacío"));
                }

                if (string.IsNullOrWhiteSpace(instructorNew.Name))
                {
                    return BadRequest(new ErrorResponse(ErrorCode.ErrorValidacion, "El nombre del instructor es obligatorio"));
                }

                if (string.IsNullOrWhiteSpace(instructorNew.Phone) || !SoloNumerosRegex.IsMatch(instructorNew.Phone))
                {
                    return BadRequest(new ErrorResponse(ErrorCode.ErrorValidacion, "El número de telefono es obligatorio y debe contener solo números."));
                }

                if (string.IsNullOrWhiteSpace(instructorNew.Email) || !EmailRegex.IsMatch(instructorNew.Email))
                {
                    return BadRequest(new ErrorResponse(ErrorCode.ErrorValidacion, "El correo electrónico es obligatorio y debe tener un formato correcto."));
                }

                if (string.IsNullOrWhiteSpace(instructorNew.PhotoUrl) ||
                    !Uri.TryCreate(instructorNew.PhotoUrl, UriKind.Absolute, out Uri? uriResult) ||
                    (uriResult.Scheme != Uri.UriSchemeHttp && uriResult.Scheme != Uri.UriSchemeHttps))
                {
                    return BadRequest(new ErrorResponse(ErrorCode.ErrorValidacion, "La foto no es válida. Tiene que empezar con http:// o https://"));
                }

                Instructor? instructor = _repository.Add(instructorNew, out ErrorResponse? errorResponse);
                if (errorResponse != null)
                {
                    return BadRequest(errorResponse);
                }

                return StatusCode(StatusCodes.Status201Created, instructor);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                    new ErrorResponse(ErrorCode.ErrorInternoEnServidor, "An internal server error occurred"));
            }
        }
    }
}
