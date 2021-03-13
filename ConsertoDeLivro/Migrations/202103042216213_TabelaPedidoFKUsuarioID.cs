namespace ConsertoDeLivro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TabelaPedidoFKUsuarioID : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Pedido", "Usuario_UsuarioID", "dbo.Usuario");
            DropIndex("dbo.Pedido", new[] { "Usuario_UsuarioID" });
            RenameColumn(table: "dbo.Pedido", name: "Usuario_UsuarioID", newName: "UsuarioID");
            AlterColumn("dbo.Pedido", "UsuarioID", c => c.Int(nullable: false));
            CreateIndex("dbo.Pedido", "UsuarioID");
            AddForeignKey("dbo.Pedido", "UsuarioID", "dbo.Usuario", "UsuarioID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pedido", "UsuarioID", "dbo.Usuario");
            DropIndex("dbo.Pedido", new[] { "UsuarioID" });
            AlterColumn("dbo.Pedido", "UsuarioID", c => c.Int());
            RenameColumn(table: "dbo.Pedido", name: "UsuarioID", newName: "Usuario_UsuarioID");
            CreateIndex("dbo.Pedido", "Usuario_UsuarioID");
            AddForeignKey("dbo.Pedido", "Usuario_UsuarioID", "dbo.Usuario", "UsuarioID");
        }
    }
}
