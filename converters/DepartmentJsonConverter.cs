using System.Collections.Generic;
using Newtonsoft.Json.Linq;

using ScheduleServer.Models;

namespace ScheduleServer.Converters {
    public class DepartmentJsonConverter : IModelJsonConverter<IDictionary<string, JToken>, Department> {
        public Department Convert(IDictionary<string, JToken> value) {
            var department = new Department();

            department.Code = value["id"].Value<string>();
            department.FullName = value["title"].Value<string>();
            department.ShortName = value["name"].Value<string>();

            return department;
        }
    }
}