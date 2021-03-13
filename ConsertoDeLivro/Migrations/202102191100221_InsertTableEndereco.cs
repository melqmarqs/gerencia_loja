namespace ConsertoDeLivro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InsertTableEndereco : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbl_endereco",
                c => new
                    {
                        EnderecoID = c.Int(nullable: false, identity: true),
                        CEP = c.String(nullable: false, maxLength: 8),
                        Logradouro = c.String(),
                        Numero = c.String(),
                        Bairro = c.String(),
                    })
                .PrimaryKey(t => t.EnderecoID);
            
            AddColumn("dbo.tbl_usuarios", "UltimoNome", c => c.String(nullable: false));
            AddColumn("dbo.tbl_usuarios", "Endereco_EnderecoID", c => c.Int());
            CreateIndex("dbo.tbl_usuarios", "Endereco_EnderecoID");
            AddForeignKey("dbo.tbl_usuarios", "Endereco_EnderecoID", "dbo.tbl_endereco", "EnderecoID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tbl_usuarios", "Endereco_EnderecoID", "dbo.tbl_endereco");
            DropIndex("dbo.tbl_usuarios", new[] { "Endereco_EnderecoID" });
            DropColumn("dbo.tbl_usuarios", "Endereco_EnderecoID");
            DropColumn("dbo.tbl_usuarios", "UltimoNome");
            DropTable("dbo.tbl_endereco");
        }
    }
}
