using System.Web.Mvc;
using Leikjavefur.ViewModels;
using WebMatrix.WebData;

namespace Leikjavefur.Controllers
{
    public class ChatController : Controller
    {
        public ActionResult ChatPartial()
        {
            if (!WebSecurity.IsAuthenticated)
            {
                var model = new ChatModel
                {
                    ID = 0,
                    UserName = "",
                    GameInstance = 0
                };
                return PartialView(model);
            }
                
            var model2 = new ChatModel
            {
                ID = WebSecurity.CurrentUserId,
                UserName = WebSecurity.CurrentUserName,
                GameInstance = 0
            };
            return PartialView(model2);
        }

    }
}
