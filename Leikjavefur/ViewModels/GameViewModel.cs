using System.Linq;
using Leikjavefur.Models;

namespace Leikjavefur.ViewModels
{
    public class GameViewModel
    {
        public Game Game { get; set; }
        public IQueryable<UserProfile> Players { get; set; }
        public GameInstance GameInstance { get; set; }
    }
}