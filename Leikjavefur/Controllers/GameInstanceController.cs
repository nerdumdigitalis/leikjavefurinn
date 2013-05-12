using System.Web.Mvc;
using Leikjavefur.Models;
using Leikjavefur.Models.Interfaces;
using Leikjavefur.Models.Repository;
using Leikjavefur.ViewModels;
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
            _gameInstanceRepository = gameInstanceRepository;
        }

        public ActionResult Index()
        {
            return View(_gameInstanceRepository.GetGameInstances());
        }

        public ActionResult Create(int gameID, string gameName)
        {
            if (WebSecurity.IsAuthenticated)
            {

                var gameInstance =_gameInstanceRepository.CreateNewGameInstance(gameID, WebSecurity.CurrentUserId);
                _gameInstanceRepository.Save();
                return RedirectToAction("Game", gameInstance);
            }
            return RedirectToAction("Login", "Account");
        }

        public ActionResult Join(GameInstance gameInstance)
        {
            var gameName = new GameRepository().Find(gameInstance.GameID).Name;
            _gameInstanceRepository.JoinActiveGameInstance(gameInstance, WebSecurity.CurrentUserId);
            return RedirectToAction(gameName, "GameInstance", gameInstance);
        }

        public ActionResult DeleteGameInstance(GameInstance gameInstance)
        {
            _gameInstanceRepository.DeleteGameInstance(gameInstance);
            return RedirectToAction("Index");
        }

        public ActionResult GetUsersByGameInstance(string gameInstance)
        {
            
            return PartialView(_gameInstanceRepository.GetUsersByGameInstance(gameInstance));
        }

        public ActionResult Game(GameInstance gameInstance)
        {
            var players = _gameInstanceRepository.GetUsersByGameInstance(gameInstance.GameInstanceID);
            var game = new GameRepository().Find(gameInstance.GameID);
            var viewModel = new GameViewModel {GameInstance = gameInstance, Players = players, Game = game};

            return View(viewModel);
        }

        public ActionResult TicTacToe(GameInstance gameInstance)
        {
            return View(gameInstance);
        }

        public ActionResult SnakesAndLadders(GameInstance gameInstance)
        {
            return View(gameInstance);
        }
    }
}
