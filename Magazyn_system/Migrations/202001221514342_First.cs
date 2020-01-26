namespace Magazyn_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Aplikacjas",
                c => new
                    {
                        AplikacjaID = c.Int(nullable: false, identity: true),
                        Tytul = c.String(),
                        RokProdukcji = c.Int(nullable: false),
                        IloscOkladek = c.Int(nullable: false),
                        Dostepnosc = c.Boolean(nullable: false),
                        Opakownie = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AplikacjaID);
            
            CreateTable(
                "dbo.Films",
                c => new
                    {
                        FilmID = c.Int(nullable: false, identity: true),
                        StanMagazynuTytul = c.Int(nullable: false),
                        Tytul = c.String(),
                        RokProdukcji = c.Int(nullable: false),
                        IloscOkladek = c.Int(nullable: false),
                        Dostepnosc = c.Boolean(nullable: false),
                        Opakownie = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FilmID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Films");
            DropTable("dbo.Aplikacjas");
        }
    }
}
