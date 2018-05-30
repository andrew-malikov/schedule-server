using System;
using ScheduleServer.Libs;

namespace ScheduleServer.Models {
    public class SerializedTutorSchedule : SerializedSchedule {
        public Tutor Tutor { get; set; }
    }
}