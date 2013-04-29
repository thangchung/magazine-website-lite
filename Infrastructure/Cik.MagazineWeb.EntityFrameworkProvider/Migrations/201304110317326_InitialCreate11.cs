namespace Cik.MagazineWeb.EntityFrameworkProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "CreatedBy", c => c.String(nullable: false));
            AddColumn("dbo.Items", "ModifiedBy", c => c.String());
            AddColumn("dbo.Items", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Items", "ModifiedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.ItemContents", "CreatedBy", c => c.String(nullable: false));
            AddColumn("dbo.ItemContents", "ModifiedBy", c => c.String());
            AddColumn("dbo.ItemContents", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.ItemContents", "ModifiedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ItemContents", "ModifiedDate");
            DropColumn("dbo.ItemContents", "CreatedDate");
            DropColumn("dbo.ItemContents", "ModifiedBy");
            DropColumn("dbo.ItemContents", "CreatedBy");
            DropColumn("dbo.Items", "ModifiedDate");
            DropColumn("dbo.Items", "CreatedDate");
            DropColumn("dbo.Items", "ModifiedBy");
            DropColumn("dbo.Items", "CreatedBy");
        }
    }
}
