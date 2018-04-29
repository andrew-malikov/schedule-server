using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace ScheduleServer.Converters {
    public interface IModelJsonConverter<T, V> : IConvertable<T, V> where T : IDictionary<string, JToken> {

    }
}