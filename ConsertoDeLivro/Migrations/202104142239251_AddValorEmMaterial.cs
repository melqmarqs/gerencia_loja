namespace ConsertoDeLivro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddValorEmMaterial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Material", "ValorMetro", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Material", "ValorMetro");
        }
    }
}
