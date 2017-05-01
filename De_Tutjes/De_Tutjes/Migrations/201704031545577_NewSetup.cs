namespace De_Tutjes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewSetup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        AddressId = c.Int(nullable: false, identity: true),
                        Street = c.String(),
                        Number = c.Int(nullable: false),
                        Bus = c.String(),
                        City = c.String(),
                        PostalCode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AddressId);
            
            CreateTable(
                "dbo.Persons",
                c => new
                    {
                        PersonId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Gender = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                        RegistrationDate = c.DateTime(nullable: false),
                        Active = c.Boolean(nullable: false),
                        AddressId = c.Int(),
                        ContactDetailId = c.Int(),
                        UserAccountId = c.String(),
                    })
                .PrimaryKey(t => t.PersonId)
                .ForeignKey("dbo.Addresses", t => t.AddressId)
                .Index(t => t.AddressId);
            
            CreateTable(
                "dbo.ContactDetails",
                c => new
                    {
                        ContactDetailId = c.Int(nullable: false),
                        HomePhone = c.Int(),
                        CellPhone = c.Int(nullable: false),
                        WorkPhone = c.Int(),
                        Email = c.String(),
                        PersonId = c.Int(),
                    })
                .PrimaryKey(t => t.ContactDetailId)
                .ForeignKey("dbo.Persons", t => t.ContactDetailId)
                .Index(t => t.ContactDetailId);
            
            CreateTable(
                "dbo.RelationLinks",
                c => new
                    {
                        RelationLinkID = c.Int(nullable: false, identity: true),
                        RelationToChild = c.String(),
                        PersonId = c.Int(nullable: false),
                        ToddlerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RelationLinkID)
                .ForeignKey("dbo.Persons", t => t.PersonId)
                .ForeignKey("dbo.Toddlers", t => t.ToddlerId)
                .Index(t => t.PersonId)
                .Index(t => t.ToddlerId);
            
            CreateTable(
                "dbo.Toddlers",
                c => new
                    {
                        ToddlerId = c.Int(nullable: false, identity: true),
                        ToddlerSession = c.String(),
                        DailyRoutine = c.String(),
                        ImportantNotice = c.String(),
                        FoodId = c.Int(),
                        MedicalId = c.Int(),
                        SleepId = c.Int(),
                        PersonId = c.Int(nullable: false),
                        Doctor_DoctorId = c.Int(),
                    })
                .PrimaryKey(t => t.ToddlerId)
                .ForeignKey("dbo.Eating", t => t.FoodId)
                .ForeignKey("dbo.Doctors", t => t.Doctor_DoctorId)
                .ForeignKey("dbo.MedicalInfo", t => t.MedicalId)
                .ForeignKey("dbo.Persons", t => t.PersonId)
                .ForeignKey("dbo.Sleeping", t => t.SleepId)
                .Index(t => t.FoodId)
                .Index(t => t.MedicalId)
                .Index(t => t.SleepId)
                .Index(t => t.PersonId)
                .Index(t => t.Doctor_DoctorId);
            
            CreateTable(
                "dbo.AgreedDays",
                c => new
                    {
                        AgreedDaysId = c.Int(nullable: false, identity: true),
                        Monday = c.Boolean(),
                        Tuesday = c.Boolean(),
                        Wednesday = c.Boolean(),
                        Thursday = c.Boolean(),
                        Friday = c.Boolean(),
                        SpecialNotice = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        ToddlerId = c.Int(nullable: false),
                        LocationId = c.Int(),
                    })
                .PrimaryKey(t => t.AgreedDaysId)
                .ForeignKey("dbo.Locations", t => t.LocationId)
                .ForeignKey("dbo.Toddlers", t => t.ToddlerId)
                .Index(t => t.ToddlerId)
                .Index(t => t.LocationId);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        LocationId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ManagerId = c.Int(nullable: false),
                        AddressId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LocationId)
                .ForeignKey("dbo.Addresses", t => t.AddressId)
                .ForeignKey("dbo.Employees", t => t.ManagerId)
                .Index(t => t.ManagerId)
                .Index(t => t.AddressId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeID = c.Int(nullable: false, identity: true),
                        Role = c.Int(nullable: false),
                        PersonId = c.Int(nullable: false),
                        LocationId = c.Int(nullable: false),
                        Location_LocationId = c.Int(),
                    })
                .PrimaryKey(t => t.EmployeeID)
                .ForeignKey("dbo.Locations", t => t.LocationId)
                .ForeignKey("dbo.Persons", t => t.PersonId)
                .ForeignKey("dbo.Locations", t => t.Location_LocationId)
                .Index(t => t.PersonId)
                .Index(t => t.LocationId)
                .Index(t => t.Location_LocationId);
            
            CreateTable(
                "dbo.Eating",
                c => new
                    {
                        FoodID = c.Int(nullable: false, identity: true),
                        SpecialDiet = c.String(),
                        Allergies = c.String(),
                        MayNotEat = c.String(),
                        BottlePowder = c.String(),
                        BottleDay = c.Int(nullable: false),
                        SpecialNotice = c.String(),
                    })
                .PrimaryKey(t => t.FoodID);
            
            CreateTable(
                "dbo.MedicalInfo",
                c => new
                    {
                        MedicalID = c.Int(nullable: false, identity: true),
                        HealthServiceNumber = c.String(),
                        Medication = c.Boolean(nullable: false),
                        MedicationName = c.String(),
                        Allergies = c.String(),
                        AllergiesMedication = c.String(),
                        ChildDisease = c.String(),
                        DiseaseWhen = c.String(),
                        SpecialNotice = c.String(),
                        Doctor_DoctorId = c.Int(),
                    })
                .PrimaryKey(t => t.MedicalID)
                .ForeignKey("dbo.Doctors", t => t.Doctor_DoctorId)
                .Index(t => t.Doctor_DoctorId);
            
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        DoctorId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        PersonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DoctorId)
                .ForeignKey("dbo.Persons", t => t.PersonId)
                .Index(t => t.PersonId);
            
            CreateTable(
                "dbo.Sleeping",
                c => new
                    {
                        SleepID = c.Int(nullable: false, identity: true),
                        SleepingPosition = c.String(),
                        HasToy = c.Boolean(nullable: false),
                        Toy = c.String(),
                        HasSoother = c.Boolean(nullable: false),
                        Soother = c.String(),
                        SpecialNotice = c.String(),
                    })
                .PrimaryKey(t => t.SleepID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Person_PersonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Persons", t => t.Person_PersonId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.Person_PersonId);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.NewChildWizardSessions",
                c => new
                    {
                        SessionId = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        ToddlerSession = c.String(),
                        Start = c.DateTime(nullable: false),
                        Stop = c.DateTime(),
                        Complete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.SessionId);
            
            CreateTable(
                "dbo.Parents",
                c => new
                    {
                        ParentId = c.Int(nullable: false, identity: true),
                        Relation = c.String(),
                        PersonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ParentId)
                .ForeignKey("dbo.Persons", t => t.PersonId)
                .Index(t => t.PersonId);
            
            CreateTable(
                "dbo.Pickups",
                c => new
                    {
                        PickupId = c.Int(nullable: false, identity: true),
                        Relation = c.String(),
                        PersonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PickupId)
                .ForeignKey("dbo.Persons", t => t.PersonId)
                .Index(t => t.PersonId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.VacationDays",
                c => new
                    {
                        VacationDayId = c.Int(nullable: false, identity: true),
                        Day = c.DateTime(nullable: false),
                        ToddlerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VacationDayId)
                .ForeignKey("dbo.Toddlers", t => t.ToddlerId)
                .Index(t => t.ToddlerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VacationDays", "ToddlerId", "dbo.Toddlers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Pickups", "PersonId", "dbo.Persons");
            DropForeignKey("dbo.Parents", "PersonId", "dbo.Persons");
            DropForeignKey("dbo.AspNetUsers", "Person_PersonId", "dbo.Persons");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.RelationLinks", "ToddlerId", "dbo.Toddlers");
            DropForeignKey("dbo.Toddlers", "SleepId", "dbo.Sleeping");
            DropForeignKey("dbo.Toddlers", "PersonId", "dbo.Persons");
            DropForeignKey("dbo.Toddlers", "MedicalId", "dbo.MedicalInfo");
            DropForeignKey("dbo.MedicalInfo", "Doctor_DoctorId", "dbo.Doctors");
            DropForeignKey("dbo.Toddlers", "Doctor_DoctorId", "dbo.Doctors");
            DropForeignKey("dbo.Doctors", "PersonId", "dbo.Persons");
            DropForeignKey("dbo.Toddlers", "FoodId", "dbo.Eating");
            DropForeignKey("dbo.AgreedDays", "ToddlerId", "dbo.Toddlers");
            DropForeignKey("dbo.AgreedDays", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.Locations", "ManagerId", "dbo.Employees");
            DropForeignKey("dbo.Employees", "Location_LocationId", "dbo.Locations");
            DropForeignKey("dbo.Employees", "PersonId", "dbo.Persons");
            DropForeignKey("dbo.Employees", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.Locations", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.RelationLinks", "PersonId", "dbo.Persons");
            DropForeignKey("dbo.ContactDetails", "ContactDetailId", "dbo.Persons");
            DropForeignKey("dbo.Persons", "AddressId", "dbo.Addresses");
            DropIndex("dbo.VacationDays", new[] { "ToddlerId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Pickups", new[] { "PersonId" });
            DropIndex("dbo.Parents", new[] { "PersonId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "Person_PersonId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Doctors", new[] { "PersonId" });
            DropIndex("dbo.MedicalInfo", new[] { "Doctor_DoctorId" });
            DropIndex("dbo.Employees", new[] { "Location_LocationId" });
            DropIndex("dbo.Employees", new[] { "LocationId" });
            DropIndex("dbo.Employees", new[] { "PersonId" });
            DropIndex("dbo.Locations", new[] { "AddressId" });
            DropIndex("dbo.Locations", new[] { "ManagerId" });
            DropIndex("dbo.AgreedDays", new[] { "LocationId" });
            DropIndex("dbo.AgreedDays", new[] { "ToddlerId" });
            DropIndex("dbo.Toddlers", new[] { "Doctor_DoctorId" });
            DropIndex("dbo.Toddlers", new[] { "PersonId" });
            DropIndex("dbo.Toddlers", new[] { "SleepId" });
            DropIndex("dbo.Toddlers", new[] { "MedicalId" });
            DropIndex("dbo.Toddlers", new[] { "FoodId" });
            DropIndex("dbo.RelationLinks", new[] { "ToddlerId" });
            DropIndex("dbo.RelationLinks", new[] { "PersonId" });
            DropIndex("dbo.ContactDetails", new[] { "ContactDetailId" });
            DropIndex("dbo.Persons", new[] { "AddressId" });
            DropTable("dbo.VacationDays");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Pickups");
            DropTable("dbo.Parents");
            DropTable("dbo.NewChildWizardSessions");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Sleeping");
            DropTable("dbo.Doctors");
            DropTable("dbo.MedicalInfo");
            DropTable("dbo.Eating");
            DropTable("dbo.Employees");
            DropTable("dbo.Locations");
            DropTable("dbo.AgreedDays");
            DropTable("dbo.Toddlers");
            DropTable("dbo.RelationLinks");
            DropTable("dbo.ContactDetails");
            DropTable("dbo.Persons");
            DropTable("dbo.Addresses");
        }
    }
}
