using System;
using System.Linq;
using System.Linq.Expressions;

namespace Leikjavefur.Models.Interfaces
{
    public interface IUserRepository : IDisposable
    {
        IQueryable<UserProfile> All { get; }
        IQueryable<UserProfile> AllIncluding(params Expression<Func<UserProfile, object>>[] includeProperties);
        UserProfile Find(int id);
        void InsertOrUpdate(UserProfile userProfile);
        void Delete(int id);
        void Save();
    }
}
