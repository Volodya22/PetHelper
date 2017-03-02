namespace DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UsersImages : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "ImageUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "ImageUrl");
        }
    }
}
