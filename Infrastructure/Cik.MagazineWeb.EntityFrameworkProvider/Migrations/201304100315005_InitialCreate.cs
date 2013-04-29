namespace Cik.MagazineWeb.EntityFrameworkProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ItemContentId = c.Int(nullable: false),
                        Category_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id)
                .ForeignKey("dbo.ItemContents", t => t.ItemContentId, cascadeDelete: true)
                .Index(t => t.Category_Id)
                .Index(t => t.ItemContentId);
            
            CreateTable(
                "dbo.ItemContents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        ShortDescription = c.String(nullable: false),
                        Content = c.String(nullable: false),
                        SmallImage = c.String(),
                        MediumImage = c.String(),
                        BigImage = c.String(),
                        NumOfView = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Items", new[] { "ItemContentId" });
            DropIndex("dbo.Items", new[] { "Category_Id" });
            DropForeignKey("dbo.Items", "ItemContentId", "dbo.ItemContents");
            DropForeignKey("dbo.Items", "Category_Id", "dbo.Categories");
            DropTable("dbo.ItemContents");
            DropTable("dbo.Items");
            DropTable("dbo.Categories");
        }
    }
}
