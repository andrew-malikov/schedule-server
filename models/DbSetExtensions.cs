using Microsoft.EntityFrameworkCore;

namespace ScheduleServer.Models {
    public static class DbSetExtension {
        public static DbSet<Group> IncludeDependent(this DbSet<Group> set) {
            return set.Include(g => g.Faculty).Include(g => g.Course) as DbSet<Group>;
        }

        public static DbSet<Tutor> IncludeDependent(this DbSet<Tutor> set) {
            return set.Include(t => t.Department).ThenInclude(d => d.Faculty) as DbSet<Tutor>;
        }
    }
}