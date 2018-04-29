using System.Collections.Generic;

using ScheduleServer.Models;

namespace ScheduleServer.Converters {
    public class CourseConverter<T> : IModelConverter<T, Course> where T : IDictionary<string, object> {
        public Course Convert(T value) {
            var course = new Course();

            course.Code = (string)value["id"];
            course.Name = (string)value["name"];
            course.Number = (int)value["id"];

            return course;
        }
    }
}