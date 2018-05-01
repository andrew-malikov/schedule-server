using Microsoft.EntityFrameworkCore;

namespace ScheduleServer.Configuration {
    public class ConfigurationContext : DbContext {
        public ConfigurationContext(DbContextOptions options) : base(options) { }

        public DbSet<ConfigurationValue> Values { get; set; }
    }
}