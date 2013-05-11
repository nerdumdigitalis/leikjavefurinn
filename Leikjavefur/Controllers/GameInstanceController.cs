using System.Web.Mvc;
using Leikjavefur.Models;
using Leikjavefur.Models.Interfaces;
using Leikjavefur.Models.Repository;
using WebMatrix.WebData;

namespace Leikjavefur.Controllers
{
    public class GameInstanceController : Controller
    {
        private readonly IGameInstanceRepository _gameInstanceRepository;

        public GameInstanceController(): this(new GameInstanceRepository())
        {

        }

        public GameInstanceController(IGameInstanceRepository gameInstanceRepository)
        {
            this._gameInstanceRepository = gameInstanceRepository;
        }

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Create(int gameID, string gameName)
        {
            if (WebSecurity.IsAuthenticated)
            {

                var gameInstance =_gameInstanceRepository.CreateNewGameInstance(gameID, WebSecurity.CurrentUserId);
                _gameInstanceRepository.Save();
                return RedirectToAction(gameName, gameInstance);
            }
            return RedirectToAction("Login", "Account");
        }

        public ActionResult Join(GameInstance gameInstance)
        {
            var gameName = new GameRepository().Find(gameInstance.GameID).Name;
            _gameInstanceRepository.JoinActiveGameInstance(gameInstance, WebSecurity.CurrentUserId);
            return RedirectToAction(gameName, "GameInstance", gameInstance);
        }

        //public ActionResult TicTacToe()
        //{
        //    return View();
        //}

        public ActionResult TicTacToe(GameInstance gameInstance)
        {
            return View(gameInstance);
        }

        //public ActionResult SnakesAndLadders()
        //{
        //    return View();
        //}

        public ActionResult SnakesAndLadders(GameInstance gameInstance)
        {
            return View(gameInstance);
        }
    }
}
