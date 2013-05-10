using System.Web.Mvc;
using Leikjavefur.Models.Context;
using Leikjavefur.Models.Repository;
using Leikjavefur.ViewModels;

namespace Leikjavefur.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var appContext = new ApplicationContext();
            //var gameRep = new GameRepository();
            //var mainPageViewModel = new MainPageViewModel();
            //mainPageViewModel.Games = gameRep.All;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
