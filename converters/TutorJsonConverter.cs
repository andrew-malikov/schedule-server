using System.Collections.Generic;
using Newtonsoft.Json.Linq;

using ScheduleServer.Models;

namespace ScheduleServer.Converters {
    public class TutorJsonConverter : IModelJsonConverter<Tutor> {
        public Tutor Convert(JToken value) {
            var tutor = new Tutor();

            tutor.Code = value["id"].Value<string>();
            tutor.FullName = value["title"].Value<string>();
            tutor.ShortName = value["name"].Value<string>();

            return tutor;
        }
    }
}