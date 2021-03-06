namespace ConsertoDeLivro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BDEstadoEEndereco2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Endereco", "EstadoID", "dbo.Estado");
            DropForeignKey("dbo.Usuario", "Endereco_Id", "dbo.Endereco");
            DropIndex("dbo.Usuario", new[] { "Endereco_Id" });
            DropIndex("dbo.Endereco", new[] { "EstadoID" });
            AddColumn("dbo.Usuario", "CEP", c => c.String());
            AddColumn("dbo.Usuario", "Logradouro", c => c.String());
            AddColumn("dbo.Usuario", "Numero", c => c.String());
            AddColumn("dbo.Usuario", "Bairro", c => c.String());
            AddColumn("dbo.Usuario", "Estado", c => c.String());
            AddColumn("dbo.Usuario", "Cidade", c => c.String());
            AddColumn("dbo.Usuario", "Complemento", c => c.String());
            DropColumn("dbo.Usuario", "Endereco_Id");
            DropTable("dbo.Endereco");
            DropTable("dbo.Estado");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Estado",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        UF = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Endereco",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CEP = c.String(),
                        Logradouro = c.String(),
                        Numero = c.String(),
                        Complemento = c.String(),
                        Bairro = c.String(),
                        Cidade = c.String(),
                        EstadoID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Usuario", "Endereco_Id", c => c.Int());
            DropColumn("dbo.Usuario", "Complemento");
            DropColumn("dbo.Usuario", "Cidade");
            DropColumn("dbo.Usuario", "Estado");
            DropColumn("dbo.Usuario", "Bairro");
            DropColumn("dbo.Usuario", "Numero");
            DropColumn("dbo.Usuario", "Logradouro");
            DropColumn("dbo.Usuario", "CEP");
            CreateIndex("dbo.Endereco", "EstadoID");
            CreateIndex("dbo.Usuario", "Endereco_Id");
            AddForeignKey("dbo.Usuario", "Endereco_Id", "dbo.Endereco", "Id");
            AddForeignKey("dbo.Endereco", "EstadoID", "dbo.Estado", "Id", cascadeDelete: true);
        }
    }
}
