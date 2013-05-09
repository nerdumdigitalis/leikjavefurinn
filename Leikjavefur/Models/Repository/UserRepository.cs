using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Leikjavefur.Models.Context;
using Leikjavefur.Models.Interfaces;

namespace Leikjavefur.Models.Repository
{ 
    public class UserRepository : IUserRepository
    {
        readonly ApplicationContext _context = new ApplicationContext();

        public IQueryable<UserProfile> All
        {
            get { return _context.Users; }
        }

        public IQueryable<UserProfile> AllIncluding(params Expression<Func<UserProfile, object>>[] includeProperties)
        {
            IQueryable<UserProfile> query = _context.Users;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public UserProfile Find(int id)
        {
            return _context.Users.Find(id);
        }

        public void InsertOrUpdate(UserProfile userProfile)
        {
            if (userProfile.UserID == default(int)) {
                // New entity
                _context.Users.Add(userProfile);
            } else {
                // Existing entity
                _context.Entry(userProfile).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var user = _context.Users.Find(id);
            _context.Users.Remove(user);
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