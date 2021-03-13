namespace ConsertoDeLivro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Senha : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_usuarios", "Senha", c => c.String(nullable: false, maxLength: 13));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbl_usuarios", "Senha");
        }
    }
}
