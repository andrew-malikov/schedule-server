using System;
using ScheduleServer.Libs;

namespace ScheduleServer.Models {
    public class SerializedGroupSchedule : SerializedSchedule {
        public Group Group { get; set; }
    }
}