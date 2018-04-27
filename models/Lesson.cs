namespace ScheduleServer.Models {
    public class Lesson {
        public Group Group { get; set; }
        public Tutor Tutor { get; set; }
        public Room Room { get; set; }
        public Discipline Discipline { get; set; }
        public Time Time { get; set; }
        public LessonType Type { get; set; }
    }
}