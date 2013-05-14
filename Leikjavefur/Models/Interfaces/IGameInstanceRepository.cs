using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Leikjavefur.Models.Interfaces
{
    public interface IGameInstanceRepository : IDisposable
    {
        IQueryable<GameInstance> All { get; }
        IQueryable<GameInstance> AllIncluding(params Expression<Func<GameInstance, object>>[] includeProperties);
        GameInstance Find(string gameInstanceID);
        GameInstance CreateNewGameInstance(int gameID, int currentUserID);
        void JoinGameInstance(GameInstance gameInstance, int currentUserID);
        void DeleteGameInstance(string gameInstanceId);
        List<GameInstance> GetGameInstances();
        IQueryable<UserProfile> GetUsersByGameInstance(string gameInstanceID);
        List<GameInstance> GetGameInstancesByUser(int userID);
        int GetGameIDByGameInstanceID(string gameInstanceID);
        void ActivateGameInstance(GameInstance gameInstance);
        void Save();
        void Dispose();
    }
}