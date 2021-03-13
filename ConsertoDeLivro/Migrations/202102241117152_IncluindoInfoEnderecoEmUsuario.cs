namespace ConsertoDeLivro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IncluindoInfoEnderecoEmUsuario : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tbl_usuarios", "Endereco_EnderecoID", "dbo.tbl_endereco");
            DropIndex("dbo.tbl_usuarios", new[] { "Endereco_EnderecoID" });
            AddColumn("dbo.tbl_usuarios", "CEP", c => c.String(maxLength: 8));
            AddColumn("dbo.tbl_usuarios", "Logradouro", c => c.String());
            AddColumn("dbo.tbl_usuarios", "Numero", c => c.String());
            AddColumn("dbo.tbl_usuarios", "Bairro", c => c.String());
            AddColumn("dbo.tbl_usuarios", "Estado", c => c.String());
            AddColumn("dbo.tbl_usuarios", "Cidade", c => c.String());
            DropColumn("dbo.tbl_usuarios", "Endereco_EnderecoID");
            DropTable("dbo.tbl_endereco");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.tbl_endereco",
                c => new
                    {
                        EnderecoID = c.Int(nullable: false, identity: true),
                        CEP = c.String(maxLength: 8),
                        Logradouro = c.String(),
                        Numero = c.String(),
                        Bairro = c.String(),
                        Estado = c.String(),
                        Cidade = c.String(),
                    })
                .PrimaryKey(t => t.EnderecoID);
            
            AddColumn("dbo.tbl_usuarios", "Endereco_EnderecoID", c => c.Int());
            DropColumn("dbo.tbl_usuarios", "Cidade");
            DropColumn("dbo.tbl_usuarios", "Estado");
            DropColumn("dbo.tbl_usuarios", "Bairro");
            DropColumn("dbo.tbl_usuarios", "Numero");
            DropColumn("dbo.tbl_usuarios", "Logradouro");
            DropColumn("dbo.tbl_usuarios", "CEP");
            CreateIndex("dbo.tbl_usuarios", "Endereco_EnderecoID");
            AddForeignKey("dbo.tbl_usuarios", "Endereco_EnderecoID", "dbo.tbl_endereco", "EnderecoID");
        }
    }
}
