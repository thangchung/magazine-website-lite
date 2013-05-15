namespace Cik.MagazineWeb.EntityFrameworkProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUserApplication20130514 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        CreatedBy = c.String(nullable: false),
                        ModifiedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ItemContentId = c.Int(nullable: false),
                        CreatedBy = c.String(nullable: false),
                        ModifiedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        Category_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id, cascadeDelete: true)
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
                        CreatedBy = c.String(nullable: false),
                        ModifiedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        DisplayName = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Role = c.Int(nullable: false),
                        CreatedBy = c.String(nullable: false),
                        ModifiedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Items", new[] { "ItemContentId" });
            DropIndex("dbo.Items", new[] { "Category_Id" });
            DropForeignKey("dbo.Items", "ItemContentId", "dbo.ItemContents");
            DropForeignKey("dbo.Items", "Category_Id", "dbo.Categories");
            DropTable("dbo.Users");
            DropTable("dbo.ItemContents");
            DropTable("dbo.Items");
            DropTable("dbo.Categories");
        }
    }
}
