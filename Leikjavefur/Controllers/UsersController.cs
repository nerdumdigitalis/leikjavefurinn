using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Leikjavefur.Models;
using Leikjavefur.Models.Interfaces;
using Leikjavefur.Models.Repository;

namespace Leikjavefur.Controllers
{   
    public class UsersController : Controller
    {
		private readonly IUserRepository userRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public UsersController() : this(new UserRepository())
        {
        }

        public UsersController(IUserRepository userRepository)
        {
			this.userRepository = userRepository;
        }

        //
        // GET: /Users/

        public ViewResult Index()
        {
            return View(userRepository.All);
        }

        //
        // GET: /Users/Details/5

        public ViewResult Details(int id)
        {
            return View(userRepository.Find(id));
        }

        //
        // GET: /Users/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Users/Create

        [HttpPost]
        public ActionResult Create(UserProfile userProfile)
        {
            if (ModelState.IsValid) {
                userProfile.DateCreated = DateTime.Now;
                userRepository.InsertOrUpdate(userProfile);
                userRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /Users/Edit/5
 
        public ActionResult Edit(int id)
        {
             return View(userRepository.Find(id));
        }

        //
        // POST: /Users/Edit/5

        [HttpPost]
        public ActionResult Edit(UserProfile userProfile)
        {
            if (ModelState.IsValid) {
                //UserProfile.DateCreated = DateTime.Now;
                userRepository.InsertOrUpdate(userProfile);
                userRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /Users/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(userRepository.Find(id));
        }

        //
        // POST: /Users/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            userRepository.Delete(id);
            userRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                userRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

