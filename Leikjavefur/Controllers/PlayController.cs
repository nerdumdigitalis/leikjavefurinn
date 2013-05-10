using System.Web.Mvc;
using WebMatrix.WebData;

namespace Leikjavefur.Controllers
{
    public class PlayController : Controller
    {
        //
        // GET: /Play/

        public ActionResult Index(string game)
        {
            return WebSecurity.IsAuthenticated ? RedirectToAction(game) : RedirectToAction("Login", "Account");
        }

        public ActionResult TicTacToe()
        {

            return View();
        }

        public ActionResult SnakesAndLadders()
        {
            return View();
        }

    }
}
