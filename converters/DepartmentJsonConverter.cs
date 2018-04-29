using System.Collections.Generic;
using Newtonsoft.Json.Linq;

using ScheduleServer.Models;

namespace ScheduleServer.Converters {
    public class DepartmentJsonConverter : IModelJsonConverter<Department> {
        public Department Convert(JToken value) {
            var department = new Department();

            department.Code = value["id"].Value<string>();
            department.FullName = value["title"].Value<string>();
            department.ShortName = value["name"].Value<string>();

            return department;
        }
    }
}