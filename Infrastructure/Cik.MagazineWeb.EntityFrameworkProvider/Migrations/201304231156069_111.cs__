namespace Cik.MagazineWeb.EntityFrameworkProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _111 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Categories", "ModifiedDate", c => c.DateTime());
            AlterColumn("dbo.Items", "ModifiedDate", c => c.DateTime());
            AlterColumn("dbo.ItemContents", "ModifiedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ItemContents", "ModifiedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Items", "ModifiedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Categories", "ModifiedDate", c => c.DateTime(nullable: false));
        }
    }
}
