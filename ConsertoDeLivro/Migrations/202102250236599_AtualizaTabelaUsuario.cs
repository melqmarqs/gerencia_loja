namespace ConsertoDeLivro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AtualizaTabelaUsuario : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tbl_usuarios", "CEP", c => c.String(maxLength: 9));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tbl_usuarios", "CEP", c => c.String(maxLength: 8));
        }
    }
}
