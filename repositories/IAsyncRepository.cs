using System;
using System.Threading.Tasks;

namespace ScheduleServer.Repositories {
    public interface IAsyncRepository<K, V> : IDisposable {
        void Add(K key, V value);
        Task<V> Get(K key);
        void Remove(K key);
        void RemoveAll();
    }
}