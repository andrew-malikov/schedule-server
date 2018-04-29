using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace ScheduleServer.Converters {
    public interface IModelJsonConverter<V> : IConvertable<JToken, V> { }
}