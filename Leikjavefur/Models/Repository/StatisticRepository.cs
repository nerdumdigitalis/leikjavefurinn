using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Leikjavefur.Models.Context;
using Leikjavefur.Models.Interfaces;
using System.Collections.Generic;

namespace Leikjavefur.Models.Repository
{ 
    public class StatisticRepository : IStatisticRepository
    {
        readonly IDataContext _context;


        public StatisticRepository()
        {
            _context = new ApplicationContext();
        }

        public StatisticRepository(IDataContext dataContext)
        {
            _context = dataContext;
        }

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

        public List<Statistic> FindAllByUserId(int userId)
        {
            var PlayerList =  (from Stats in _context.Statistics
                    select Stats).ToList();


            List<Statistic> playTotalScoreArray = new List<Statistic>();

            foreach (var item in PlayerList)
            {
                playTotalScoreArray[item.UserID].Wins += item.Wins;
                playTotalScoreArray[item.UserID].Losses += item.Wins;
                playTotalScoreArray[item.UserID].Draws += item.Wins;
                playTotalScoreArray[item.UserID].GamesPlayed += item.Wins;
                playTotalScoreArray[item.UserID].UserID = item.UserID;
            }
            playTotalScoreArray = playTotalScoreArray.OrderByDescending(x => x.Wins).ToList();
            playTotalScoreArray = playTotalScoreArray.Take(10).ToList();

            return playTotalScoreArray;
        }

        public void InsertOrUpdate(Statistic statistic)
        {
            if (statistic.Id == default(int)) {
                // New entity
                _context.Statistics.Add(statistic);
            } else {
                // Existing entity
                _context.SetModified(statistic);
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

        public List<Statistic> GetStatisticsByGame(int gameId)
        {
            return (from stats in _context.Statistics
                    where stats.GameID == gameId
                    select stats).Take(10).ToList();
        }
    }

}