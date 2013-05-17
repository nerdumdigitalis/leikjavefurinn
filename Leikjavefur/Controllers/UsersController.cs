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

namespace Leikjavefur.Controllers
{   
    public class UsersController : Controller
    {
        private readonly IDataRepository _dataRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public UsersController() : this(new DataRepository())
        {
        }

        private UsersController(IDataRepository dataRepository)
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
            var allUsers = _dataRepository.UserRepository.All.ToList();
            allUsers.Remove(_dataRepository.UserRepository.Find(WebSecurity.CurrentUserId));
            var friends = _dataRepository.UserRepository.GetFriends(WebSecurity.CurrentUserId).ToList();
            List<UserProfile> notfriends = allUsers.Except(friends).ToList();

            var result = friends.Select(instance => new UserProfileViewModel
                                                        {
                                                            UserProfile = instance, IsFriend = true
                                                        }).ToList();
            result.AddRange(notfriends.Select(instance => new UserProfileViewModel
                                                              {
                                                                  UserProfile = instance, IsFriend = false
                                                              }));

            return PartialView(result);
        }

        public ActionResult FriendsList()
        {
            var friends = _dataRepository.UserRepository.GetFriends(WebSecurity.CurrentUserId).ToList();

            var result = friends.Select(instance => new UserProfileViewModel
                                                        {
                                                            UserProfile = instance, IsFriend = true
                                                        }).ToList();

            return PartialView(result);
        }

        public ActionResult AddFriend(int id)
        {
            _dataRepository.UserRepository.AddFriend(WebSecurity.CurrentUserId, id);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult RemoveFriend(int id)
        {
            _dataRepository.UserRepository.RemoveFriend(WebSecurity.CurrentUserId, id);
            return RedirectToAction("Index", "Home");
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
            }
            return View();
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
                //return RedirectToAction("Index","Games");
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

        public ActionResult ProfilePartial()
        {
            var loginViewModel = new LoginPartialViewModel();
            var myStats = _dataRepository.StatisticRepository.FindByUserId(WebSecurity.CurrentUserId);
            var myProfile = _dataRepository.UserRepository.Find(WebSecurity.CurrentUserId);

            loginViewModel.userProfile = myProfile;
            loginViewModel.wins = myStats.Wins;
            loginViewModel.gamesPlayed = myStats.GamesPlayed;

            return PartialView(loginViewModel);
        }

        public ActionResult ToggleRoleMembership(string username, string rolename)
        {
            if (Roles.IsUserInRole(username, rolename))
                Roles.RemoveUsersFromRoles(new[] { username }, new[] { rolename });
            else
                Roles.AddUsersToRoles(new[] { username }, new[] { rolename });

            return RedirectToAction("Index");
        }

    }
}

