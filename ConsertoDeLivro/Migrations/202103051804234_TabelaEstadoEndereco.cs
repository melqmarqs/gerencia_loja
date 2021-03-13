namespace ConsertoDeLivro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TabelaEstadoEndereco : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Usuario", "CEP");
            DropColumn("dbo.Usuario", "Logradouro");
            DropColumn("dbo.Usuario", "Numero");
            DropColumn("dbo.Usuario", "Bairro");
            DropColumn("dbo.Usuario", "Estado");
            DropColumn("dbo.Usuario", "Cidade");
            DropColumn("dbo.Usuario", "Complemento");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Usuario", "Complemento", c => c.String());
            AddColumn("dbo.Usuario", "Cidade", c => c.String());
            AddColumn("dbo.Usuario", "Estado", c => c.String());
            AddColumn("dbo.Usuario", "Bairro", c => c.String());
            AddColumn("dbo.Usuario", "Numero", c => c.String());
            AddColumn("dbo.Usuario", "Logradouro", c => c.String());
            AddColumn("dbo.Usuario", "CEP", c => c.String());
        }
    }
}
