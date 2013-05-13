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
            var instanceIDs = _dataRepository.GameInstanceRepository.GetGameInstancesID();

            foreach (var id in instanceIDs)
            {
                result.Add(new GameInstanceViewModel
                               {
                                   GameInstanceID = id,
                                   GameName = _dataRepository.GameRepository.GetGameByGameID(_dataRepository.GameInstanceRepository.GetGameIDByGameInstanceID(id)).Name,
                                   Players = _dataRepository.GameInstanceRepository.GetUsersByGameInstance(id)
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
                _dataRepository.GameInstanceRepository.JoinActiveGameInstance(gameInstance, WebSecurity.CurrentUserId);
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
            var viewModel = new GameInstanceViewModel {GameInstanceID = gameInstance.GameInstanceID, Players = players, GameName = game.Name};

            return View(viewModel);
        }

    }
}
