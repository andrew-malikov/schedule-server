using Newtonsoft.Json;

namespace ScheduleServer.Libs {
    public class JsonSerializator : ISerializable {
        public T Deserialize<T>(string value) {
            return JsonConvert.DeserializeObject<T>(value);
        }

        public string Serialize(object value) {
            return JsonConvert.SerializeObject(value);
        }
    }
}