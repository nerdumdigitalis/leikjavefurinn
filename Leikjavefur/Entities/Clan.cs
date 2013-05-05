﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Leikjavefur.Entities
{
    public class Clan
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public string Logo { get; set; }
        public int[] Members { get; set; }
    }
}