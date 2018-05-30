using System.Threading.Tasks;
using ScheduleServer.Models;

namespace ScheduleServer.Libs {
    public interface IScheduleManager<T> {
        Task<SerializedSchedule> GetSchedule(T entity);
    }
}