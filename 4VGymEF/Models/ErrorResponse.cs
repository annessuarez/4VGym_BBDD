using System.Text.Json.Serialization;

namespace _4VGymEF.Models
{
    public enum ErrorCode
    {
        ErrorInternoEnServidor = 1,
        NoEncontrado = 2,
        DatoInvalido = 3,
        ErrorValidacion = 4,
        NombreDuplicado = 5,

        InstructorDuplicado = 34,
        InstructorNoEncontrado = 35,
        ActivityTypeNoEncontrado = 44,
        ErrorDesconocido = 9999
    }

    public class ErrorResponse
    {
        [JsonPropertyName("code")]
        public long Code { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;
        public ErrorResponse(int code, string description)
        {
            Code = code;
            Description = description;
        }
        public ErrorResponse(ErrorCode code, string description)
        {
            Code = (long)code;
            Description = description;
        }
    }
}
