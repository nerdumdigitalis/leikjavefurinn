using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Leikjavefur.Models.Interfaces
{
    public interface IStatisticRepository : IDisposable
    {
        IQueryable<Statistic> All { get; }
        IQueryable<Statistic> AllIncluding(params Expression<Func<Statistic, object>>[] includeProperties);
        Statistic Find(string id);
        Statistic FindByUserId(int userID);
        Statistic FindByUserIdAndGameID(int userId, int gameId);
        List<Statistic> FindTopScoreForAll(int howToSort);
        void InsertOrUpdate(Statistic statistic);
        void Delete(string id);
        void Save();
        List<Statistic> GetStatisticsByGame(int gameId);
    }
}
