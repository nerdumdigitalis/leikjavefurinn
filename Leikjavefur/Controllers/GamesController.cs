using System;
using System.Web.Mvc;
using Leikjavefur.Models;
using Leikjavefur.Models.Interfaces;
using Leikjavefur.Models.Repository;

namespace Leikjavefur.Controllers
{   
    public class GamesController : Controller
    {
		private readonly IGameRepository _gameRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public GamesController() : this(new GameRepository())
        {
        }

        public GamesController(IGameRepository gameRepository)
        {
			this._gameRepository = gameRepository;
        }

        //
        // GET: /Games/

        public ActionResult Index()
        {
            return View(_gameRepository.All);
        }

        //
        // GET: /Games/Details/5

        public ActionResult GamesList()
        {
            return PartialView(_gameRepository.All);
        }

        public ActionResult ActiveGamesList()
        {

            return PartialView();
        }

        public ViewResult Details(int id)
        {
            return View(_gameRepository.Find(id));
        }

        //
        // GET: /Games/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Games/Create

        [HttpPost]
        public ActionResult Create(Game game)
        {
            if (ModelState.IsValid) {
                game.DateAdded = DateTime.Now;
                _gameRepository.InsertOrUpdate(game);
                _gameRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /Games/Edit/5
 
        public ActionResult Edit(int id)
        {
             return View(_gameRepository.Find(id));
        }

        //
        // POST: /Games/Edit/5

        [HttpPost]
        public ActionResult Edit(Game game)
        {
            if (ModelState.IsValid) {
                //game.DateAdded = DateTime.Now;
                _gameRepository.InsertOrUpdate(game);
                _gameRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /Games/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(_gameRepository.Find(id));
        }

        //
        // POST: /Games/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _gameRepository.Delete(id);
            _gameRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                _gameRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

