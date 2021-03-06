﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace Leikjavefur.Models
{

    [Table("UserProfile")]
    public class UserProfile
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }

        [Required]
        [Display(Name = "Notendanafn")]
        public string UserName { get; set; }

        //[MembershipPassword]
        //[Display(Name = "Lykilorð")]
        //public string Password { get; set; }
        
        [Display(Name = "Póstfang")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Mynd (Valkvætt)")]
        public string Avatar { get; set; }

        public DateTime DateCreated { get; set; }

        [Display(Name = "Stutt lýsing, max 200 stafir (Valkvætt)")]
        [StringLength(200, ErrorMessage = "Styttu textan niður í max 200 stafi")]
        [DataType(DataType.MultilineText)]
        public string About { get; set; }


        public virtual ICollection<UserProfile> Friends { get; set; }
    }

    [Table("GameProfile")]
    public class Game
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int GameID { get; set; }
        
        [Required]
        [Display(Name = "Nafn")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Hámarksfjöldi spilara")]
        public int MaxPlayers { get; set; }

        [Required]
        [Display(Name = "Lágmarksfjöldi spilara")]
        public int MinPlayers { get; set; }

        [Display(Name = "Mynd")]
        public string Avatar { get; set; }

        public DateTime DateAdded { get; set; }

        [Display(Name = "Stutt lýsing, max 200 stafir (Valkvætt)")]
        [StringLength(200, ErrorMessage = "Styttu textan niður í max 200 stafi")]
        [DataType(DataType.MultilineText)]
        public string About { get; set; }

        [Display(Name = "Reglur")]
        [StringLength(500, ErrorMessage = "Styttu textan niður í max 500 stafi")]
        [DataType(DataType.MultilineText)]
        public string Rules { get; set; }
    }

    [Table("Statistic")]
    public class Statistic
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int UserID { get; set; }
        //[ForeignKey("UserID")]
        //public virtual UserProfile UserProfile { get; set; }

        public int GameID { get; set; }
        //[ForeignKey("GameID")]
        //public virtual Game Game { get; set; }

        public int GamesPlayed { get; set; }

        public int Wins { get; set; }

        public int Losses { get; set; }

        public int Draws { get; set; }

        public int Points { get; set; }
    }

    [Table("Report")]
    public class Report
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ReportId { get; set; }

        public int UserID { get; set; }
        //[ForeignKey("UserID")]
        //public virtual UserProfile UserProfile { get; set; }

        public int ReporterID { get; set; }
        //[ForeignKey("UserID")]
        //public virtual UserProfile Reporter { get; set; }

        public string Details { get; set; }

        public DateTime DateCreated { get; set; }
    
    }

    [Table("GameInstance")]
    public class GameInstance
    {
        [Key, Column(Order = 0)]
        public string GameInstanceID { get; set; }

        public int GameID { get; set; }
        //[ForeignKey("GameID")]
        //public virtual Game Game { get; set; }

        [Key, Column(Order = 1)]
        public int UserID { get; set; }
        //[ForeignKey("UserID")]
        //public virtual UserProfile WinnerID { get; set; }

        public bool IsActive { get; set; }
    }

    [Table("Friends")]
    public class Friends
    {
        [Key, Column(Order = 0)]
        public int UserID { get; set; }

        [Key, Column(Order = 1)]
        public int FriendID { get; set; }

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

        /*[Display(Name = "Stutt lýsing, max 200 stafir (Valkvætt)")]
        [StringLength(200, ErrorMessage = "Styttu textan niður í max 200 stafi")]
        public string About { get; set; }*/
    }

}
