using System.Collections.Generic;
using Newtonsoft.Json.Linq;

using ScheduleServer.Models;

namespace ScheduleServer.Converters {
    public class GroupJsonConverter : IModelJsonConverter<IDictionary<string, JToken>, Group> {
        public Group Convert(IDictionary<string, JToken> value) {
            var group = new Group();

            group.Code = value["id"].Value<string>();
            group.Name = value["name"].Value<string>();

            return group;
        }
    }
}