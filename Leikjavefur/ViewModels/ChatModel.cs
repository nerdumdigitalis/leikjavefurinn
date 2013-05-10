using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Leikjavefur.ViewModels
{
    public class ChatModel
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public int gameInstance { get; set; }
    }
}