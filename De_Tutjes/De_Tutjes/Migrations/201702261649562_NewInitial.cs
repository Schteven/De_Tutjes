namespace De_Tutjes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewInitial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Address",
                c => new
                    {
                        AddressId = c.Int(nullable: false, identity: true),
                        Street = c.String(),
                        Number = c.Int(nullable: false),
                        Bus = c.String(),
                        City = c.String(),
                        PostalCode = c.String(),
                    })
                .PrimaryKey(t => t.AddressId);
            
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
                        Address_AddressId = c.Int(),
                    })
                .PrimaryKey(t => t.PersonId)
                .ForeignKey("dbo.Address", t => t.Address_AddressId)
                .Index(t => t.Address_AddressId);
            
            CreateTable(
                "dbo.RelationLinks",
                c => new
                    {
                        RelationLinkID = c.Int(nullable: false, identity: true),
                        RelationToChild = c.String(),
                        Toddler_ToddlerId = c.Int(),
                    })
                .PrimaryKey(t => t.RelationLinkID)
                .ForeignKey("dbo.Toddlers", t => t.Toddler_ToddlerId)
                .Index(t => t.Toddler_ToddlerId);
            
            CreateTable(
                "dbo.Toddlers",
                c => new
                    {
                        ToddlerId = c.Int(nullable: false, identity: true),
                        DailyRoutine = c.String(),
                        ImportantNotice = c.String(),
                        Food_FoodID = c.Int(),
                        Doctor_DoctorId = c.Int(),
                        Medical_MedicalID = c.Int(),
                        Person_PersonId = c.Int(),
                        Sleep_SleepID = c.Int(),
                    })
                .PrimaryKey(t => t.ToddlerId)
                .ForeignKey("dbo.Eating", t => t.Food_FoodID)
                .ForeignKey("dbo.Doctors", t => t.Doctor_DoctorId)
                .ForeignKey("dbo.MedicalInfo", t => t.Medical_MedicalID)
                .ForeignKey("dbo.Persons", t => t.Person_PersonId)
                .ForeignKey("dbo.Sleeping", t => t.Sleep_SleepID)
                .Index(t => t.Food_FoodID)
                .Index(t => t.Doctor_DoctorId)
                .Index(t => t.Medical_MedicalID)
                .Index(t => t.Person_PersonId)
                .Index(t => t.Sleep_SleepID);
            
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
                        Title = c.Int(nullable: false),
                        Person_PersonId = c.Int(),
                    })
                .PrimaryKey(t => t.DoctorId)
                .ForeignKey("dbo.Persons", t => t.Person_PersonId)
                .Index(t => t.Person_PersonId);
            
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
                "dbo.ContactDetail",
                c => new
                    {
                        ContactDetailsId = c.Int(nullable: false, identity: true),
                        HomePhone = c.Int(nullable: false),
                        CellPhone = c.Int(nullable: false),
                        WorkPhone = c.Int(nullable: false),
                        Email = c.String(),
                        Person_PersonId = c.Int(),
                    })
                .PrimaryKey(t => t.ContactDetailsId)
                .ForeignKey("dbo.Persons", t => t.Person_PersonId)
                .Index(t => t.Person_PersonId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeID = c.Int(nullable: false, identity: true),
                        LocationID = c.Int(nullable: false),
                        Role = c.Int(nullable: false),
                        Person_PersonId = c.Int(),
                    })
                .PrimaryKey(t => t.EmployeeID)
                .ForeignKey("dbo.Persons", t => t.Person_PersonId)
                .Index(t => t.Person_PersonId);
            
            CreateTable(
                "dbo.Pickups",
                c => new
                    {
                        PickupId = c.Int(nullable: false, identity: true),
                        Relation = c.String(),
                        Person_PersonId = c.Int(),
                    })
                .PrimaryKey(t => t.PickupId)
                .ForeignKey("dbo.Persons", t => t.Person_PersonId)
                .Index(t => t.Person_PersonId);
            
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
                "dbo.RelationLinkPerson",
                c => new
                    {
                        RelationLink_RelationLinkID = c.Int(nullable: false),
                        Person_PersonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RelationLink_RelationLinkID, t.Person_PersonId })
                .ForeignKey("dbo.RelationLinks", t => t.RelationLink_RelationLinkID, cascadeDelete: true)
                .ForeignKey("dbo.Persons", t => t.Person_PersonId, cascadeDelete: true)
                .Index(t => t.RelationLink_RelationLinkID)
                .Index(t => t.Person_PersonId);
            
            CreateTable(
                "dbo.Parents",
                c => new
                    {
                        PersonId = c.Int(nullable: false),
                        Person_PersonId = c.Int(),
                        ParentID = c.Int(nullable: false),
                        Relation = c.String(),
                    })
                .PrimaryKey(t => t.PersonId)
                .ForeignKey("dbo.Persons", t => t.PersonId)
                .ForeignKey("dbo.Persons", t => t.Person_PersonId)
                .Index(t => t.PersonId)
                .Index(t => t.Person_PersonId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Parents", "Person_PersonId", "dbo.Persons");
            DropForeignKey("dbo.Parents", "PersonId", "dbo.Persons");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Pickups", "Person_PersonId", "dbo.Persons");
            DropForeignKey("dbo.Employees", "Person_PersonId", "dbo.Persons");
            DropForeignKey("dbo.ContactDetail", "Person_PersonId", "dbo.Persons");
            DropForeignKey("dbo.Persons", "Address_AddressId", "dbo.Address");
            DropForeignKey("dbo.AspNetUsers", "Person_PersonId", "dbo.Persons");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.RelationLinks", "Toddler_ToddlerId", "dbo.Toddlers");
            DropForeignKey("dbo.Toddlers", "Sleep_SleepID", "dbo.Sleeping");
            DropForeignKey("dbo.Toddlers", "Person_PersonId", "dbo.Persons");
            DropForeignKey("dbo.Toddlers", "Medical_MedicalID", "dbo.MedicalInfo");
            DropForeignKey("dbo.MedicalInfo", "Doctor_DoctorId", "dbo.Doctors");
            DropForeignKey("dbo.Toddlers", "Doctor_DoctorId", "dbo.Doctors");
            DropForeignKey("dbo.Doctors", "Person_PersonId", "dbo.Persons");
            DropForeignKey("dbo.Toddlers", "Food_FoodID", "dbo.Eating");
            DropForeignKey("dbo.RelationLinkPerson", "Person_PersonId", "dbo.Persons");
            DropForeignKey("dbo.RelationLinkPerson", "RelationLink_RelationLinkID", "dbo.RelationLinks");
            DropIndex("dbo.Parents", new[] { "Person_PersonId" });
            DropIndex("dbo.Parents", new[] { "PersonId" });
            DropIndex("dbo.RelationLinkPerson", new[] { "Person_PersonId" });
            DropIndex("dbo.RelationLinkPerson", new[] { "RelationLink_RelationLinkID" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Pickups", new[] { "Person_PersonId" });
            DropIndex("dbo.Employees", new[] { "Person_PersonId" });
            DropIndex("dbo.ContactDetail", new[] { "Person_PersonId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "Person_PersonId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Doctors", new[] { "Person_PersonId" });
            DropIndex("dbo.MedicalInfo", new[] { "Doctor_DoctorId" });
            DropIndex("dbo.Toddlers", new[] { "Sleep_SleepID" });
            DropIndex("dbo.Toddlers", new[] { "Person_PersonId" });
            DropIndex("dbo.Toddlers", new[] { "Medical_MedicalID" });
            DropIndex("dbo.Toddlers", new[] { "Doctor_DoctorId" });
            DropIndex("dbo.Toddlers", new[] { "Food_FoodID" });
            DropIndex("dbo.RelationLinks", new[] { "Toddler_ToddlerId" });
            DropIndex("dbo.Persons", new[] { "Address_AddressId" });
            DropTable("dbo.Parents");
            DropTable("dbo.RelationLinkPerson");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Pickups");
            DropTable("dbo.Employees");
            DropTable("dbo.ContactDetail");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Sleeping");
            DropTable("dbo.Doctors");
            DropTable("dbo.MedicalInfo");
            DropTable("dbo.Eating");
            DropTable("dbo.Toddlers");
            DropTable("dbo.RelationLinks");
            DropTable("dbo.Persons");
            DropTable("dbo.Address");
        }
    }
}
