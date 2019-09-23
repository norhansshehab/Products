namespace api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CatID = c.Int(nullable: false, identity: true),
                        CatName = c.String(),
                        ParentID = c.Int(),
                    })
                .PrimaryKey(t => t.CatID)
                .ForeignKey("dbo.Categories", t => t.ParentID)
                .Index(t => t.ParentID);
            
            CreateTable(
                "dbo.CategoryFeatures",
                c => new
                    {
                        CatID = c.Int(nullable: false),
                        FeatureID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CatID, t.FeatureID })
                .ForeignKey("dbo.Categories", t => t.CatID, cascadeDelete: true)
                .ForeignKey("dbo.Features", t => t.FeatureID, cascadeDelete: true)
                .Index(t => t.CatID)
                .Index(t => t.FeatureID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProdID = c.Int(nullable: false, identity: true),
                        ProdName = c.String(),
                        CatID = c.Int(),
                    })
                .PrimaryKey(t => t.ProdID)
                .ForeignKey("dbo.Categories", t => t.CatID)
                .Index(t => t.CatID);
            
            CreateTable(
                "dbo.ProductFeatureValues",
                c => new
                    {
                        ProdID = c.Int(nullable: false),
                        FeatureID = c.Int(nullable: false),
                        Value = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ProdID, t.FeatureID, t.Value })
                .ForeignKey("dbo.Products", t => t.ProdID, cascadeDelete: true)
                .ForeignKey("dbo.Features", t => t.FeatureID, cascadeDelete: true)
                .Index(t => t.ProdID)
                .Index(t => t.FeatureID);
            
            CreateTable(
                "dbo.Features",
                c => new
                    {
                        FeatureID = c.Int(nullable: false, identity: true),
                        FeatureName = c.String(),
                    })
                .PrimaryKey(t => t.FeatureID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductFeatureValues", "FeatureID", "dbo.Features");
            DropForeignKey("dbo.CategoryFeatures", "FeatureID", "dbo.Features");
            DropForeignKey("dbo.Products", "CatID", "dbo.Categories");
            DropForeignKey("dbo.ProductFeatureValues", "ProdID", "dbo.Products");
            DropForeignKey("dbo.Categories", "ParentID", "dbo.Categories");
            DropForeignKey("dbo.CategoryFeatures", "CatID", "dbo.Categories");
            DropIndex("dbo.ProductFeatureValues", new[] { "FeatureID" });
            DropIndex("dbo.ProductFeatureValues", new[] { "ProdID" });
            DropIndex("dbo.Products", new[] { "CatID" });
            DropIndex("dbo.CategoryFeatures", new[] { "FeatureID" });
            DropIndex("dbo.CategoryFeatures", new[] { "CatID" });
            DropIndex("dbo.Categories", new[] { "ParentID" });
            DropTable("dbo.Features");
            DropTable("dbo.ProductFeatureValues");
            DropTable("dbo.Products");
            DropTable("dbo.CategoryFeatures");
            DropTable("dbo.Categories");
        }
    }
}
