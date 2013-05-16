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
        private readonly IDataContext _context;

        public GameInstanceRepository()
        {
            _context = new ApplicationContext();
        }

        public GameInstanceRepository(IDataContext dataContext)
        {
            _context = dataContext;
        }


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

        public GameInstance Find(string gameInstanceID)
        {
            return Enumerable.FirstOrDefault(_context.GameInstances.Where(instance => instance.GameInstanceID == gameInstanceID));
        }

        public void DeleteGameInstance(string gameInstanceId)
        {
            var allGameInst = All;
            foreach (var gameInst in allGameInst)
            {
                if (gameInst.GameInstanceID == gameInstanceId)
                {
                    _context.GameInstances.Remove(gameInst);
                }
            }
        }

        public List<GameInstance> GetGameInstances()
        {
            return All.GroupBy(element => element.GameInstanceID).Select(grp => grp.FirstOrDefault()).ToList();
            //return _context.GameInstances.Select(element => element.GameInstanceID).Distinct().ToList();

        }

        public IQueryable<UserProfile> GetUsersByGameInstance(string gameInstance)
        {
            var users = from c in _context.GameInstances
                        join o in _context.Users on c.UserID equals o.UserID
                        where c.GameInstanceID == gameInstance
                        select o;
            return users;
        }

        public List<GameInstance> GetGameInstancesByUser(int userID)
        {
            return new List<GameInstance>();
        }

        public int GetGameIDByGameInstanceID(string gameInstanceID)
        {
            foreach (var inst in _context.GameInstances.Where(inst => inst.GameInstanceID == gameInstanceID))
            {
                return inst.GameID;
            }
            return -1;
        }

        public GameInstance CreateNewGameInstance (int gameID, int currentUserID)
        {
            Guid g = Guid.NewGuid();
            var gameInstance = new GameInstance {GameID = gameID, UserID = currentUserID, GameInstanceID = g.ToString()};
            _context.GameInstances.Add(gameInstance);
            return gameInstance;
        }

        public void JoinGameInstance(GameInstance gameInstance, int currentUserID)
        {
            if (GetUsersByGameInstance(gameInstance.GameInstanceID).ToList().Exists(user => user.UserID == currentUserID)) return;
            var joinInstance = new GameInstance { GameID = gameInstance.GameID, UserID = currentUserID, GameInstanceID = gameInstance.GameInstanceID };
            _context.GameInstances.Add(joinInstance);
            Save();

        }

        public void ActivateGameInstance(string gameInstanceId)
        {
            foreach (var instance in All)
            {
                if (instance.GameInstanceID == gameInstanceId)
                {
                    instance.IsActive = true;
                }
            }
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