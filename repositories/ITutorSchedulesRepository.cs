using System;
using System.Collections.Generic;

using ScheduleServer.Libs;
using ScheduleServer.Models;

namespace ScheduleServer.Repositories {
    public interface ITutorSchedulesRepository : IDisposable {
        IEnumerable<SerializedTutorSchedule> Get();
        SerializedTutorSchedule Get(int id);
        Schedule GetSchedule(int id);
        SerializedTutorSchedule Insert(Tutor tutor, Schedule schedule);
        void Delete(int id);
        void Update(SerializedTutorSchedule schedule);
        void Save();
    }
}
