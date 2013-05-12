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

        public GameInstance Find(string gameInstanceID)
        {
            return Enumerable.FirstOrDefault(_context.GameInstances.Where(instance => instance.GameInstanceID == gameInstanceID));
        }

        public void DeleteGameInstance(GameInstance gameInstance)
        {
            var allGameInst = All;
            foreach (var gameInst in allGameInst)
            {
                if (gameInst.GameInstanceID == gameInstance.GameInstanceID)
                    _context.GameInstances.Remove(gameInst);
            }
        }

        public List<string> GetGameInstancesID()
        {

            return _context.GameInstances.Select(element => element.GameInstanceID).Distinct().ToList();

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

        public void JoinActiveGameInstance(GameInstance gameInstance, int currentUserID)
        {
            if (GetUsersByGameInstance(gameInstance.GameInstanceID).ToList().Exists(user => user.UserID == currentUserID)) return;
            var joinInstance = new GameInstance { GameID = gameInstance.GameID, UserID = currentUserID, GameInstanceID = gameInstance.GameInstanceID };
            _context.GameInstances.Add(joinInstance);
            Save();

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