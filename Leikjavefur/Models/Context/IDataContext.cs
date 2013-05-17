using System;
using System.Data.Entity;

namespace Leikjavefur.Models.Context
{
    public interface IDataContext : IDisposable
    {
        IDbSet<UserProfile> Users { get; set; }
        IDbSet<Game> Games { get; set; }
        IDbSet<Statistic> Statistics { get; set; }
        IDbSet<Report> Reports { get; set; }
        IDbSet<GameInstance> GameInstances { get; set; }
        IDbSet<Friends> Friends { get; set; }
        int SaveChanges();
        void SetModified(object entity);
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}
