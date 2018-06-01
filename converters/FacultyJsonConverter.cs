using System.Collections.Generic;
using Newtonsoft.Json.Linq;

using ScheduleServer.Models;

namespace ScheduleServer.Converters {
    public class FacultyJsonConverter : IModelJsonConverter<Faculty> {
        public Faculty Convert(JToken value) {
            var faculty = new Faculty() {
                Code = value["id"].Value<string>(),
                LongName = value["title"].Value<string>(),
                ShortName = value["name"].Value<string>()
            };

            return faculty;
        }
    }
}