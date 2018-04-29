using System.Collections.Generic;

using ScheduleServer.Models;

namespace ScheduleServer.Converters {
    public class TutorConverter<T> : IModelConverter<T, Tutor> where T : IDictionary<string, object> {
        public Tutor Convert(T value) {
            var tutor = new Tutor();

            tutor.Code = (string)value["id"];
            tutor.FullName = (string)value["title"];
            tutor.ShortName = (string)value["name"];

            return tutor;
        }
    }
}