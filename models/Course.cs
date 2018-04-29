using System.Collections.Generic;

namespace ScheduleServer.Models {
    public class Course {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }

        public List<Group> Groups { get; set; }
    }
}