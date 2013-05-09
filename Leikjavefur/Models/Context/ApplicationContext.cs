using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Leikjavefur.Models.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext()
            : base("DefaultConnection")
        {
        }
        public DbSet<UserProfile> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Statistic> Statistics { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<GameInstance> GameInstances { get; set; }

    }
}