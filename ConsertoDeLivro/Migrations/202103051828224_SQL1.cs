namespace ConsertoDeLivro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SQL1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Endereco", "EstadoID", "dbo.Estado");
            DropIndex("dbo.Endereco", new[] { "EstadoID" });
            DropTable("dbo.Endereco");
            DropTable("dbo.Estado");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Estado",
                c => new
                    {
                        EstadoID = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        UF = c.String(),
                    })
                .PrimaryKey(t => t.EstadoID);
            
            CreateTable(
                "dbo.Endereco",
                c => new
                    {
                        EnderecoID = c.Int(nullable: false, identity: true),
                        CEP = c.String(),
                        Logradouro = c.String(),
                        Numero = c.String(),
                        Complemento = c.String(),
                        Bairro = c.String(),
                        Cidade = c.String(),
                        EstadoID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EnderecoID);
            
            CreateIndex("dbo.Endereco", "EstadoID");
            AddForeignKey("dbo.Endereco", "EstadoID", "dbo.Estado", "EstadoID", cascadeDelete: true);
        }
    }
}
