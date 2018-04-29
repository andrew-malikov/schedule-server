using System.Collections.Generic;
using Newtonsoft.Json.Linq;

using ScheduleServer.Models;

namespace ScheduleServer.Converters {
    public class CourseJsonConverter : IModelJsonConverter<Course> {
        public Course Convert(JToken value) {
            var course = new Course();

            course.Code = value["id"].Value<string>();
            course.Name = value["name"].Value<string>();
            course.Number = value["id"].Value<int>();

            return course;
        }
    }
}