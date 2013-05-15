namespace Cik.MagazineWeb.EntityFrameworkProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveSomeRequireOnUser : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "DisplayName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "DisplayName", c => c.String(nullable: false));
        }
    }
}
