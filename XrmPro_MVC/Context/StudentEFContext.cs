using Microsoft.EntityFrameworkCore;
using XrmPro_MVC.Models;
namespace XrmPro_MVC.Context
{
    public class StudentEFContext : DbContext
    {
        public StudentEFContext(DbContextOptions<StudentEFContext> options)
            : base(options)
        {
        }


        public DbSet<StudentEFModel> students;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=students-ef.db");
        }

        public DbSet<StudentEFModel> StudentEFModel { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentEFModel>().ToTable("StudentEF");
        }
    }
}
