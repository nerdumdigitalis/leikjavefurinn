using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Leikjavefur.Models;

namespace Leikjavefur.ViewModels
{
    public class StatisticsTopTenWithUsernameViewModel
    {
        public IEnumerable<Statistic> Statistic { get; set; }
        public string UserName { get; set; }
    }
}