namespace De_Tutjes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Persons",
                c => new
                    {
                        PersonId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Sex = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                        RegistrationDate = c.DateTime(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PersonId);
            
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        PersonId = c.Int(nullable: false),
                        DoctorId = c.Int(nullable: false),
                        Title = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PersonId)
                .ForeignKey("dbo.Persons", t => t.PersonId)
                .Index(t => t.PersonId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        PersonId = c.Int(nullable: false),
                        EmployeeID = c.Int(nullable: false),
                        LocationID = c.Int(nullable: false),
                        Role = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PersonId)
                .ForeignKey("dbo.Persons", t => t.PersonId)
                .Index(t => t.PersonId);
            
            CreateTable(
                "dbo.Parents",
                c => new
                    {
                        PersonId = c.Int(nullable: false),
                        ParentID = c.Int(nullable: false),
                        Relation = c.String(),
                    })
                .PrimaryKey(t => t.PersonId)
                .ForeignKey("dbo.Persons", t => t.PersonId)
                .Index(t => t.PersonId);
            
            CreateTable(
                "dbo.Pickups",
                c => new
                    {
                        PersonId = c.Int(nullable: false),
                        PickupId = c.Int(nullable: false),
                        Relation = c.String(),
                    })
                .PrimaryKey(t => t.PersonId)
                .ForeignKey("dbo.Persons", t => t.PersonId)
                .Index(t => t.PersonId);
            
            CreateTable(
                "dbo.Toddlers",
                c => new
                    {
                        PersonId = c.Int(nullable: false),
                        ToddlerId = c.Int(nullable: false),
                        FoodId = c.Int(nullable: false),
                        MedicalId = c.Int(nullable: false),
                        SleepID = c.Int(nullable: false),
                        ImportantNotice = c.String(),
                        DailyRoutine = c.String(),
                    })
                .PrimaryKey(t => t.PersonId)
                .ForeignKey("dbo.Persons", t => t.PersonId)
                .Index(t => t.PersonId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Toddlers", "PersonId", "dbo.Persons");
            DropForeignKey("dbo.Pickups", "PersonId", "dbo.Persons");
            DropForeignKey("dbo.Parents", "PersonId", "dbo.Persons");
            DropForeignKey("dbo.Employees", "PersonId", "dbo.Persons");
            DropForeignKey("dbo.Doctors", "PersonId", "dbo.Persons");
            DropIndex("dbo.Toddlers", new[] { "PersonId" });
            DropIndex("dbo.Pickups", new[] { "PersonId" });
            DropIndex("dbo.Parents", new[] { "PersonId" });
            DropIndex("dbo.Employees", new[] { "PersonId" });
            DropIndex("dbo.Doctors", new[] { "PersonId" });
            DropTable("dbo.Toddlers");
            DropTable("dbo.Pickups");
            DropTable("dbo.Parents");
            DropTable("dbo.Employees");
            DropTable("dbo.Doctors");
            DropTable("dbo.Persons");
        }
    }
}
