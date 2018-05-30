using System;

namespace ScheduleServer.Models {
    public class SerializedSchedule {
        public int Id { get; set; }
        public string Value { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}