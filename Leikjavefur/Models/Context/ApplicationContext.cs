using System.Data;
using System.Data.Entity;

namespace Leikjavefur.Models.Context
{
    public class ApplicationContext : DbContext, IDataContext
    {
        public ApplicationContext() : base("DefaultConnection")
        {
        }

        public DbSet<UserProfile> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Statistic> Statistics { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<GameInstance> GameInstances { get; set; }
        public DbSet<Friends> Friends { get; set; }
        public void SetModified(object entity)
        {
            Entry(entity).State = EntityState.Modified;
        }
    }
}