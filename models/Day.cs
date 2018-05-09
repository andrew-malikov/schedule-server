using System.Collections.Generic;

namespace ScheduleServer.Models {
    public class Day {
        public Date Date { get; set; }
        public List<Lesson> Lessons { get; set; }
    }
}