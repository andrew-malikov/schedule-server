using System.Collections.Generic;
using Newtonsoft.Json.Linq;

using ScheduleServer.Models;

namespace ScheduleServer.Converters {
    public class TutorJsonConverter : IModelJsonConverter<IDictionary<string, JToken>, Tutor> {
        public Tutor Convert(IDictionary<string, JToken> value) {
            var tutor = new Tutor();

            tutor.Code = value["id"].Value<string>();
            tutor.FullName = value["title"].Value<string>();
            tutor.ShortName = value["name"].Value<string>();

            return tutor;
        }
    }
}