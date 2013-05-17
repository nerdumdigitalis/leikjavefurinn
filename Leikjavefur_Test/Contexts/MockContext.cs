using System;
using System.Data;
using System.Reflection;
using Leikjavefur.Models;
using Leikjavefur.Models.Context;
using System.Data.Entity;
using Leikjavefur_Test.Data;

namespace Leikjavefur_Test.Contexts
{
    public class MockContext : DbContext, IDataContext
    {
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

        public new IDbSet<T> Set<T>() where T : class
        {
            foreach (PropertyInfo property in typeof(MockContext).GetProperties())
            {
                if (property.PropertyType == typeof(IDbSet<T>))
                    return property.GetValue(this, null) as IDbSet<T>;
            }
            throw new Exception("Type collection not found");
        }

        public MockContext()
        {
            GameInstances = new FakeDbSet<GameInstance>
            {
                
                                   new GameInstance {GameID = 1, GameInstanceID = "1", IsActive = true, UserID = 1},
                                   new GameInstance {GameID = 1, GameInstanceID = "2", IsActive = true, UserID = 2},
                                   new GameInstance {GameID = 2, GameInstanceID = "3", IsActive = true, UserID = 1},
                                   new GameInstance {GameID = 2, GameInstanceID = "3", IsActive = true, UserID = 2},
                                   new GameInstance {GameID = 2, GameInstanceID = "3", IsActive = true, UserID = 3},
                                   new GameInstance {GameID = 3, GameInstanceID = "4", IsActive = false, UserID = 5},
                                   new GameInstance {GameID = 3, GameInstanceID = "4", IsActive = false, UserID = 6}
            };

            Users = new FakeDbSet<UserProfile>
                        {
                            new UserProfile {About = "trall1", Avatar = "trall1", DateCreated = DateTime.Now, Email = "emailtrall1", Friends = null, UserID = 1, UserName = "Siggi"},
                            new UserProfile {About = "trall2", Avatar = "trall2", DateCreated = DateTime.Now, Email = "emailtrall2", Friends = null, UserID = 2, UserName = "Sverrir"},
                            new UserProfile {About = "trall3", Avatar = "trall3", DateCreated = DateTime.Now, Email = "emailtrall3", Friends = null, UserID = 3, UserName = "Jakob"},
                            new UserProfile {About = "trall4", Avatar = "trall4", DateCreated = DateTime.Now, Email = "emailtrall4", Friends = null, UserID = 4, UserName = "Natan"},
                            new UserProfile {About = "trall5", Avatar = "trall5", DateCreated = DateTime.Now, Email = "emailtrall5", Friends = null, UserID = 5, UserName = "Björgvin"},
                            new UserProfile {About = "trall6", Avatar = "trall6", DateCreated = DateTime.Now, Email = "emailtrall6", Friends = null, UserID = 6, UserName = "Chuck Norris"},
                            new UserProfile {About = "trall7", Avatar = "trall7", DateCreated = DateTime.Now, Email = "emailtrall7", Friends = null, UserID = 7, UserName = "Peter Griffin"},
                        };
        }
    }
}