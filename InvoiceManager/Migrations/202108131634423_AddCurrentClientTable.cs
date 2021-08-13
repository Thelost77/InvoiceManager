namespace InvoiceManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCurrentClientTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CurrentClients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        AddressId = c.Int(nullable: false),
                        Email = c.String(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.AddressId, cascadeDelete: false)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: false)
                .Index(t => t.AddressId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CurrentClients", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CurrentClients", "AddressId", "dbo.Addresses");
            DropIndex("dbo.CurrentClients", new[] { "UserId" });
            DropIndex("dbo.CurrentClients", new[] { "AddressId" });
            DropTable("dbo.CurrentClients");
        }
    }
}
