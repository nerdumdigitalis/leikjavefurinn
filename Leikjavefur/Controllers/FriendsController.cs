using System;
using System.Web.Mvc;
using Leikjavefur.Models;
using Leikjavefur.Models.Interfaces;
using Leikjavefur.Models.Repository;
namespace Leikjavefur.Controllers
{ 
    public class FriendsController : Controller
    {

        private readonly IFriendsRepository friendsRepository;
        //
        // GET: /Friends/

      
       

        public FriendsController(IFriendsRepository friendsRepository)
        {
			this.friendsRepository = friendsRepository;
        }

        //
        // GET: /Users/

        public ViewResult Index()
        {
            return View(friendsRepository.All);
        }

    }
}
