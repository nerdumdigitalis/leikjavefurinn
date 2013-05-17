using System.Collections.Generic;
using Leikjavefur.Models;

namespace Leikjavefur.ViewModels
{
    public class StatisticTopTenWithUsernameAndGame
    {
        public IEnumerable<StatisticViewModel> StatisticWithUsername { get; set; }
        public Game Game { get; set; }
    }
}