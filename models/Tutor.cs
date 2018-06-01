using Newtonsoft.Json;

namespace ScheduleServer.Models {
    public class Tutor {
        public int Id { get; set; }
        [JsonIgnore]
        public string Code { get; set; }
        public string LongName { get; set; }
        public string ShortName { get; set; }

        public Department Department { get; set; }

        [JsonIgnore]
        public SerializedTutorSchedule Schedule { get; set; }
    }
}