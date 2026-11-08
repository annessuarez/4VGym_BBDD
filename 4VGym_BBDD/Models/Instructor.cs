using System.Text.Json.Serialization;

namespace _4VGymEF.Models
{
    public class Instructor
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("photo_url")]
        public string PhotoUrl { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        [JsonIgnore]
        public ICollection<Activity> Activities { get; set; } = new List<Activity>();

        public bool isEmpty()
        {
            return string.IsNullOrEmpty(Name) && string.IsNullOrEmpty(Email) && string.IsNullOrEmpty(PhotoUrl)
                && string.IsNullOrEmpty(Phone);
        }
    }
}
