using System.Collections.Generic;

namespace ScheduleServer.Models {
    public class Course {
        public int id { get; set; }
        public string Code { get; set; }
        public int Number { get; set; }

        public List<Group> Groups { get; set; }
    }
}