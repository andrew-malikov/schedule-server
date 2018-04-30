using System.Collections.Generic;
using Newtonsoft.Json.Linq;

using ScheduleServer.Models;

namespace ScheduleServer.Converters {
    public class GroupJsonConverter : IModelJsonConverter<Group> {
        public Group Convert(JToken value) {
            var group = new Group() {
                Code = value["id"].Value<string>(),
                Name = value["name"].Value<string>()
            };

            return group;
        }
    }
}