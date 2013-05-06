using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;

namespace Leikjavefur.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Notendanafn")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Leyniorð")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Veffang")]
        public string Email { get; set; }

        [Display(Name = "Mynd (Valkvætt)")]
        public string Avatar { get; set; }

        public DateTime DateCreated { get; set; }

        public bool IsAdmin { get; set; }

        [Display(Name = "Stutt lýsing, max 200 stafir (Valkvætt)")]
        [StringLength(200, ErrorMessage="Styttu textan niður í max 200 stafi")]
        public string About { get; set; }
    }
}