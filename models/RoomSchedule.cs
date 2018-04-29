namespace ScheduleServer.Models {
    public class RoomSchedule : Schedule {
        public Room Room { get; set; }

        public override string GetId() {
            return Room.Number;
        }
    }
}