using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Leikjavefur.Models.Context
{
    public class MockContext : DbContext
    {
        public MockContext()
        {
            //Users = UsersDB.AsQueryable();
            //Games = GamesDB.AsQueryable();
            //Statistics = StatisticsDB.AsQueryable();
            //Reports = ReportsDB.AsQueryable();
            //GameInstances = GameInstancesDB.AsQueryable();
            //Friends = FriendsDB.AsQueryable();
        }



        public DbSet<UserProfile> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Statistic> Statistics { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<GameInstance> GameInstances { get; set; }
        public DbSet<Friends> Friends { get; set; }

        
    }
}