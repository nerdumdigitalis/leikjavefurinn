using System.Web.Mvc;
using Leikjavefur.Models;
using Leikjavefur.Models.Interfaces;
using Leikjavefur.Models.Repository;

namespace Leikjavefur.Controllers
{   
    public class StatisticsController : Controller
    {
		private readonly IUserRepository userRepository;
		private readonly IGameRepository gameRepository;
		private readonly IStatisticRepository statisticRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public StatisticsController() : this(new UserRepository(), new GameRepository(), new StatisticRepository())
        {
        }

        public StatisticsController(IUserRepository userRepository, IGameRepository gameRepository, IStatisticRepository statisticRepository)
        {
			this.userRepository = userRepository;
			this.gameRepository = gameRepository;
			this.statisticRepository = statisticRepository;
        }

        //
        // GET: /Statistics/

        public ViewResult Index()
        {
            return View(statisticRepository.AllIncluding(statistic => statistic.UserID, statistic => statistic.GameID));
        }

        //
        // GET: /Statistics/Details/5

        public ViewResult Details(string id)
        {
            return View(statisticRepository.Find(id));
        }

        //
        // GET: /Statistics/Create

        public ActionResult Create()
        {
			ViewBag.PossibleUsers = userRepository.All;
			ViewBag.PossibleGames = gameRepository.All;
            return View();
        } 

        //
        // POST: /Statistics/Create

        [HttpPost]
        public ActionResult Create(Statistic statistic)
        {
            if (ModelState.IsValid) {
                statisticRepository.InsertOrUpdate(statistic);
                statisticRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleUsers = userRepository.All;
				ViewBag.PossibleGames = gameRepository.All;
				return View();
			}
        }
        
        //
        // GET: /Statistics/Edit/5
 
        public ActionResult Edit(string id)
        {
			ViewBag.PossibleUsers = userRepository.All;
			ViewBag.PossibleGames = gameRepository.All;
             return View(statisticRepository.Find(id));
        }

        //
        // POST: /Statistics/Edit/5

        [HttpPost]
        public ActionResult Edit(Statistic statistic)
        {
            if (ModelState.IsValid) {
                statisticRepository.InsertOrUpdate(statistic);
                statisticRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleUsers = userRepository.All;
				ViewBag.PossibleGames = gameRepository.All;
				return View();
			}
        }

        //
        // GET: /Statistics/Delete/5
 
        public ActionResult Delete(string id)
        {
            return View(statisticRepository.Find(id));
        }

        //
        // POST: /Statistics/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            statisticRepository.Delete(id);
            statisticRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                userRepository.Dispose();
                gameRepository.Dispose();
                statisticRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

