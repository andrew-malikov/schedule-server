using System;
using System.Linq;
using System.Threading.Tasks;
using ScheduleServer.Clients;
using ScheduleServer.Models;
using ScheduleServer.Repositories;

namespace ScheduleServer.Libs {
    public class GroupScheduleManager : IScheduleManager<Group> {
        protected UniversityContext context;
        protected OsuGroupApi api;
        protected GroupSchedulesRepository schedules;

        public GroupScheduleManager(UniversityContext context, OsuGroupApi api, GroupSchedulesRepository schedules) {
            this.context = context;
            this.api = api;
            this.schedules = schedules;
        }

        public async Task<SerializedSchedule> GetSchedule(Group group) {
            SerializedGroupSchedule serializedSchedule;

            try {
                serializedSchedule = context.GroupSchedules.SingleOrDefault(s => s.Group.Id == group.Id);
            }
            catch (ArgumentNullException) { serializedSchedule = null; }
            catch (InvalidOperationException) { serializedSchedule = null; }

            if (serializedSchedule == null) {
                var schedule = await api.GetSchedule(group);
                serializedSchedule = schedules.Insert(group, schedule);
                schedules.Save();
            }

            return serializedSchedule;
        }
    }
}