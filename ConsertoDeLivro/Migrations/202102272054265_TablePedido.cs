namespace ConsertoDeLivro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TablePedido : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tbl_servicos", "Usuario_UsuarioID", "dbo.tbl_usuarios");
            DropIndex("dbo.tbl_servicos", new[] { "Usuario_UsuarioID" });
            CreateTable(
                "dbo.Material",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Descricao = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Pedido",
                c => new
                    {
                        PedidoID = c.Int(nullable: false, identity: true),
                        Largura = c.String(),
                        Comprimento = c.String(),
                        Expessura = c.String(),
                        Aceito = c.Boolean(nullable: false),
                        Feito = c.Boolean(nullable: false),
                        Entregue = c.Boolean(nullable: false),
                        Material_Id = c.Int(),
                        Usuario_UsuarioID = c.Int(),
                    })
                .PrimaryKey(t => t.PedidoID)
                .ForeignKey("dbo.Material", t => t.Material_Id)
                .ForeignKey("dbo.tbl_usuarios", t => t.Usuario_UsuarioID)
                .Index(t => t.Material_Id)
                .Index(t => t.Usuario_UsuarioID);
            
            DropTable("dbo.tbl_servicos");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.tbl_servicos",
                c => new
                    {
                        ServicoID = c.Int(nullable: false, identity: true),
                        Usuario_UsuarioID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ServicoID);
            
            DropForeignKey("dbo.Pedido", "Usuario_UsuarioID", "dbo.tbl_usuarios");
            DropForeignKey("dbo.Pedido", "Material_Id", "dbo.Material");
            DropIndex("dbo.Pedido", new[] { "Usuario_UsuarioID" });
            DropIndex("dbo.Pedido", new[] { "Material_Id" });
            DropTable("dbo.Pedido");
            DropTable("dbo.Material");
            CreateIndex("dbo.tbl_servicos", "Usuario_UsuarioID");
            AddForeignKey("dbo.tbl_servicos", "Usuario_UsuarioID", "dbo.tbl_usuarios", "UsuarioID", cascadeDelete: true);
        }
    }
}
