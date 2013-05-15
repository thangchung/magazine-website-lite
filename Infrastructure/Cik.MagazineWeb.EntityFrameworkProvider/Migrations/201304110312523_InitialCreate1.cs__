namespace Cik.MagazineWeb.EntityFrameworkProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "CreatedBy", c => c.String(nullable: false));
            AddColumn("dbo.Categories", "ModifiedBy", c => c.String());
            AddColumn("dbo.Categories", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Categories", "ModifiedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "ModifiedDate");
            DropColumn("dbo.Categories", "CreatedDate");
            DropColumn("dbo.Categories", "ModifiedBy");
            DropColumn("dbo.Categories", "CreatedBy");
        }
    }
}
