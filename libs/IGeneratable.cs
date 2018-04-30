namespace ScheduleServer.Libs {
    public interface IGeneratable<V, K> {
        K Generate(V value);
    }
}