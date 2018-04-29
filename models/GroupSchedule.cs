namespace ScheduleServer.Models {
    public class GroupSchedule : Schedule {
        public Group Group { get; set; }

        public override string GetId() {
            return Group.Name;
        }
    }
}