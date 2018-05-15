using System.Collections.Generic;
using Newtonsoft.Json;

namespace ScheduleServer.Models {
    public class Faculty {
        public int Id { get; set; }
        public string Code { get; set; }
        public string ShortName { get; set; }
        public string FullName { get; set; }

        [JsonIgnore]
        public List<Group> Groups { get; set; }
        [JsonIgnore]
        public List<Department> Departments { get; set; }
    }
}