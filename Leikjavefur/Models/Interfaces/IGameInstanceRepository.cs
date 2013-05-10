using System;
using System.Linq;
using System.Linq.Expressions;

namespace Leikjavefur.Models.Interfaces
{
    public interface IGameInstanceRepository : IDisposable
    {
        IQueryable<GameInstance> All { get; }
        IQueryable<GameInstance> AllIncluding(params Expression<Func<GameInstance, object>>[] includeProperties);
    }
}