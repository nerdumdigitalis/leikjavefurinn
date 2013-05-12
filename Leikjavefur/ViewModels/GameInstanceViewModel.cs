using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Leikjavefur.Models;

namespace Leikjavefur.ViewModels
{
    public class GameInstanceViewModel
    {
        public string GameInstanceID { get; set; }
        public string GameName { get; set; }
        public IQueryable<UserProfile> Players { get; set; }
        public int CurrentUser { get; set; }
        public string CurrentUserName { get; set; }
    }
}