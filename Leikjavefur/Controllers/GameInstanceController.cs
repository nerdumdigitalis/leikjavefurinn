using System.Collections.Generic;
using System.Linq;
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
            var result = new List<GameInstanceViewModel>();
            var instanceIDs = _gameInstanceRepository.GetGameInstancesID();

            foreach (var id in instanceIDs)
            {
                result.Add(new GameInstanceViewModel
                               {
                                   GameInstanceID = id,
                                   GameName = new GameRepository().GetGameByGameID(_gameInstanceRepository.GetGameIDByGameInstanceID(id)).Name,
                                   Players = _gameInstanceRepository.GetUsersByGameInstance(id)
                               });

            }
            return PartialView(result);
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

        public ActionResult Join(string gameInstanceID)
        {
            if (WebSecurity.IsAuthenticated)
            {
                var gameInstance = _gameInstanceRepository.Find(gameInstanceID);
                _gameInstanceRepository.JoinActiveGameInstance(gameInstance, WebSecurity.CurrentUserId);
                return RedirectToAction("Game", gameInstance);
            }
            return RedirectToAction("Login", "Account");
        }

        public ActionResult DeleteGameInstance(GameInstance gameInstance)
        {
            _gameInstanceRepository.DeleteGameInstance(gameInstance.GameInstanceID);
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
            var viewModel = new GameInstanceViewModel {GameInstanceID = gameInstance.GameInstanceID, Players = players, GameName = game.Name};

            return View(viewModel);
        }

    }
}
