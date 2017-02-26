namespace De_Tutjes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTables : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Food", newName: "Eating");
            RenameTable(name: "dbo.Medical", newName: "MedicalInfo");
            RenameTable(name: "dbo.RelationLink", newName: "RelationLinks");
            RenameTable(name: "dbo.Sleep", newName: "Sleeping");
            DropForeignKey("dbo.PersonApplicationUser", "Person_PersonId", "dbo.Persons");
            DropForeignKey("dbo.PersonApplicationUser", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Medical", "Doctor_PersonId", "dbo.Doctors");
            DropForeignKey("dbo.Toddlers", "Doctor_PersonId", "dbo.Doctors");
            DropIndex("dbo.PersonApplicationUser", new[] { "Person_PersonId" });
            DropIndex("dbo.PersonApplicationUser", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Doctors", new[] { "PersonId" });
            DropIndex("dbo.Toddlers", new[] { "PersonId" });
            DropIndex("dbo.Employees", new[] { "PersonId" });
            DropIndex("dbo.Pickups", new[] { "PersonId" });
            RenameColumn(table: "dbo.MedicalInfo", name: "Doctor_PersonId", newName: "Doctor_DoctorId");
            RenameColumn(table: "dbo.Toddlers", name: "Doctor_PersonId", newName: "Doctor_DoctorId");
            RenameColumn(table: "dbo.Doctors", name: "PersonId", newName: "Person_PersonId");
            RenameColumn(table: "dbo.Toddlers", name: "PersonId", newName: "Person_PersonId");
            RenameColumn(table: "dbo.Employees", name: "PersonId", newName: "Person_PersonId");
            RenameColumn(table: "dbo.Pickups", name: "PersonId", newName: "Person_PersonId");
            RenameIndex(table: "dbo.Toddlers", name: "IX_Doctor_PersonId", newName: "IX_Doctor_DoctorId");
            RenameIndex(table: "dbo.MedicalInfo", name: "IX_Doctor_PersonId", newName: "IX_Doctor_DoctorId");
            DropPrimaryKey("dbo.Doctors");
            DropPrimaryKey("dbo.Toddlers");
            DropPrimaryKey("dbo.Employees");
            DropPrimaryKey("dbo.Pickups");
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
            
            AddColumn("dbo.ContactDetail", "Person_PersonId", c => c.Int());
            AddColumn("dbo.Toddlers", "Food_FoodID", c => c.Int());
            AddColumn("dbo.Toddlers", "Medical_MedicalID", c => c.Int());
            AddColumn("dbo.Toddlers", "Sleep_SleepID", c => c.Int());
            AddColumn("dbo.Persons", "Address_AddressId", c => c.Int());
            AddColumn("dbo.Parents", "Person_PersonId", c => c.Int());
            AddColumn("dbo.AspNetUsers", "Person_PersonId", c => c.Int(nullable: false));
            AddColumn("dbo.RelationLinks", "RelationToChild", c => c.String());
            AddColumn("dbo.RelationLinks", "Toddler_ToddlerId", c => c.Int());
            AlterColumn("dbo.Doctors", "Person_PersonId", c => c.Int());
            AlterColumn("dbo.Doctors", "DoctorId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Toddlers", "Person_PersonId", c => c.Int());
            AlterColumn("dbo.Toddlers", "ToddlerId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Employees", "Person_PersonId", c => c.Int());
            AlterColumn("dbo.Employees", "EmployeeID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Pickups", "Person_PersonId", c => c.Int());
            AlterColumn("dbo.Pickups", "PickupId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Doctors", "DoctorId");
            AddPrimaryKey("dbo.Toddlers", "ToddlerId");
            AddPrimaryKey("dbo.Employees", "EmployeeID");
            AddPrimaryKey("dbo.Pickups", "PickupId");
            CreateIndex("dbo.Persons", "Address_AddressId");
            CreateIndex("dbo.RelationLinks", "Toddler_ToddlerId");
            CreateIndex("dbo.Toddlers", "Food_FoodID");
            CreateIndex("dbo.Toddlers", "Medical_MedicalID");
            CreateIndex("dbo.Toddlers", "Person_PersonId");
            CreateIndex("dbo.Toddlers", "Sleep_SleepID");
            CreateIndex("dbo.Doctors", "Person_PersonId");
            CreateIndex("dbo.AspNetUsers", "Person_PersonId");
            CreateIndex("dbo.ContactDetail", "Person_PersonId");
            CreateIndex("dbo.Employees", "Person_PersonId");
            CreateIndex("dbo.Pickups", "Person_PersonId");
            CreateIndex("dbo.Parents", "Person_PersonId");
            AddForeignKey("dbo.Toddlers", "Food_FoodID", "dbo.Eating", "FoodID");
            AddForeignKey("dbo.Toddlers", "Medical_MedicalID", "dbo.MedicalInfo", "MedicalID");
            AddForeignKey("dbo.Toddlers", "Sleep_SleepID", "dbo.Sleeping", "SleepID");
            AddForeignKey("dbo.RelationLinks", "Toddler_ToddlerId", "dbo.Toddlers", "ToddlerId");
            AddForeignKey("dbo.AspNetUsers", "Person_PersonId", "dbo.Persons", "PersonId");
            AddForeignKey("dbo.Persons", "Address_AddressId", "dbo.Address", "AddressId");
            AddForeignKey("dbo.ContactDetail", "Person_PersonId", "dbo.Persons", "PersonId");
            AddForeignKey("dbo.Parents", "Person_PersonId", "dbo.Persons", "PersonId");
            AddForeignKey("dbo.Toddlers", "Doctor_DoctorId", "dbo.Doctors", "DoctorId");
            AddForeignKey("dbo.MedicalInfo", "Doctor_DoctorId", "dbo.Doctors", "DoctorId");
            DropColumn("dbo.Toddlers", "FoodId");
            DropColumn("dbo.Toddlers", "MedicalId");
            DropColumn("dbo.Toddlers", "SleepID");
            DropColumn("dbo.RelationLinks", "ToddlerID");
            DropColumn("dbo.RelationLinks", "PersonID");
            DropTable("dbo.PersonApplicationUser");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PersonApplicationUser",
                c => new
                    {
                        Person_PersonId = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Person_PersonId, t.ApplicationUser_Id });
            
            AddColumn("dbo.RelationLinks", "PersonID", c => c.Int(nullable: false));
            AddColumn("dbo.RelationLinks", "ToddlerID", c => c.Int(nullable: false));
            AddColumn("dbo.Toddlers", "SleepID", c => c.Int(nullable: false));
            AddColumn("dbo.Toddlers", "MedicalId", c => c.Int(nullable: false));
            AddColumn("dbo.Toddlers", "FoodId", c => c.Int(nullable: false));
            DropForeignKey("dbo.MedicalInfo", "Doctor_DoctorId", "dbo.Doctors");
            DropForeignKey("dbo.Toddlers", "Doctor_DoctorId", "dbo.Doctors");
            DropForeignKey("dbo.Parents", "Person_PersonId", "dbo.Persons");
            DropForeignKey("dbo.ContactDetail", "Person_PersonId", "dbo.Persons");
            DropForeignKey("dbo.Persons", "Address_AddressId", "dbo.Address");
            DropForeignKey("dbo.AspNetUsers", "Person_PersonId", "dbo.Persons");
            DropForeignKey("dbo.RelationLinks", "Toddler_ToddlerId", "dbo.Toddlers");
            DropForeignKey("dbo.Toddlers", "Sleep_SleepID", "dbo.Sleeping");
            DropForeignKey("dbo.Toddlers", "Medical_MedicalID", "dbo.MedicalInfo");
            DropForeignKey("dbo.Toddlers", "Food_FoodID", "dbo.Eating");
            DropForeignKey("dbo.RelationLinkPerson", "Person_PersonId", "dbo.Persons");
            DropForeignKey("dbo.RelationLinkPerson", "RelationLink_RelationLinkID", "dbo.RelationLinks");
            DropIndex("dbo.Parents", new[] { "Person_PersonId" });
            DropIndex("dbo.RelationLinkPerson", new[] { "Person_PersonId" });
            DropIndex("dbo.RelationLinkPerson", new[] { "RelationLink_RelationLinkID" });
            DropIndex("dbo.Pickups", new[] { "Person_PersonId" });
            DropIndex("dbo.Employees", new[] { "Person_PersonId" });
            DropIndex("dbo.ContactDetail", new[] { "Person_PersonId" });
            DropIndex("dbo.AspNetUsers", new[] { "Person_PersonId" });
            DropIndex("dbo.Doctors", new[] { "Person_PersonId" });
            DropIndex("dbo.Toddlers", new[] { "Sleep_SleepID" });
            DropIndex("dbo.Toddlers", new[] { "Person_PersonId" });
            DropIndex("dbo.Toddlers", new[] { "Medical_MedicalID" });
            DropIndex("dbo.Toddlers", new[] { "Food_FoodID" });
            DropIndex("dbo.RelationLinks", new[] { "Toddler_ToddlerId" });
            DropIndex("dbo.Persons", new[] { "Address_AddressId" });
            DropPrimaryKey("dbo.Pickups");
            DropPrimaryKey("dbo.Employees");
            DropPrimaryKey("dbo.Toddlers");
            DropPrimaryKey("dbo.Doctors");
            AlterColumn("dbo.Pickups", "PickupId", c => c.Int(nullable: false));
            AlterColumn("dbo.Pickups", "Person_PersonId", c => c.Int(nullable: false));
            AlterColumn("dbo.Employees", "EmployeeID", c => c.Int(nullable: false));
            AlterColumn("dbo.Employees", "Person_PersonId", c => c.Int(nullable: false));
            AlterColumn("dbo.Toddlers", "ToddlerId", c => c.Int(nullable: false));
            AlterColumn("dbo.Toddlers", "Person_PersonId", c => c.Int(nullable: false));
            AlterColumn("dbo.Doctors", "DoctorId", c => c.Int(nullable: false));
            AlterColumn("dbo.Doctors", "Person_PersonId", c => c.Int(nullable: false));
            DropColumn("dbo.RelationLinks", "Toddler_ToddlerId");
            DropColumn("dbo.RelationLinks", "RelationToChild");
            DropColumn("dbo.AspNetUsers", "Person_PersonId");
            DropColumn("dbo.Parents", "Person_PersonId");
            DropColumn("dbo.Persons", "Address_AddressId");
            DropColumn("dbo.Toddlers", "Sleep_SleepID");
            DropColumn("dbo.Toddlers", "Medical_MedicalID");
            DropColumn("dbo.Toddlers", "Food_FoodID");
            DropColumn("dbo.ContactDetail", "Person_PersonId");
            DropTable("dbo.RelationLinkPerson");
            AddPrimaryKey("dbo.Pickups", "PersonId");
            AddPrimaryKey("dbo.Employees", "PersonId");
            AddPrimaryKey("dbo.Toddlers", "PersonId");
            AddPrimaryKey("dbo.Doctors", "PersonId");
            RenameIndex(table: "dbo.MedicalInfo", name: "IX_Doctor_DoctorId", newName: "IX_Doctor_PersonId");
            RenameIndex(table: "dbo.Toddlers", name: "IX_Doctor_DoctorId", newName: "IX_Doctor_PersonId");
            RenameColumn(table: "dbo.Pickups", name: "Person_PersonId", newName: "PersonId");
            RenameColumn(table: "dbo.Employees", name: "Person_PersonId", newName: "PersonId");
            RenameColumn(table: "dbo.Toddlers", name: "Person_PersonId", newName: "PersonId");
            RenameColumn(table: "dbo.Doctors", name: "Person_PersonId", newName: "PersonId");
            RenameColumn(table: "dbo.Toddlers", name: "Doctor_DoctorId", newName: "Doctor_PersonId");
            RenameColumn(table: "dbo.MedicalInfo", name: "Doctor_DoctorId", newName: "Doctor_PersonId");
            CreateIndex("dbo.Pickups", "PersonId");
            CreateIndex("dbo.Employees", "PersonId");
            CreateIndex("dbo.Toddlers", "PersonId");
            CreateIndex("dbo.Doctors", "PersonId");
            CreateIndex("dbo.PersonApplicationUser", "ApplicationUser_Id");
            CreateIndex("dbo.PersonApplicationUser", "Person_PersonId");
            AddForeignKey("dbo.Toddlers", "Doctor_PersonId", "dbo.Doctors", "PersonId");
            AddForeignKey("dbo.Medical", "Doctor_PersonId", "dbo.Doctors", "PersonId");
            AddForeignKey("dbo.PersonApplicationUser", "ApplicationUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PersonApplicationUser", "Person_PersonId", "dbo.Persons", "PersonId", cascadeDelete: true);
            RenameTable(name: "dbo.Sleeping", newName: "Sleep");
            RenameTable(name: "dbo.RelationLinks", newName: "RelationLink");
            RenameTable(name: "dbo.MedicalInfo", newName: "Medical");
            RenameTable(name: "dbo.Eating", newName: "Food");
        }
    }
}
