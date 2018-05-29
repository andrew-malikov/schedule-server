using System;
using System.Collections.Generic;

using ScheduleServer.Libs;
using ScheduleServer.Models;

namespace ScheduleServer.Repositories {
    public interface IGroupSchedulesRepository : IDisposable {
        IEnumerable<SerializedGroupSchedule> Get();
        SerializedGroupSchedule Get(int id);
        Schedule GetSchedule(int id);
        SerializedGroupSchedule Insert(Group group, Schedule schedule);
        void Delete(int id);
        void Update(SerializedGroupSchedule schedule);
        void Save();
    }
}
