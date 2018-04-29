namespace ScheduleServer.Models {
    public class TutorSchedule : Schedule {
        public Tutor Tutor { get; set; }

        public override string GetId() {
            return Tutor.ShortName;
        }
    }
}