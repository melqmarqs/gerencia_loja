namespace ConsertoDeLivro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColunaCodigoToMaterial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Material", "Codigo", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Material", "Codigo");
        }
    }
}
