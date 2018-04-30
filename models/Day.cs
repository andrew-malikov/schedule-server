using System;
using System.Collections.Generic;

namespace ScheduleServer.Models {
    public class Day {
        public DateTime Date { get; set; }
        public List<Lesson> Lessons { get; set; }
    }
}