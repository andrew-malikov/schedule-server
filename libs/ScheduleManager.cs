using System.Threading.Tasks;

using ScheduleServer.Repositories;
using ScheduleServer.Models;
using ScheduleServer.Clients;
using ScheduleServer.Exceptions;

namespace ScheduleServer.Libs {
    public class ScheduleManager {
        protected UniversityContext context;
        protected FileRepository<string, Schedule> schedules;
        protected OsuGroupApi groupApi;
        protected OsuTutorApi tutorApi;

        public ScheduleManager(UniversityContext context, FileRepository<string, Schedule> schedules, OsuGroupApi groupApi, OsuTutorApi tutorApi) {
            this.context = context;
            this.schedules = schedules;
            this.groupApi = groupApi;
            this.tutorApi = tutorApi;
        }

        public async Task<GroupSchedule> GetGroupSchedule(Group group) {
            try {
                return await schedules.Get(group.Name) as GroupSchedule;
            }
            catch (NotFoundException) {
                var schedule = await groupApi.GetSchedule(group);

                schedules.Add(group.Name, schedule);

                return schedule;
            }
        }

        public async Task<TutorSchedule> GetTutorSchedule(Tutor tutor) {
            try {
                return await schedules.Get(tutor.ShortName) as TutorSchedule;
            }
            catch (NotFoundException) {
                var schedule = await tutorApi.GetSchedule(tutor);

                schedules.Add(tutor.ShortName, schedule);

                return schedule as TutorSchedule;
            }
        }
    }
}