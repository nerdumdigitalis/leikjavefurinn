using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;

namespace Leikjavefur.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsAdmin { get; set; }
        public string About { get; set; }
    }
}