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
    public class UserRepository : IUserRepository
    {
        readonly ApplicationContext _context = new ApplicationContext();

        public IQueryable<UserProfile> All
        {
            get
            {
                //return _context.Users;  // skilar öllum notendum
                return (from user in _context.Users // skilar öllum notendum nema UserID=1 (admin notandanum)
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
            if (!IsFriend(currentUserId, friendsId))
            {
                Friends newFriend = new Friends();
                newFriend.UserID = currentUserId;
                newFriend.FriendID = friendsId;
                _context.Friends.Add(newFriend);
                _context.SaveChanges();
            }
            //return "Index";
        }

        public void RemoveFriend(int currentUserId, int friendsId)
        {
            if (IsFriend(currentUserId, friendsId))
            {
                _context.Friends.Remove(GetFriend(currentUserId, friendsId));
                _context.SaveChanges();
            }
            //return "Index";
        }

        public bool IsFriend(int currentUserId, int friendsId)
        {
            if (GetFriend(currentUserId, friendsId) == null)
                return false;
            else
                return true;
        }
    }
}
