using ScheduleServer.Libs;

namespace ScheduleServer.Models {
    public class Group {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public Faculty Faculty { get; set; }
        public Course Course { get; set; }

        public SerializedGroupSchedule Schedule { get; set; }
    }
}