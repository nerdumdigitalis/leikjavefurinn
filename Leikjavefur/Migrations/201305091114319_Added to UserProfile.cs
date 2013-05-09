namespace Leikjavefur.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedtoUserProfile : DbMigration
    {
        public override void Up()
        {
            AddColumn("UserProfile", "Friends", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("UserProfile", "Friends");
        }
    }
}
