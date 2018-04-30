using System.Collections.Generic;
using Newtonsoft.Json.Linq;

using ScheduleServer.Models;

namespace ScheduleServer.Converters {
    public class CourseJsonConverter : IModelJsonConverter<Course> {
        public Course Convert(JToken value) {
            var course = new Course() {
                Code = value["id"].Value<string>(),
                Name = value["name"].Value<string>(),
                Number = value["id"].Value<int>()
            };

            return course;
        }
    }
}