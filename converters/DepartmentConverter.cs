using System.Collections.Generic;

using ScheduleServer.Models;

namespace ScheduleServer.Converters {
    public class DepartmentConverter<T> : IModelConverter<T, Department> where T : IDictionary<string, object> {
        public Department Convert(T value) {
            var department = new Department();

            department.Code = (string)value["id"];
            department.FullName = (string)value["title"];
            department.ShortName = (string)value["name"];

            return department;
        }
    }
}