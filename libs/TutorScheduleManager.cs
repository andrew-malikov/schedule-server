using System;
using System.Linq;
using System.Threading.Tasks;
using ScheduleServer.Clients;
using ScheduleServer.Models;
using ScheduleServer.Repositories;

namespace ScheduleServer.Libs {
    public class TutorScheduleManager : IScheduleManager<Tutor> {
        protected UniversityContext context;
        protected OsuTutorApi api;
        protected TutorSchedulesRepository schedules;

        public TutorScheduleManager(UniversityContext context, OsuTutorApi api, TutorSchedulesRepository schedules) {
            this.context = context;
            this.api = api;
            this.schedules = schedules;
        }

        public async Task<SerializedSchedule> GetSchedule(Tutor tutor) {
            SerializedTutorSchedule serializedSchedule;

            try {
                serializedSchedule = context.TutorSchedules.SingleOrDefault(s => s.Tutor.Id == tutor.Id);
            }
            catch (ArgumentNullException) { serializedSchedule = null; }
            catch (InvalidOperationException) { serializedSchedule = null; }

            if (serializedSchedule == null) {
                var schedule = await api.GetSchedule(tutor);
                serializedSchedule = schedules.Insert(tutor, schedule);
                schedules.Save();
            }

            return serializedSchedule;
        }
    }
}