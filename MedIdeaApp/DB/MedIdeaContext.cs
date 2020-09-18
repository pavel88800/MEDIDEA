using MedIdeaApp.DB;
using MedIdeaApp.DB.Models;
using Microsoft.EntityFrameworkCore;

namespace MedIdeaApp.Model
{
    internal class MedIdeaContext : DbContext
    {
        public MedIdeaContext()
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Treatment> Treatments { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=localhost;Initial Catalog=MedIdeaDb;Integrated Security=True;");
        }
    }
}