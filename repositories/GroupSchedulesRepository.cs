using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using ScheduleServer.Libs;
using ScheduleServer.Models;

namespace ScheduleServer.Repositories {
    public class GroupSchedulesRepository : IGroupSchedulesRepository {
        protected UniversityContext context;
        protected ISerializable serializator;

        public GroupSchedulesRepository(UniversityContext context, JsonSerializator serializator) {
            this.context = context;
            this.serializator = serializator;
        }

        public IEnumerable<SerializedGroupSchedule> Get() {
            return context.GroupSchedules.ToList();
        }

        public SerializedGroupSchedule Get(int id) {
            return context.GroupSchedules.Find(id);
        }

        public Schedule GetSchedule(int id) {
            var serializedSchedule = Get(id);
            if (serializedSchedule == null) return null;
            return serializator.Deserialize<Schedule>(serializedSchedule.Value);
        }

        public SerializedGroupSchedule Insert(Group group, Schedule schedule) {
            var findedGroup = context.Groups.Find(group.Id);
            var serializedSchedule = new SerializedGroupSchedule() {
                Value = serializator.Serialize(schedule),
                Group = findedGroup,
                TimeStamp = DateTime.Now
            };

            context.GroupSchedules.Add(serializedSchedule);
            return serializedSchedule;
        }

        public void Update(SerializedGroupSchedule schedule) {
            throw new System.NotImplementedException();
        }

        public void Delete(int id) {
            var findedSchedule = context.GroupSchedules.Find(id);
            if (findedSchedule == null) return;
            context.GroupSchedules.Remove(findedSchedule);
        }
        public void Save() {
            context.SaveChanges();
        }
        public void Dispose() {
            GC.SuppressFinalize(this);
        }
    }
}