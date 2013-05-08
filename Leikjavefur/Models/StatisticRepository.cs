using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Leikjavefur.Models
{ 
    public class StatisticRepository : IStatisticRepository
    {
        LeikjavefurContext context = new LeikjavefurContext();

        public IQueryable<Statistic> All
        {
            get { return context.Statistics; }
        }

        public IQueryable<Statistic> AllIncluding(params Expression<Func<Statistic, object>>[] includeProperties)
        {
            IQueryable<Statistic> query = context.Statistics;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Statistic Find(string id)
        {
            return context.Statistics.Find(id);
        }

        public void InsertOrUpdate(Statistic statistic)
        {
            if (statistic.Id == default(string)) {
                // New entity
                context.Statistics.Add(statistic);
            } else {
                // Existing entity
                context.Entry(statistic).State = EntityState.Modified;
            }
        }

        public void Delete(string id)
        {
            var statistic = context.Statistics.Find(id);
            context.Statistics.Remove(statistic);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose() 
        {
            context.Dispose();
        }
    }

    public interface IStatisticRepository : IDisposable
    {
        IQueryable<Statistic> All { get; }
        IQueryable<Statistic> AllIncluding(params Expression<Func<Statistic, object>>[] includeProperties);
        Statistic Find(string id);
        void InsertOrUpdate(Statistic statistic);
        void Delete(string id);
        void Save();
    }
}