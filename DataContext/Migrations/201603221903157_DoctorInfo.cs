namespace DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DoctorInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Specialization", c => c.String());
            AddColumn("dbo.Users", "Education", c => c.String());
            AddColumn("dbo.Users", "Trophies", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Trophies");
            DropColumn("dbo.Users", "Education");
            DropColumn("dbo.Users", "Specialization");
        }
    }
}
