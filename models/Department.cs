using System.Collections.Generic;
using Newtonsoft.Json;

namespace ScheduleServer.Models {
    public class Department {
        public int Id { get; set; }
        [JsonIgnore]
        public string Code { get; set; }
        public string LongName { get; set; }
        public string ShortName { get; set; }

        public Faculty Faculty { get; set; }

        [JsonIgnore]
        public List<Tutor> Tutors { get; set; }
    }
}