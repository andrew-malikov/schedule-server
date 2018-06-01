using Newtonsoft.Json;

namespace ScheduleServer.Models {
    public class Group {
        public int Id { get; set; }
        [JsonIgnore]
        public string Code { get; set; }
        public string Name { get; set; }

        public Faculty Faculty { get; set; }
        public Course Course { get; set; }

        [JsonIgnore]
        public SerializedGroupSchedule Schedule { get; set; }
    }
}