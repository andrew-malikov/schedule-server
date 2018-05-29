using ScheduleServer.Libs;

namespace ScheduleServer.Models {
    public class Tutor {
        public int Id { get; set; }
        public string Code { get; set; }
        public string FullName { get; set; }
        public string ShortName { get; set; }

        public Department Department { get; set; }

        public SerializedTutorSchedule Schedule { get; set; }
    }
}