using ScheduleServer.Clients;
using ScheduleServer.Models;

namespace ScheduleServer.Libs {
    public class SqliteUniversityUpdater : UniversityUpdater {
        public SqliteUniversityUpdater(UniversityContext context, OsuApi osuApi) : base(context, osuApi) { }

        protected override void Clear() {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}