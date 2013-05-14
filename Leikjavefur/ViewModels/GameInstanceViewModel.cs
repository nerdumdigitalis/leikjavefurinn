using System.Linq;
using Leikjavefur.Models;

namespace Leikjavefur.ViewModels
{
    public class GameInstanceViewModel
    {
        public GameInstance GameInstance { get; set; }
        public Game Game { get; set; }
        public IQueryable<UserProfile> Players { get; set; }
    }
}