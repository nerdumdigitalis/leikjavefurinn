using System;
using System.Linq;
using System.Linq.Expressions;

namespace Leikjavefur.Models.Interfaces
{
    public interface IFriendsRepository: IDisposable
  
    {
        IQueryable<Friends> All { get; }
        IQueryable<Friends> AllIncluding(params Expression<Func<Friends, object>>[] includeProperties);
        Friends Find(int id);
        void InsertOrUpdate(Friends friends);
        void Delete(int id);
        void Save();
    }
}
