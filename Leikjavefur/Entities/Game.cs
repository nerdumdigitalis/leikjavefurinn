using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;

namespace Leikjavefur.Entities
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }
        public DateTime DateAdded { get; set; }
        public string Logo { get; set; }
        public string About { get; set; }
        public string Rules { get; set; }

    }
}