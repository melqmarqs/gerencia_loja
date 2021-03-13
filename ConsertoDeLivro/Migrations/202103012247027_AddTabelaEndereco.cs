namespace ConsertoDeLivro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTabelaEndereco : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Endereco",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Logradouro = c.String(),
                        Numero = c.String(),
                        Complemento = c.String(),
                        Bairro = c.String(),
                        Cidade = c.String(),
                        EstadoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Estado", t => t.EstadoId, cascadeDelete: true)
                .Index(t => t.EstadoId);
            
            CreateTable(
                "dbo.Estado",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        UF = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Usuario", "Endereco_Id", c => c.Int());
            CreateIndex("dbo.Usuario", "Endereco_Id");
            AddForeignKey("dbo.Usuario", "Endereco_Id", "dbo.Endereco", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Usuario", "Endereco_Id", "dbo.Endereco");
            DropForeignKey("dbo.Endereco", "EstadoId", "dbo.Estado");
            DropIndex("dbo.Endereco", new[] { "EstadoId" });
            DropIndex("dbo.Usuario", new[] { "Endereco_Id" });
            DropColumn("dbo.Usuario", "Endereco_Id");
            DropTable("dbo.Estado");
            DropTable("dbo.Endereco");
        }
    }
}
