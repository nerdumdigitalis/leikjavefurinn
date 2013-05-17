using System.Data;
using System.Data.Entity;

namespace Leikjavefur.Models.Context
{
    public class ApplicationContext : DbContext, IDataContext
    {
        public ApplicationContext() : base("DefaultConnection")
        {
        }

        public IDbSet<UserProfile> Users { get; set; }
        public IDbSet<Game> Games { get; set; }
        public IDbSet<Statistic> Statistics { get; set; }
        public IDbSet<Report> Reports { get; set; }
        public IDbSet<GameInstance> GameInstances { get; set; }
        public IDbSet<Friends> Friends { get; set; }
        public void SetModified(object entity)
        {
            Entry(entity).State = EntityState.Modified;
        }
        public IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

    }
}