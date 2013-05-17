using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Leikjavefur.Models.Context;
using Leikjavefur.Models.Interfaces;

namespace Leikjavefur.Models.Repository
{ 
    public class GameRepository : IGameRepository
    {
        private readonly IDataContext _context;

        public GameRepository()
        {
            _context = new ApplicationContext();
        }

        public GameRepository(IDataContext dataContext)
        {
            _context = dataContext;
        }



        public IQueryable<Game> All
        {
            get { return _context.Games; }
        }

        public IQueryable<Game> AllIncluding(params Expression<Func<Game, object>>[] includeProperties)
        {
            IQueryable<Game> query = _context.Games;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Game Find(int id)
        {
            return _context.Games.Find(id);
        }

        public void InsertOrUpdate(Game game)
        {
            if (game.GameID == default(int)) {
                // New entity
                _context.Games.Add(game);
            } else {
                // Existing entity
                _context.SetModified(game);
            }
        }

        public void Delete(int id)
        {
            var game = _context.Games.Find(id);
            _context.Games.Remove(game);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose() 
        {
            _context.Dispose();
        }

        public Game GetGameByGameID(int gameID)
        {
            return _context.Games.Find(gameID);
        }
    }

}