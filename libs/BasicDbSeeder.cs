using Microsoft.EntityFrameworkCore;

namespace ScheduleServer.Libs {
    public class BasicDbSeeder<T> : DbSeeder<T> where T : DbContext {
        public BasicDbSeeder(T context) : base(context) {
        }

        public override void Migrate() {
            context.Database.Migrate();
        }

        public override void Create() {
            context.Database.EnsureCreated();
        }
    }
}