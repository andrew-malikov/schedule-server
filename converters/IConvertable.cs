namespace ScheduleServer.Converters {
    public interface IConvertable<T, V> {
        V Convert(T value);
    }
}