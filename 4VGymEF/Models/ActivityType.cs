using System.Text.Json.Serialization;

namespace _4VGymEF.Models
{
    public class ActivityType
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("number-monitors")]
        public int NumberInstructors { get; set; } = 1;

        [JsonIgnore]
        public ICollection<Activity> Activities { get; set; } = new List<Activity>();

    }

}
