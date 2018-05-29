using Microsoft.EntityFrameworkCore;

using ScheduleServer.Models;
using ScheduleServer.Clients;

namespace ScheduleServer.Libs {
    public abstract class SchedulesUpdate : IUpdatable {
        protected UniversityContext context;
        protected abstract string SQL_UPDATE_GROUP_SCHEDULES_QUERY { get; }
        protected abstract string SQL_UPDATE_TUTOR_SCHEDULES_QUERY { get; }

        public SchedulesUpdate(UniversityContext context) {
            this.context = context;
        }

        public void Update() {
            context.Database.ExecuteSqlCommandAsync(SQL_UPDATE_GROUP_SCHEDULES_QUERY);
            context.Database.ExecuteSqlCommandAsync(SQL_UPDATE_TUTOR_SCHEDULES_QUERY);
        }
    }
}