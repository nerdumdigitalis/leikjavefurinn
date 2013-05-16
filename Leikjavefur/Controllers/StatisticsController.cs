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
    public class StatisticsController : Controller
    {
		private readonly IDataRepository _dataRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public StatisticsController() : this(new DataRepository())
        {
        }

        private StatisticsController(IDataRepository dataRepository)
        {
			_dataRepository = dataRepository;
        }

        //
        // GET: /Statistics/

        public ViewResult Index()
        {
            return View(_dataRepository.StatisticRepository.AllIncluding(statistic => statistic.UserID, statistic => statistic.GameID));
        }

        //
        // GET: /Statistics/Details/5

        public ViewResult Details(string id)
        {
            return View(_dataRepository.StatisticRepository.Find(id));
        }

        //
        // GET: /Statistics/Create

        public ActionResult Create()
        {
            ViewBag.PossibleUsers = _dataRepository.UserRepository.All;
            ViewBag.PossibleGames = _dataRepository.GameRepository.All;
            return View();
        } 

        //
        // POST: /Statistics/Create

        [HttpPost]
        public ActionResult Create(Statistic statistic)
        {
            if (ModelState.IsValid) 
            {
                _dataRepository.StatisticRepository.InsertOrUpdate(statistic);
                _dataRepository.StatisticRepository.Save();
                return RedirectToAction("Index");
            }
            ViewBag.PossibleUsers = _dataRepository.UserRepository.All;
            ViewBag.PossibleGames = _dataRepository.GameRepository.All;
            return View();
        }
        
        //
        // GET: /Statistics/Edit/5
 
        public ActionResult Edit(string id)
        {
            ViewBag.PossibleUsers = _dataRepository.UserRepository.All;
            ViewBag.PossibleGames = _dataRepository.GameRepository.All;
            return View(_dataRepository.StatisticRepository.Find(id));
        }

        //
        // POST: /Statistics/Edit/5

        [HttpPost]
        public ActionResult Edit(Statistic statistic)
        {
            if (ModelState.IsValid) 
            {
                _dataRepository.StatisticRepository.InsertOrUpdate(statistic);
                _dataRepository.StatisticRepository.Save();
                return RedirectToAction("Index");
            }
            ViewBag.PossibleUsers = _dataRepository.UserRepository.All;
            ViewBag.PossibleGames = _dataRepository.GameRepository.All;
            return View();
        }

        //
        // GET: /Statistics/Delete/5
 
        public ActionResult Delete(string id)
        {
            return View(_dataRepository.StatisticRepository.Find(id));
        }

        //
        // POST: /Statistics/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            _dataRepository.StatisticRepository.Delete(id);
            _dataRepository.StatisticRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) 
            {
                _dataRepository.UserRepository.Dispose();
                _dataRepository.GameRepository.Dispose();
                _dataRepository.StatisticRepository.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}

