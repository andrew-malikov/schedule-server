using System.Collections.Generic;

namespace ScheduleServer.Converters {
    public interface IModelConverter<T, V> : IConvertable<T, V> where T : IDictionary<string, object> {

    }
}