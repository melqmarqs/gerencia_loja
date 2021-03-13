namespace ConsertoDeLivro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbl_servicos",
                c => new
                    {
                        ServicoID = c.Int(nullable: false, identity: true),
                        Usuario_UsuarioID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ServicoID)
                .ForeignKey("dbo.tbl_usuarios", t => t.Usuario_UsuarioID, cascadeDelete: true)
                .Index(t => t.Usuario_UsuarioID);
            
            CreateTable(
                "dbo.tbl_usuarios",
                c => new
                    {
                        UsuarioID = c.Int(nullable: false, identity: true),
                        Adm = c.Boolean(nullable: false),
                        Nome = c.String(nullable: false),
                        Email = c.String(),
                        CPF = c.String(nullable: false, maxLength: 11),
                        Celular = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UsuarioID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tbl_servicos", "Usuario_UsuarioID", "dbo.tbl_usuarios");
            DropIndex("dbo.tbl_servicos", new[] { "Usuario_UsuarioID" });
            DropTable("dbo.tbl_usuarios");
            DropTable("dbo.tbl_servicos");
        }
    }
}
