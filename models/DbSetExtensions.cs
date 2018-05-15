using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace ScheduleServer.Models {
    public static class DbSetExtension {
        public static IIncludableQueryable<Group, Course> IncludeDependent(this DbSet<Group> set) {
            return set.Include(g => g.Faculty).Include(g => g.Course);
        }

        public static IIncludableQueryable<Tutor, Faculty> IncludeDependent(this DbSet<Tutor> set) {
            return set.Include(t => t.Department).ThenInclude(d => d.Faculty);
        }
    }
}