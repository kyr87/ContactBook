namespace ContactBook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        ContactId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.ContactId);
            
            CreateTable(
                "dbo.Telephones",
                c => new
                    {
                        TelephoneId = c.Int(nullable: false, identity: true),
                        PhoneNumber = c.String(nullable: false),
                        ContactID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TelephoneId)
                .ForeignKey("dbo.Contacts", t => t.ContactID, cascadeDelete: true)
                .Index(t => t.ContactID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Telephones", "ContactID", "dbo.Contacts");
            DropIndex("dbo.Telephones", new[] { "ContactID" });
            DropTable("dbo.Telephones");
            DropTable("dbo.Contacts");
        }
    }
}
