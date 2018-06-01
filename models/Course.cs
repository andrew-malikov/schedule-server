using System.Collections.Generic;
using Newtonsoft.Json;

namespace ScheduleServer.Models {
    public class Course {
        public int Id { get; set; }
        [JsonIgnore]
        public string Code { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }

        [JsonIgnore]
        public List<Group> Groups { get; set; }
    }
}