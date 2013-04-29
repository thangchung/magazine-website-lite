namespace Cik.MagazineWeb.EntityFrameworkProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewSchemaForItemContent : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Items", "Category_Id", "dbo.Categories");
            DropIndex("dbo.Items", new[] { "Category_Id" });
            AlterColumn("dbo.Items", "Category_Id", c => c.Int(nullable: false));
            AddForeignKey("dbo.Items", "Category_Id", "dbo.Categories", "Id", cascadeDelete: true);
            CreateIndex("dbo.Items", "Category_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Items", new[] { "Category_Id" });
            DropForeignKey("dbo.Items", "Category_Id", "dbo.Categories");
            AlterColumn("dbo.Items", "Category_Id", c => c.Int());
            CreateIndex("dbo.Items", "Category_Id");
            AddForeignKey("dbo.Items", "Category_Id", "dbo.Categories", "Id");
        }
    }
}
