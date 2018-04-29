using System.Collections.Generic;

namespace ScheduleServer.Models {
    public class Department {
        public int Id { get; set; }
        public string Code { get; set; }
        public string FullName { get; set; }
        public string ShortName { get; set; }

        public Faculty Faculty { get; set; }

        public List<Tutor> Tutors { get; set; }
    }
}