namespace Blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128, unicode: false),
                        Slug = c.String(nullable: false, maxLength: 128, unicode: false),
                        Content = c.String(nullable: false, unicode: false, storeType: "text"),
                        CreatedAt = c.DateTime(nullable: false, precision: 0),
                        UpdatedAt = c.DateTime(precision: 0),
                        DeletedAt = c.DateTime(precision: 0),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Slug = c.String(nullable: false, maxLength: 128, unicode: false),
                        Name = c.String(nullable: false, maxLength: 128, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 128, unicode: false),
                        Email = c.String(nullable: false, maxLength: 255, unicode: false),
                        PasswordHash = c.String(nullable: false, maxLength: 128, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TagPosts",
                c => new
                    {
                        TagId = c.Int(nullable: false),
                        PostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TagId, t.PostId })
                .ForeignKey("dbo.Posts", t => t.TagId, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.PostId, cascadeDelete: true)
                .Index(t => t.TagId)
                .Index(t => t.PostId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.TagPosts", "PostId", "dbo.Tags");
            DropForeignKey("dbo.TagPosts", "TagId", "dbo.Posts");
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.TagPosts", new[] { "PostId" });
            DropIndex("dbo.TagPosts", new[] { "TagId" });
            DropIndex("dbo.Posts", new[] { "User_Id" });
            DropTable("dbo.UserRoles");
            DropTable("dbo.TagPosts");
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.Tags");
            DropTable("dbo.Posts");
        }
    }
}
