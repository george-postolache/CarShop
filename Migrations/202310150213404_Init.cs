namespace CarShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        CarId = c.Int(nullable: false, identity: true),
                        VIN = c.String(nullable: false, maxLength: 17),
                        Make = c.String(nullable: false, maxLength: 50),
                        Model = c.String(nullable: false, maxLength: 50),
                        Color = c.String(maxLength: 50),
                        Year = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ClientId = c.Int(),
                    })
                .PrimaryKey(t => t.CarId)
                .ForeignKey("dbo.Clients", t => t.ClientId)
                .Index(t => t.VIN, unique: true)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ClientId = c.Int(nullable: false, identity: true),
                        PIN = c.String(nullable: false, maxLength: 13),
                        FirstName = c.String(nullable: false, maxLength: 150),
                        LastName = c.String(nullable: false, maxLength: 150),
                        PhoneNumber = c.String(nullable: false, maxLength: 10),
                        Address = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.ClientId)
                .Index(t => t.PIN, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cars", "ClientId", "dbo.Clients");
            DropIndex("dbo.Clients", new[] { "PIN" });
            DropIndex("dbo.Cars", new[] { "ClientId" });
            DropIndex("dbo.Cars", new[] { "VIN" });
            DropTable("dbo.Clients");
            DropTable("dbo.Cars");
        }
    }
}
