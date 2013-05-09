namespace Leikjavefur.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class friendstable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                 "Friends",
                 c => new
                 {
                     UserID = c.Int(),
                     FriendID = c.Int(),
                 })
//                 .ForeignKey("UserProfile", t => t.UserID, cascadeDelete: true)
//                 .ForeignKey("UserProfile", t => t.FriendID, cascadeDelete: true)
                 .Index(t => t.UserID);
        }
        
        public override void Down()
        {
        }
    }
}
