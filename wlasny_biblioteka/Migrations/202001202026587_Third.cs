namespace wlasny_biblioteka.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Third : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Czytelniks", "DataZapisania");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Czytelniks", "DataZapisania", c => c.DateTime(nullable: false));
        }
    }
}
