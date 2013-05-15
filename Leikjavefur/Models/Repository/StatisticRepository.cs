using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Leikjavefur.Models.Context;
using Leikjavefur.Models.Interfaces;

namespace Leikjavefur.Models.Repository
{ 
    public class StatisticRepository : IStatisticRepository
    {
        readonly ApplicationContext _context = new ApplicationContext();

        public IQueryable<Statistic> All
        {
            get { return _context.Statistics; }
        }

        public IQueryable<Statistic> AllIncluding(params Expression<Func<Statistic, object>>[] includeProperties)
        {
            IQueryable<Statistic> query = _context.Statistics;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Statistic Find(string id)
        {
            return _context.Statistics.Find(id);
        }

        public Statistic FindByUserIdAndGameID(int userId, int gameId)
        {
            return (from Stats in _context.Statistics
                              where Stats.UserID == userId
                              &&  Stats.GameID == gameId
                              select Stats).FirstOrDefault();
        }

        public void InsertOrUpdate(Statistic statistic)
        {
            if (statistic.Id == default(string)) {
                // New entity
                _context.Statistics.Add(statistic);
            } else {
                // Existing entity
                _context.Entry(statistic).State = EntityState.Modified;
            }
        }

        public void Delete(string id)
        {
            var statistic = _context.Statistics.Find(id);
            _context.Statistics.Remove(statistic);
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