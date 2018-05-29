using System.Threading.Tasks;

using ScheduleServer.Repositories;
using ScheduleServer.Models;
using ScheduleServer.Clients;
using ScheduleServer.Exceptions;
using System;
using System.Linq;

namespace ScheduleServer.Libs {
    public class ScheduleManager {
        protected UniversityContext context;
        protected OsuGroupApi groupApi;
        protected OsuTutorApi tutorApi;
        protected GroupSchedulesRepository groupSchedules;
        protected TutorSchedulesRepository tutorSchedules;

        public ScheduleManager(UniversityContext context, OsuGroupApi groupApi, OsuTutorApi tutorApi, GroupSchedulesRepository groupSchedules, TutorSchedulesRepository tutorSchedules) {
            this.context = context;
            this.groupApi = groupApi;
            this.tutorApi = tutorApi;
            this.groupSchedules = groupSchedules;
            this.tutorSchedules = tutorSchedules;
        }

        public async Task<SerializedGroupSchedule> GetGroupSchedule(Group group) {
            SerializedGroupSchedule serializedSchedule;

            try {
                serializedSchedule = context.GroupSchedules.SingleOrDefault(s => s.Group.Id == group.Id);
            }
            catch (ArgumentNullException) { serializedSchedule = null; }
            catch (InvalidOperationException) { serializedSchedule = null; }

            if (serializedSchedule == null) {
                var schedule = await groupApi.GetSchedule(group);
                serializedSchedule = groupSchedules.Insert(group, schedule);
                groupSchedules.Save();
            }

            return serializedSchedule;
        }

        public async Task<SerializedTutorSchedule> GetTutorSchedule(Tutor tutor) {
            SerializedTutorSchedule serializedSchedule;

            try {
                serializedSchedule = context.TutorSchedules.SingleOrDefault(s => s.Tutor.Id == tutor.Id);
            }
            catch (ArgumentNullException) { serializedSchedule = null; }
            catch (InvalidOperationException) { serializedSchedule = null; }

            if (serializedSchedule == null) {
                var schedule = await tutorApi.GetSchedule(tutor);
                serializedSchedule = tutorSchedules.Insert(tutor, schedule);
                tutorSchedules.Save();
            }

            return serializedSchedule;
        }
    }
}