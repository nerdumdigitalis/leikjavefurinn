using System.Collections.Generic;
using System;
using System.Web.Mvc;
using Leikjavefur.Models;
using Leikjavefur.Models.Interfaces;
using Leikjavefur.Models.Repository;
using WebMatrix.WebData;
using System.Web.Security;
using Leikjavefur.ViewModels;
using System.Linq;
using System.Linq.Expressions;

namespace Leikjavefur.Controllers
{   
    public class UsersController : Controller
    {
        private readonly IDataRepository _dataRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public UsersController() : this(new DataRepository())
        {
        }

        public UsersController(IDataRepository dataRepository)
        {
			_dataRepository = dataRepository;
        }

        //
        // GET: /Players/

        [Authorize(Roles = "Administrator")]
        public ViewResult Index()
        {
            return View(_dataRepository.UserRepository.All);
        }

        public ActionResult UserList()
        {
            var result = new List<UserProfileViewModel>();
            var allusers = _dataRepository.UserRepository.All.ToList();
            allusers.Remove(_dataRepository.UserRepository.Find(WebSecurity.CurrentUserId));
            var friends = _dataRepository.UserRepository.GetFriends(WebSecurity.CurrentUserId).ToList();
            List<UserProfile> notfriends = allusers.Except(friends).ToList();
   
            foreach (var instance in friends)
            {
                result.Add(new UserProfileViewModel
                {
                    UserProfile = instance,
                    IsFriend = true
                });
            }

            foreach (var instance in notfriends)
            {
                result.Add(new UserProfileViewModel
                {
                    UserProfile = instance,
                    IsFriend = false
                });
            }

            return PartialView(result);
        }

        public ActionResult FriendsList()
        {
            //return PartialView(_dataRepository.UserRepository.GetFriends(WebSecurity.CurrentUserId));
            var result = new List<UserProfileViewModel>();
            var friends = _dataRepository.UserRepository.GetFriends(WebSecurity.CurrentUserId).ToList();

            foreach (var instance in friends)
            {
                result.Add(new UserProfileViewModel
                {
                    UserProfile = instance,
                    IsFriend = true
                });
            }

            return PartialView(result);
        }

        public ActionResult AddFriend(int id)
        {
            //return PartialView(_userRepository.AddFriend(WebSecurity.CurrentUserId, id));
            _dataRepository.UserRepository.AddFriend(WebSecurity.CurrentUserId, id);
            return RedirectToAction("Details", new { id = id });
        }

        public ActionResult RemoveFriend(int id)
        {
            //return PartialView(_userRepository.AddFriend(WebSecurity.CurrentUserId, id));
            _dataRepository.UserRepository.RemoveFriend(WebSecurity.CurrentUserId, id);
            return RedirectToAction("Details", new { id = id });
        }

        //
        // GET: /Players/Details/5

        public ViewResult Details(int id)
        {
            return View(_dataRepository.UserRepository.Find(id));
        }

        //
        // GET: /Players/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Players/Create

        [HttpPost]
        public ActionResult Create(UserProfile userProfile)
        {
            if (ModelState.IsValid) {
                userProfile.DateCreated = DateTime.Now;
                _dataRepository.UserRepository.InsertOrUpdate(userProfile);
                _dataRepository.UserRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /Players/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View(_dataRepository.UserRepository.Find(id));
        }

        //
        // POST: /Players/Edit/5

        [HttpPost]
        public ActionResult Edit(UserProfile userProfile)
        {
            if (ModelState.IsValid) 
            {
                _dataRepository.UserRepository.InsertOrUpdate(userProfile);
                _dataRepository.UserRepository.Save();
                return RedirectToAction("Index","Games");
            }
            return View();
        }

        //
        // GET: /Players/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(_dataRepository.UserRepository.Find(id));
        }

        //
        // POST: /Players/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _dataRepository.UserRepository.Delete(id);
            _dataRepository.UserRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                _dataRepository.UserRepository.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult _ProfilePartial()
        {
            return PartialView(_dataRepository.UserRepository.Find(WebSecurity.CurrentUserId));
        }

        public ActionResult ToggleRoleMembership(string username, string rolename)
        {
            if (Roles.IsUserInRole(username, rolename))
                Roles.RemoveUsersFromRoles(new[] { username }, new[] { rolename });
            else
                Roles.AddUsersToRoles(new[] { username }, new[] { rolename });

            return RedirectToAction("Index");
        }

        //public bool IsFriend(int id)
        //{
        //    return (_dataRepository.UserRepository.IsFriend(WebSecurity.CurrentUserId, id));
        //}

    }
}

