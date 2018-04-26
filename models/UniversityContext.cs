using Microsoft.EntityFrameworkCore;

namespace ScheduleServer.Models {
    public class UniversityContext : DbContext {
        public DbSet<Faculty> Faculties;
        public DbSet<Course> Courses;
        public DbSet<Group> Groups;
        public DbSet<Department> Departments;
        public DbSet<Tutor> Tutors;
        public DbSet<Room> Rooms;

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
        }
    }
}