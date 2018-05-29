using Microsoft.EntityFrameworkCore;
using ScheduleServer.Libs;

namespace ScheduleServer.Models {
    public class UniversityContext : DbContext {
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Tutor> Tutors { get; set; }
        public DbSet<SerializedGroupSchedule> GroupSchedules { get; set; }
        public DbSet<SerializedTutorSchedule> TutorSchedules { get; set; }

        public UniversityContext(DbContextOptions<UniversityContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Group>()
            .HasOne(g => g.Faculty)
            .WithMany(f => f.Groups);

            modelBuilder.Entity<Group>()
            .HasOne(g => g.Course)
            .WithMany(c => c.Groups);

            modelBuilder.Entity<Department>()
            .HasOne(d => d.Faculty)
            .WithMany(f => f.Departments);

            modelBuilder.Entity<Tutor>()
            .HasOne(t => t.Department)
            .WithMany(d => d.Tutors);

            modelBuilder.Entity<Group>()
            .HasOne(g => g.Schedule)
            .WithOne(s => s.Group)
            .IsRequired(false);

            modelBuilder.Entity<Tutor>()
            .HasOne(t => t.Schedule)
            .WithOne(s => s.Tutor)
            .IsRequired(false);
        }
    }
}