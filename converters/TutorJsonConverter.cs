using System.Collections.Generic;
using Newtonsoft.Json.Linq;

using ScheduleServer.Models;

namespace ScheduleServer.Converters {
    public class TutorJsonConverter : IModelJsonConverter<Tutor> {
        public Tutor Convert(JToken value) {
            var tutor = new Tutor() {
                Code = value["id"].Value<string>(),
                LongName = value["title"].Value<string>(),
                ShortName = value["name"].Value<string>()
            };

            return tutor;
        }
    }
}