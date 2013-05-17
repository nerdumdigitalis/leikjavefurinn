using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Leikjavefur.Models;

namespace Leikjavefur.ViewModels
{
    public class StatisticTopTenWithUsernameAndGame
    {
        public IEnumerable<StatisticViewModel> StatisticWithUsername { get; set; }
        public Game Game { get; set; }
    }
}