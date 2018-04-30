using System.Collections.Generic;

namespace ScheduleServer.Models {
    public abstract class Schedule {
        public List<Day> Days { get; set; }

        public abstract string GetId();
    }
}