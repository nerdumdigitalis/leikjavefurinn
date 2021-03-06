using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Leikjavefur.Models.Context;
using Leikjavefur.Models.Interfaces;
using System.Collections.Generic;

namespace Leikjavefur.Models.Repository
{ 
    public class UserRepository : IUserRepository
    {
        private readonly IDataContext _context;


        public UserRepository()
        {
            _context = new ApplicationContext();
        }

        public UserRepository(IDataContext dataContext)
        {
            _context = dataContext;
        }

        public IQueryable<UserProfile> All
        {
            get
            {
                //return _context.Users;  // skilar �llum notendum
                return (from user in _context.Users // skilar �llum notendum nema UserID=1 (admin notandanum)
                        where user.UserID != 1
                        select user);
            }
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
                _context.SetModified(userProfile);
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

        public List<UserProfile> GetFriends(int currentUserId)
        {
            var friendsIDs = (from friend in _context.Friends
                              join user in _context.Users on friend.UserID equals user.UserID
                              where friend.UserID == currentUserId
                              select friend.FriendID).ToList();

            return friendsIDs.Select(Find).ToList();
        }

        public Friends GetFriend(int currentUserId, int friendsId)
        {
            return (from friend in _context.Friends
                    where friend.UserID == currentUserId
                    && friend.FriendID == friendsId
                    select friend).SingleOrDefault();
        }

        public void AddFriend(int currentUserId, int friendsId)
        {
            if (IsFriend(currentUserId, friendsId)) return;
            var newFriend = new Friends {UserID = currentUserId, FriendID = friendsId};
            _context.Friends.Add(newFriend);
            _context.SaveChanges();
        }

        public void RemoveFriend(int currentUserId, int friendsId)
        {
            if (!IsFriend(currentUserId, friendsId)) return;
            _context.Friends.Remove(GetFriend(currentUserId, friendsId));
            _context.SaveChanges();
        }


        public bool IsFriend(int currentUserId, int friendsId)
        {
            return GetFriend(currentUserId, friendsId) != null;
        }
    }
}
