using ScheduleServer.Models;

namespace ScheduleServer.Libs {
    public class SqliteScheduleUpdate : SchedulesUpdate {
        public SqliteScheduleUpdate(UniversityContext context) : base(context) {
        }

        protected override string SQL_UPDATE_GROUP_SCHEDULES_QUERY => "DELETE FROM GroupSchedules";
        protected override string SQL_UPDATE_TUTOR_SCHEDULES_QUERY => "DELETE FROM TutorSchedules";
    }
}