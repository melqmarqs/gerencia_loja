namespace ConsertoDeLivro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AA : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Usuario", "UltimoNome", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Usuario", "UltimoNome", c => c.String(nullable: false));
        }
    }
}
