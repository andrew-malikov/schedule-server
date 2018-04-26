using System.Collections.Generic;

namespace ScheduleServer.Models {
    public class Faculty {
        public int Id { get; set; }
        public string Code { get; set; }
        public string ShortName { get; set; }
        public string FullName { get; set; }

        public List<Group> Groups { get; set; }
        public List<Department> Departments { get; set; }
    }
}