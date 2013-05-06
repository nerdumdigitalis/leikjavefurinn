using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;
using Leikjavefur.Entities;

namespace Leikjavefur.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }

        public DbSet<User> Users { get; set; }
    }

    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Veffang")]
        public string Email { get; set; }

        [Display(Name = "Mynd (Valkvætt)")]
        public string Avatar { get; set; }

        public DateTime DateCreated { get; set; }

        public bool IsAdmin { get; set; }

        [Display(Name = "Stutt lýsing, max 200 stafir (Valkvætt)")]
        [StringLength(200, ErrorMessage = "Styttu textan niður í max 200 stafi")]
        public string About { get; set; }
    }

    public class RegisterExternalLoginModel
    {
        [Required]
        [Display(Name = "Notandanafn")]
        public string UserName { get; set; }

        public string ExternalLoginData { get; set; }
    }

    public class LocalPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Núverandi lykilorð")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} verður að vera a.m.k {2} stafa langt.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nýtt lykilorð")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Staðfestu nýja lykilorðið")]
        [Compare("NewPassword", ErrorMessage = "Lykilorðin stemma ekki")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        /*
            Mögulega þarf að bæta hér inn "Forgot password" og "Register".
            -Natan
         * Það verða í raun ActionLinks sem sjá um það redirection
         * -Siggi
         */
        [Required]
        [Display(Name = "Notandanafn")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Lykilorð")]
        public string Password { get; set; }

        [Display(Name = "Muna mig?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        /*
            Hér þurfum við að bæta við t.d. Email
            -Natan
         */
        [Required]
        [Display(Name = "Notandanafn")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} verður að vera a.m.k {2} stafa langt.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Lykilorð")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Staðfestu lykilorðið")]
        [Compare("Password", ErrorMessage = "Lykilorðin stemma ekki")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Veffang")]
        public string Email { get; set; }

        [Display(Name = "Mynd (Valkvætt)")]
        public string Avatar { get; set; }

        public DateTime DateCreated { get; set; }

        public bool IsAdmin { get; set; }

        [Display(Name = "Stutt lýsing, max 200 stafir (Valkvætt)")]
        [StringLength(200, ErrorMessage = "Styttu textan niður í max 200 stafi")]
        public string About { get; set; }
    }

    public class ExternalLogin
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }
}
