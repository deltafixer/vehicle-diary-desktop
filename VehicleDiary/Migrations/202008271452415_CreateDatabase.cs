﻿namespace VehicleDiary.Migrations
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
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 20),
                        LastName = c.String(nullable: false, maxLength: 20),
                        PersonType = c.Int(nullable: false),
                        User_Username = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserTable", t => t.User_Username, cascadeDelete: true)
                .Index(t => t.User_Username);
            
            CreateTable(
                "dbo.UserTable",
                c => new
                    {
                        Username = c.String(nullable: false, maxLength: 30),
                        Password = c.String(nullable: false),
                        Role = c.Int(nullable: false),
                        UserType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Username);
            
            CreateTable(
                "dbo.PersonUserVehicle",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Vin = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Id, t.Vin })
                .ForeignKey("dbo.PersonUser", t => t.Id, cascadeDelete: true)
                .ForeignKey("dbo.Vehicle", t => t.Vin, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.Vin);
            
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
                        Condition = c.Int(nullable: false),
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
                        ServicedBy_Id = c.Int(nullable: false),
                        Vehicle_Vin = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ServiceUser", t => t.ServicedBy_Id, cascadeDelete: true)
                .ForeignKey("dbo.Vehicle", t => t.Vehicle_Vin, cascadeDelete: true)
                .Index(t => t.ServicedBy_Id)
                .Index(t => t.Vehicle_Vin);
            
            CreateTable(
                "dbo.ServiceUser",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ServiceType = c.Int(nullable: false),
                        User_Username = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserTable", t => t.User_Username, cascadeDelete: true)
                .Index(t => t.User_Username);
            
            CreateTable(
                "dbo.VehicleSpecification",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MakeDate = c.DateTime(nullable: false),
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PersonUserVehicle", "Vin", "dbo.Vehicle");
            DropForeignKey("dbo.VehicleSpecification", "Vehicle_Vin", "dbo.Vehicle");
            DropForeignKey("dbo.VehicleService", "Vehicle_Vin", "dbo.Vehicle");
            DropForeignKey("dbo.VehicleService", "ServicedBy_Id", "dbo.ServiceUser");
            DropForeignKey("dbo.ServiceUser", "User_Username", "dbo.UserTable");
            DropForeignKey("dbo.SaleListing", "Vehicle_Vin", "dbo.Vehicle");
            DropForeignKey("dbo.VehicleAccident", "Vehicle_Vin", "dbo.Vehicle");
            DropForeignKey("dbo.PersonUserVehicle", "Id", "dbo.PersonUser");
            DropForeignKey("dbo.PersonUser", "User_Username", "dbo.UserTable");
            DropIndex("dbo.VehicleSpecification", new[] { "Vehicle_Vin" });
            DropIndex("dbo.ServiceUser", new[] { "User_Username" });
            DropIndex("dbo.VehicleService", new[] { "Vehicle_Vin" });
            DropIndex("dbo.VehicleService", new[] { "ServicedBy_Id" });
            DropIndex("dbo.SaleListing", new[] { "Vehicle_Vin" });
            DropIndex("dbo.VehicleAccident", new[] { "Vehicle_Vin" });
            DropIndex("dbo.PersonUserVehicle", new[] { "Vin" });
            DropIndex("dbo.PersonUserVehicle", new[] { "Id" });
            DropIndex("dbo.PersonUser", new[] { "User_Username" });
            DropTable("dbo.VehicleSpecification");
            DropTable("dbo.ServiceUser");
            DropTable("dbo.VehicleService");
            DropTable("dbo.SaleListing");
            DropTable("dbo.VehicleAccident");
            DropTable("dbo.Vehicle");
            DropTable("dbo.PersonUserVehicle");
            DropTable("dbo.UserTable");
            DropTable("dbo.PersonUser");
        }
    }
}
