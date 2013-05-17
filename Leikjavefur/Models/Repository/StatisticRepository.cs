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

        public Statistic FindByUserId(int userID)
        {
            var myStats = (from Stats in _context.Statistics
                           where Stats.UserID == userID
                           select Stats).ToList();

            var singleStat= myStats.GroupBy(x => x.UserID)
                   .Select(y => new
                   {
                       UserId = y.Key,
                       Wins = y.Sum(i => i.Wins),
                       GamesPlayed = y.Sum(i => i.GamesPlayed)
                   }).FirstOrDefault();

            Statistic newStats = new Statistic();
            if (singleStat == null)
            {
                newStats.Wins = 0;
                newStats.GamesPlayed = 0;
                newStats.UserID = 0;
            }
            else
            {
                newStats.Wins = singleStat.Wins;
                newStats.GamesPlayed = singleStat.GamesPlayed;
                newStats.UserID = singleStat.UserId;
            }


            return newStats;
        }

        public Statistic FindByUserIdAndGameID(int userId, int gameId)
        {
            return (from Stats in _context.Statistics
                              where Stats.UserID == userId
                              &&  Stats.GameID == gameId
                              select Stats).FirstOrDefault();
        }

        public List<Statistic> FindTopScoreForAll(int howToSort)
        {
            var PlayerList =  (from Stats in _context.Statistics
                    select Stats).ToList();

            var sortedList = PlayerList.GroupBy(x => x.UserID)
                    .Select(y => new
                    {
                        UserId = y.Key,
                        Wins = y.Sum(i => i.Wins),
                        Losses = y.Sum(i => i.Losses),
                        Draws = y.Sum(i => i.Draws),
                        GamesPlayed = y.Sum(i => i.GamesPlayed)
                    }).ToList();

            List<Statistic> playTotalScoreList = new List<Statistic>();
            foreach (var item in sortedList)
            {
                Statistic tempStats = new Statistic();
                tempStats.Wins = item.Wins;
                tempStats.Losses = item.Losses;
                tempStats.Draws = item.Draws;
                tempStats.GamesPlayed = item.GamesPlayed;
                tempStats.UserID = item.UserId;

                playTotalScoreList.Add(tempStats);
            }

            if(howToSort == 1)
                playTotalScoreList = playTotalScoreList.OrderByDescending(x => x.Wins).ToList();
            else if(howToSort == 2)
                playTotalScoreList = playTotalScoreList.OrderByDescending(x => x.GamesPlayed).ToList();
            else if(howToSort == 3)
                playTotalScoreList = playTotalScoreList.OrderByDescending(x => x.Points).ToList();

            playTotalScoreList = playTotalScoreList.Take(10).ToList();
            return playTotalScoreList;
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