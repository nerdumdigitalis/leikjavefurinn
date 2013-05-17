using Leikjavefur.Models;

namespace Leikjavefur.ViewModels
{
    public class LoginPartialViewModel
    {
        public UserProfile userProfile {get;set;}
        public int wins { get; set; }
        public int gamesPlayed { get; set; }
    }
}