namespace wlasny_biblioteka.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Czytelniks",
                c => new
                    {
                        CzytelnikID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.CzytelnikID);
            
            CreateTable(
                "dbo.Ksiazkas",
                c => new
                    {
                        KsiazkaID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.KsiazkaID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Ksiazkas");
            DropTable("dbo.Czytelniks");
        }
    }
}
