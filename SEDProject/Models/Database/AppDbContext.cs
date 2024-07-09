using Microsoft.EntityFrameworkCore;
using SEDProject.Models.Entities;

namespace SEDProject.Models.Database
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Department> Departments { get; set; }

        public DbSet<Participant> Participants { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                         .AddJsonFile("appsettings.json")
                         .SetBasePath(Directory.GetCurrentDirectory())
                         .Build();

            optionsBuilder.UseSqlServer(config
                .GetSection("ConnectionString").Value);
        }

    }
}
