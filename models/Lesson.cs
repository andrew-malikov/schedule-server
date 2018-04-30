using System.Collections.Generic;

namespace ScheduleServer.Models {
    public class Lesson {
        public List<Group> Groups { get; set; }
        public List<Tutor> Tutors { get; set; }
        public List<Room> Rooms { get; set; }
        public Discipline Discipline { get; set; }
        public Time Time { get; set; }
        public LessonType Type { get; set; }
    }
}