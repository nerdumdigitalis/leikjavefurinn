using System.Collections.Generic;
using System.Linq;
using Leikjavefur.Models;

namespace Leikjavefur.ViewModels
{
    public class MainPageViewModel
    {
        public IQueryable<Game> Games { get; set; }
        public List<UserProfile> Users { get; set; }
    }
}