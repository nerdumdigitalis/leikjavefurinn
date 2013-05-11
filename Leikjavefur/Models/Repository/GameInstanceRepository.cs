using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Leikjavefur.Models.Context;
using Leikjavefur.Models.Interfaces;

namespace Leikjavefur.Models.Repository
{
    public class GameInstanceRepository : IGameInstanceRepository
    {
        readonly ApplicationContext _context = new ApplicationContext();

        public IQueryable<GameInstance> All
        {
            get { return _context.GameInstances; }
        }

        public IQueryable<GameInstance> AllIncluding(params Expression<Func<GameInstance, object>>[] includeProperties)
        {
            IQueryable<GameInstance> query = _context.GameInstances;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public GameInstance Find(string gameInstance)
        {
            return _context.GameInstances.Find(gameInstance);
        }

        public List<GameInstance> GetGameInstances()
        {
            var groups = _context.GameInstances.GroupBy(inst => inst.GameInstanceID);
            var instances = new List<GameInstance>();
            foreach (var instance in groups)
            {
                 //instances.Add(instance.Key.);                 
            }
            return new List<GameInstance>();
        }

        public List<UserProfile> GetUsersByGameInstance(int gameInstance)
        {
            return new List<UserProfile>();
        }

        public List<GameInstance> GetGameInstancesByUser(int userID)
        {
            return new List<GameInstance>();
        }

        public GameInstance CreateNewGameInstance (int gameID, int currentUserID)
        {
            Guid g = Guid.NewGuid();
            var gameInstance = new GameInstance {GameID = gameID, UserID = currentUserID, GameInstanceID = g.ToString()};
            _context.GameInstances.Add(gameInstance);
            return gameInstance;
        }

        public void JoinActiveGameInstance(GameInstance gameInstance, int currentUserID)
        {
            var joinInstance = new GameInstance { GameID = gameInstance.GameID, UserID = currentUserID, GameInstanceID = gameInstance.GameInstanceID };
            _context.GameInstances.Add(joinInstance);
        }


        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}