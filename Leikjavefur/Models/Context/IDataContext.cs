using System;
using System.Data.Entity;

namespace Leikjavefur.Models.Context
{
    public interface IDataContext : IDisposable
    {
        DbSet<UserProfile> Users { get; set; }
        DbSet<Game> Games { get; set; }
        DbSet<Statistic> Statistics { get; set; }
        DbSet<Report> Reports { get; set; }
        DbSet<GameInstance> GameInstances { get; set; }
        DbSet<Friends> Friends { get; set; }
        int SaveChanges();
        void SetModified(object entity);
    }
}
