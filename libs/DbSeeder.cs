using Microsoft.EntityFrameworkCore;

namespace ScheduleServer.Libs {
    public abstract class DbSeeder<T> where T : DbContext {
        protected T _context;

        public DbSeeder(T context) {
            _context = context;
        }

        public abstract void Migrate();

        public abstract void Create();

        public T context => _context;
    }
}