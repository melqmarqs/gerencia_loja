namespace ConsertoDeLivro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AtualizacaoTabelaPedido : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Pedido", "Material_Id", "dbo.Material");
            DropIndex("dbo.Pedido", new[] { "Material_Id" });
            RenameColumn(table: "dbo.Pedido", name: "Material_Id", newName: "MaterialID");
            AlterColumn("dbo.Pedido", "MaterialID", c => c.Int(nullable: false));
            CreateIndex("dbo.Pedido", "MaterialID");
            AddForeignKey("dbo.Pedido", "MaterialID", "dbo.Material", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pedido", "MaterialID", "dbo.Material");
            DropIndex("dbo.Pedido", new[] { "MaterialID" });
            AlterColumn("dbo.Pedido", "MaterialID", c => c.Int());
            RenameColumn(table: "dbo.Pedido", name: "MaterialID", newName: "Material_Id");
            CreateIndex("dbo.Pedido", "Material_Id");
            AddForeignKey("dbo.Pedido", "Material_Id", "dbo.Material", "Id");
        }
    }
}
