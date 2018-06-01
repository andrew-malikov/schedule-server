using System.Collections.Generic;
using Newtonsoft.Json.Linq;

using ScheduleServer.Models;

namespace ScheduleServer.Converters {
    public class DepartmentJsonConverter : IModelJsonConverter<Department> {
        public Department Convert(JToken value) {
            var department = new Department() {
                Code = value["id"].Value<string>(),
                LongName = value["title"].Value<string>(),
                ShortName = value["name"].Value<string>()
            };

            return department;
        }
    }
}