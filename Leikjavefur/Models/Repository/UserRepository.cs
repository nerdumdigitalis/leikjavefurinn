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

        public IQueryable<User> All
        {
            get { return _context.Users; }
        }

        public IQueryable<User> AllIncluding(params Expression<Func<User, object>>[] includeProperties)
        {
            IQueryable<User> query = _context.Users;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public User Find(int id)
        {
            return _context.Users.Find(id);
        }

        public void InsertOrUpdate(User user)
        {
            if (user.UserID == default(int)) {
                // New entity
                _context.Users.Add(user);
            } else {
                // Existing entity
                _context.Entry(user).State = EntityState.Modified;
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