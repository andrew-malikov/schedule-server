using System;
using ScheduleServer.Libs;

namespace ScheduleServer.Models {
    public class SerializedGroupSchedule {
        public int Id { get; set; }
        public string Value { get; set; }
        public DateTime TimeStamp { get; set; }

        public Group Group { get; set; }
    }
}