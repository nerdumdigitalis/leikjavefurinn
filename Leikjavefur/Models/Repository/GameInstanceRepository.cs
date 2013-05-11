using System;
using System.Data;
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

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}