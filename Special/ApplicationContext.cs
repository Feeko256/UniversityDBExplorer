using Microsoft.EntityFrameworkCore;
using UniversityDBExplorer.Models;

namespace UniversityDBExplorer.Special
{
    public class ApplicationContext : DbContext
    {
        public DbSet<FacultetModel> Facultets { get; set; } = null!;
        public DbSet<CafedraModel> Cafedras { get; set; } = null!;
        public DbSet<GroupModel> Groups { get; set; } = null!;
        public DbSet<StudentModel> Students { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=University.db");

        }
    }
}
