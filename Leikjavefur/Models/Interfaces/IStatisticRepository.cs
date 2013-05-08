using System;
using System.Linq;
using System.Linq.Expressions;

namespace Leikjavefur.Models.Interfaces
{
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
