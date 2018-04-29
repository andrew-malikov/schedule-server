using System.Collections.Generic;

using ScheduleServer.Models;

namespace ScheduleServer.Converters {
    public class GroupConverter<T> : IModelConverter<T, Group> where T : IDictionary<string, object> {
        public Group Convert(T value) {
            var group = new Group();

            group.Code = (string)value["id"];
            group.Name = (string)value["name"];

            return group;
        }
    }
}