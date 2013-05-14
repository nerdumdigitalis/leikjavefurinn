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
        private readonly IDataRepository _dataRepository;


        public GameInstanceController(): this(new DataRepository())
        {

        }

        private GameInstanceController(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public ActionResult Index()
        {
            var result = new List<GameInstanceViewModel>();
            var instances = _dataRepository.GameInstanceRepository.GetGameInstances();

            foreach (var instance in instances)
            {
                result.Add(new GameInstanceViewModel
                               {
                                   GameInstance = instance,
                                   Game = _dataRepository.GameRepository.GetGameByGameID(_dataRepository.GameInstanceRepository.GetGameIDByGameInstanceID(instance.GameInstanceID)),
                                   Players = _dataRepository.GameInstanceRepository.GetUsersByGameInstance(instance.GameInstanceID)
                               });

            }
            return PartialView(result);
        }

        public ActionResult Create(int gameID, string gameName)
        {
            if (WebSecurity.IsAuthenticated)
            {
                var gameInstance = _dataRepository.GameInstanceRepository.CreateNewGameInstance(gameID, WebSecurity.CurrentUserId);
                _dataRepository.GameInstanceRepository.Save();
                return RedirectToAction("Game", gameInstance);
            }
            return RedirectToAction("Login", "Account");
        }

        public ActionResult Join(string gameInstanceID)
        {
            if (WebSecurity.IsAuthenticated)
            {
                var gameInstance = _dataRepository.GameInstanceRepository.Find(gameInstanceID);
                _dataRepository.GameInstanceRepository.JoinGameInstance(gameInstance, WebSecurity.CurrentUserId);
                return RedirectToAction("Game", gameInstance);
            }
            return RedirectToAction("Login", "Account");
        }

        public ActionResult DeleteGameInstance(string gameInstance)
        {
            _dataRepository.GameInstanceRepository.DeleteGameInstance(gameInstance);
            return RedirectToAction("Index");
        }

        public ActionResult GetUsersByGameInstance(string gameInstance)
        {

            return PartialView(_dataRepository.GameInstanceRepository.GetUsersByGameInstance(gameInstance));
        }

        public ActionResult Game(GameInstance gameInstance)
        {
            var players = _dataRepository.GameInstanceRepository.GetUsersByGameInstance(gameInstance.GameInstanceID);
            var game = _dataRepository.GameRepository.Find(gameInstance.GameID);
            var viewModel = new GameInstanceViewModel {GameInstance = gameInstance, Players = players, Game = game};

            return View(viewModel);
        }

        public void ActivateGameInstance(GameInstance gameInstance)
        {
            _dataRepository.GameInstanceRepository.ActivateGameInstance(gameInstance);
        }

    }
}
