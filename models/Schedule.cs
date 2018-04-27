using System.Collections.Generic;

namespace ScheduleServer.Models {
    public abstract class Schedule {
        public List<Lesson> Lessons { get; set; }
    }
}