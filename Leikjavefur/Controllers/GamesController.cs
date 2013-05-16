using System;
using System.Web.Mvc;
using Leikjavefur.Models;
using Leikjavefur.Models.Interfaces;
using Leikjavefur.Models.Repository;
using Leikjavefur.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Leikjavefur.Controllers
{   
    public class GamesController : Controller
    {
		private readonly IDataRepository _dataRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public GamesController() : this(new DataRepository())
        {
        }

        private GamesController(IDataRepository dataRepository)
        {
			_dataRepository = dataRepository;
        }

        //
        // GET: /Games/

        public ActionResult Index()
        {
            return View(_dataRepository.GameRepository.All);
        }

        //
        // GET: /Games/Details/5

        public ActionResult GamesList()
        {
            //return PartialView(_dataRepository.GameRepository.All);
            var resultTopTenWithUsername = new List<StatisticsTopTenWithUsernameViewModel>();
            var resultTopTenWithUsernameAndGame = new List<StatisticViewModel>();
            var allgames = _dataRepository.GameRepository.All;

            foreach (var gameInstance in allgames)
            {
                var statsForGame = _dataRepository.StatisticRepository.GetStatisticsByGame(gameInstance.GameID).ToList();

                foreach (var stat in statsForGame)
                {
                    resultTopTenWithUsername.Add(new StatisticsTopTenWithUsernameViewModel
                    {
                        Statistic = statsForGame,
                        UserName = _dataRepository.UserRepository.Find(stat.UserID).UserName
                    });
                }

                resultTopTenWithUsernameAndGame.Add(new StatisticViewModel
                {
                    StatisticWithUsername = resultTopTenWithUsername,
                    Game = _dataRepository.GameRepository.Find(gameInstance.GameID),
                });
            }

            return PartialView(resultTopTenWithUsernameAndGame);
        }

        public ViewResult Details(int id)
        {
            return View(_dataRepository.GameRepository.Find(id));
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
            if (ModelState.IsValid) 
            {
                game.DateAdded = DateTime.Now;
                _dataRepository.GameRepository.InsertOrUpdate(game);
                _dataRepository.GameRepository.Save();
                return RedirectToAction("Index");
            }
            return View();
        }

        //
        // GET: /Games/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View(_dataRepository.GameRepository.Find(id));
        }

        //
        // POST: /Games/Edit/5

        [HttpPost]
        public ActionResult Edit(Game game)
        {
            if (ModelState.IsValid) 
            {
                //game.DateAdded = DateTime.Now;
                _dataRepository.GameRepository.InsertOrUpdate(game);
                _dataRepository.GameRepository.Save();
                return RedirectToAction("Index");
            }
            return View();
        }

        //
        // GET: /Games/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(_dataRepository.GameRepository.Find(id));
        }

        //
        // POST: /Games/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _dataRepository.GameRepository.Delete(id);
            _dataRepository.GameRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                _dataRepository.GameRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

