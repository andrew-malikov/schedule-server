using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using ScheduleServer.Libs;
using ScheduleServer.Models;

namespace ScheduleServer.Repositories {
    public class TutorSchedulesRepository : ITutorSchedulesRepository {
        protected UniversityContext context;
        protected ISerializable serializator;

        public TutorSchedulesRepository(UniversityContext context, JsonSerializator serializator) {
            this.context = context;
            this.serializator = serializator;
        }

        public IEnumerable<SerializedTutorSchedule> Get() {
            return context.TutorSchedules.ToList();
        }

        public SerializedTutorSchedule Get(int id) {
            return context.TutorSchedules.Find(id);
        }

        public Schedule GetSchedule(int id) {
            var serializedSchedule = Get(id);
            if (serializedSchedule == null) return null;
            return serializator.Deserialize<Schedule>(serializedSchedule.Value);
        }

        public SerializedTutorSchedule Insert(Tutor tutor, Schedule schedule) {
            var findedTutor = context.Tutors.Find(tutor.Id);
            var serializedSchedule = new SerializedTutorSchedule() {
                Value = serializator.Serialize(schedule),
                Tutor = findedTutor,
                TimeStamp = DateTime.Now
            };

            context.TutorSchedules.Add(serializedSchedule);
            return serializedSchedule;
        }

        public void Update(SerializedTutorSchedule schedule) {
            throw new System.NotImplementedException();
        }

        public void Delete(int id) {
            var findedSchedule = context.TutorSchedules.Find(id);
            if (findedSchedule == null) return;
            context.TutorSchedules.Remove(findedSchedule);
        }
        public void Save() {
            context.SaveChanges();
        }
        public void Dispose() {
            GC.SuppressFinalize(this);
        }
    }
}