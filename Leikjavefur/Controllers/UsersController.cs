using System;
using System.Web.Mvc;
using Leikjavefur.Models;
using Leikjavefur.Models.Interfaces;
using Leikjavefur.Models.Repository;
using WebMatrix.WebData;

namespace Leikjavefur.Controllers
{   
    public class UsersController : Controller
    {
		private readonly IUserRepository _userRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public UsersController() : this(new UserRepository())
        {
        }

        public UsersController(IUserRepository userRepository)
        {
			this._userRepository = userRepository;
        }

        //
        // GET: /Players/

        public ViewResult Index()
        {
            return View(_userRepository.All);
        }

        public ActionResult UserList()
        {
            return PartialView(_userRepository.All);
        }

        public ActionResult FriendsList()
        {
            return PartialView(_userRepository.GetFriends(WebSecurity.CurrentUserId));
        }

        public ActionResult AddFriend(int id)
        {
            return PartialView(_userRepository.AddFriend(WebSecurity.CurrentUserId, id));
        }
        //
        // GET: /Players/Details/5

        public ViewResult Details(int id)
        {
            return View(_userRepository.Find(id));
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
                _userRepository.InsertOrUpdate(userProfile);
                _userRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /Players/Edit/5
 
        public ActionResult Edit(int id)
        {
             return View(_userRepository.Find(id));
        }

        //
        // POST: /Players/Edit/5

        [HttpPost]
        public ActionResult Edit(UserProfile userProfile)
        {
            if (ModelState.IsValid) {
                //UserProfile.DateCreated = DateTime.Now;
                _userRepository.InsertOrUpdate(userProfile);
                _userRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /Players/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(_userRepository.Find(id));
        }

        //
        // POST: /Players/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _userRepository.Delete(id);
            _userRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                _userRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

