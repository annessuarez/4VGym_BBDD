using System.Text.Json.Serialization;

namespace _4VGymEF.Models
{
    public class Instructor
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("photo_url")] //para que ell json salga con el nombre correcto
        public string PhotoUrl { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        [JsonIgnore] //paraq no intente meter las actividades en el JSON del instructor
        public ICollection<Activity> Activities { get; set; } = new List<Activity>();

        //Importante añadir este método porq sino no funcionara en el controlador.
        public bool isEmpty()
        {
            return string.IsNullOrEmpty(Name) && string.IsNullOrEmpty(Email) && string.IsNullOrEmpty(PhotoUrl)
                && string.IsNullOrEmpty(Phone);
        }

    }
}
