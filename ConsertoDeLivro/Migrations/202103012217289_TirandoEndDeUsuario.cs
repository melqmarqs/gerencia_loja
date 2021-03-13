namespace ConsertoDeLivro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TirandoEndDeUsuario : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Usuario", "Logradouro");
            DropColumn("dbo.Usuario", "Numero");
            DropColumn("dbo.Usuario", "Bairro");
            DropColumn("dbo.Usuario", "Estado");
            DropColumn("dbo.Usuario", "Cidade");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Usuario", "Cidade", c => c.String());
            AddColumn("dbo.Usuario", "Estado", c => c.String());
            AddColumn("dbo.Usuario", "Bairro", c => c.String());
            AddColumn("dbo.Usuario", "Numero", c => c.String());
            AddColumn("dbo.Usuario", "Logradouro", c => c.String());
        }
    }
}
