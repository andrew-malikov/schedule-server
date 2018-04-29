using System.Collections.Generic;

using ScheduleServer.Models;

namespace ScheduleServer.Converters {
    public class FacultyConverter<T> : IModelConverter<T, Faculty> where T : IDictionary<string, object> {
        public Faculty Convert(T value) {
            var faculty = new Faculty();

            faculty.Code = (string)value["id"];
            faculty.FullName = (string)value["title"];
            faculty.ShortName = (string)value["name"];

            return faculty;
        }
    }
}