using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Leikjavefur.Models;

namespace Leikjavefur.ViewModels
{
    public class UserProfileViewModel
    {
        public UserProfile UserProfile { get; set; }
        public bool IsFriend { get; set; }
    }
}