namespace ScheduleServer.Libs {
    public interface ISerializable {
        string Serialize(object value);
        T Deserialize<T>(string value);
    }
}