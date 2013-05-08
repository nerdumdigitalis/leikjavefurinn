using System;
using System.Linq;
using System.Linq.Expressions;

namespace Leikjavefur.Models.Interfaces
{
    public interface IGameRepository : IDisposable
    {
        IQueryable<Game> All { get; }
        IQueryable<Game> AllIncluding(params Expression<Func<Game, object>>[] includeProperties);
        Game Find(int id);
        void InsertOrUpdate(Game game);
        void Delete(int id);
        void Save();
    }

}
