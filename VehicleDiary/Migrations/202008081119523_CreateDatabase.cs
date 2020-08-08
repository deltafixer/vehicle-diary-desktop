namespace VehicleDiary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PersonUser",
                c => new
                    {
                        Username = c.String(nullable: false, maxLength: 30),
                        FirstName = c.String(nullable: false, maxLength: 20),
                        LastName = c.String(nullable: false, maxLength: 20),
                        Password = c.String(nullable: false, maxLength: 50),
                        UserType = c.Int(nullable: false),
                        Role = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Username);
            
            CreateTable(
                "dbo.Vehicle",
                c => new
                    {
                        Vin = c.String(nullable: false, maxLength: 128),
                        Make = c.Int(nullable: false),
                        Model = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Vin);
            
            CreateTable(
                "dbo.VehicleAccident",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateOfAccident = c.DateTime(nullable: false),
                        DamagePriceEvaluation = c.Single(nullable: false),
                        Vehicle_Vin = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Vehicle", t => t.Vehicle_Vin, cascadeDelete: true)
                .Index(t => t.Vehicle_Vin);
            
            CreateTable(
                "dbo.SaleListing",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Single(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        Vehicle_Vin = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Vehicle", t => t.Vehicle_Vin)
                .Index(t => t.Vehicle_Vin);
            
            CreateTable(
                "dbo.VehicleService",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateTaken = c.DateTime(nullable: false),
                        Price = c.Single(nullable: false),
                        ServiceDetails = c.String(),
                        ServicedBy_Username = c.String(nullable: false, maxLength: 30),
                        Vehicle_Vin = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ServiceUser", t => t.ServicedBy_Username, cascadeDelete: true)
                .ForeignKey("dbo.Vehicle", t => t.Vehicle_Vin, cascadeDelete: true)
                .Index(t => t.ServicedBy_Username)
                .Index(t => t.Vehicle_Vin);
            
            CreateTable(
                "dbo.ServiceUser",
                c => new
                    {
                        Username = c.String(nullable: false, maxLength: 30),
                        Name = c.String(nullable: false),
                        ServiceType = c.Int(nullable: false),
                        Password = c.String(nullable: false, maxLength: 50),
                        UserType = c.Int(nullable: false),
                        Role = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Username);
            
            CreateTable(
                "dbo.VehicleSpecification",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Single(nullable: false),
                        Condition = c.Int(nullable: false),
                        MakeYear = c.DateTime(nullable: false),
                        BodyStyle = c.Int(nullable: false),
                        DriveType = c.Int(nullable: false),
                        Kilometrage = c.Single(nullable: false),
                        FuelType = c.Int(nullable: false),
                        EngineVolume = c.Int(nullable: false),
                        EnginePowerKW = c.Int(nullable: false),
                        EngineEmissionType = c.Int(nullable: false),
                        GearboxType = c.Int(nullable: false),
                        Vehicle_Vin = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Vehicle", t => t.Vehicle_Vin)
                .Index(t => t.Vehicle_Vin);
            
            CreateTable(
                "dbo.VehicleModelPersonUserModels",
                c => new
                    {
                        VehicleModel_Vin = c.String(nullable: false, maxLength: 128),
                        PersonUserModel_Username = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => new { t.VehicleModel_Vin, t.PersonUserModel_Username })
                .ForeignKey("dbo.Vehicle", t => t.VehicleModel_Vin, cascadeDelete: true)
                .ForeignKey("dbo.PersonUser", t => t.PersonUserModel_Username, cascadeDelete: true)
                .Index(t => t.VehicleModel_Vin)
                .Index(t => t.PersonUserModel_Username);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VehicleSpecification", "Vehicle_Vin", "dbo.Vehicle");
            DropForeignKey("dbo.VehicleService", "Vehicle_Vin", "dbo.Vehicle");
            DropForeignKey("dbo.VehicleService", "ServicedBy_Username", "dbo.ServiceUser");
            DropForeignKey("dbo.SaleListing", "Vehicle_Vin", "dbo.Vehicle");
            DropForeignKey("dbo.VehicleModelPersonUserModels", "PersonUserModel_Username", "dbo.PersonUser");
            DropForeignKey("dbo.VehicleModelPersonUserModels", "VehicleModel_Vin", "dbo.Vehicle");
            DropForeignKey("dbo.VehicleAccident", "Vehicle_Vin", "dbo.Vehicle");
            DropIndex("dbo.VehicleModelPersonUserModels", new[] { "PersonUserModel_Username" });
            DropIndex("dbo.VehicleModelPersonUserModels", new[] { "VehicleModel_Vin" });
            DropIndex("dbo.VehicleSpecification", new[] { "Vehicle_Vin" });
            DropIndex("dbo.VehicleService", new[] { "Vehicle_Vin" });
            DropIndex("dbo.VehicleService", new[] { "ServicedBy_Username" });
            DropIndex("dbo.SaleListing", new[] { "Vehicle_Vin" });
            DropIndex("dbo.VehicleAccident", new[] { "Vehicle_Vin" });
            DropTable("dbo.VehicleModelPersonUserModels");
            DropTable("dbo.VehicleSpecification");
            DropTable("dbo.ServiceUser");
            DropTable("dbo.VehicleService");
            DropTable("dbo.SaleListing");
            DropTable("dbo.VehicleAccident");
            DropTable("dbo.Vehicle");
            DropTable("dbo.PersonUser");
        }
    }
}
