namespace De_Tutjes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.People",
                c => new
                    {
                        PersonId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Sex = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                        HomePhone = c.Int(nullable: false),
                        CellPhone = c.Int(nullable: false),
                        WorkPhone = c.Int(nullable: false),
                        Email = c.String(),
                        RegistrationDate = c.DateTime(nullable: false),
                        Active = c.Boolean(nullable: false),
                        DoctorId = c.Int(),
                        Title = c.Int(),
                        EmployeeID = c.Int(),
                        LocationID = c.Int(),
                        Role = c.Int(),
                        ParentID = c.Int(),
                        Relation = c.String(),
                        PickupId = c.Int(),
                        Relation1 = c.String(),
                        ToddlerId = c.Int(),
                        FoodId = c.Int(),
                        MedicalId = c.Int(),
                        SleepID = c.Int(),
                        ImportantNotice = c.String(),
                        DailyRoutine = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.PersonId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.People");
        }
    }
}
