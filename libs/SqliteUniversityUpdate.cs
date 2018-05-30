using ScheduleServer.Clients;
using ScheduleServer.Models;

namespace ScheduleServer.Libs {
    public class SqliteUniversityUpdate : UniversityUpdate {
        public SqliteUniversityUpdate(UniversityContext context, OsuApi osuApi) : base(context, osuApi) { }

        protected override void Clear() {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}