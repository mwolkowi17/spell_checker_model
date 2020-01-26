namespace wlasny_biblioteka.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Second : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Czytelniks", "ImieNazisko", c => c.String());
            AddColumn("dbo.Czytelniks", "DataZapisania", c => c.DateTime(nullable: false));
            AddColumn("dbo.Czytelniks", "Zapisany", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ksiazkas", "Tytul", c => c.String());
            AddColumn("dbo.Ksiazkas", "Autor", c => c.String());
            AddColumn("dbo.Ksiazkas", "DataWydania", c => c.String());
            AddColumn("dbo.Ksiazkas", "dostępna", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ksiazkas", "Wypozyczajacy_CzytelnikID", c => c.Int());
            CreateIndex("dbo.Ksiazkas", "Wypozyczajacy_CzytelnikID");
            AddForeignKey("dbo.Ksiazkas", "Wypozyczajacy_CzytelnikID", "dbo.Czytelniks", "CzytelnikID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ksiazkas", "Wypozyczajacy_CzytelnikID", "dbo.Czytelniks");
            DropIndex("dbo.Ksiazkas", new[] { "Wypozyczajacy_CzytelnikID" });
            DropColumn("dbo.Ksiazkas", "Wypozyczajacy_CzytelnikID");
            DropColumn("dbo.Ksiazkas", "dostępna");
            DropColumn("dbo.Ksiazkas", "DataWydania");
            DropColumn("dbo.Ksiazkas", "Autor");
            DropColumn("dbo.Ksiazkas", "Tytul");
            DropColumn("dbo.Czytelniks", "Zapisany");
            DropColumn("dbo.Czytelniks", "DataZapisania");
            DropColumn("dbo.Czytelniks", "ImieNazisko");
        }
    }
}
