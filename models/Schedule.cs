using System.Collections.Generic;

namespace ScheduleServer.Models {
    public class Schedule {
        public List<Day> Days { get; set; }

        public T ConvertTo<T>(T value) where T : Schedule {
            value.Days = Days;

            return value;
        }
    }
}