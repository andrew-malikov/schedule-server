using System;
using ScheduleServer.Libs;

namespace ScheduleServer.Models {
    public class SerializedTutorSchedule {
        public int Id { get; set; }
        public string Value { get; set; }
        public DateTime TimeStamp { get; set; }

        public Tutor Tutor { get; set; }
    }
}