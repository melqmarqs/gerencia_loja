namespace ConsertoDeLivro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataEmPedido : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pedido", "DataPedido", c => c.DateTime(nullable: false));
            AddColumn("dbo.Pedido", "Valor", c => c.Single(nullable: false));
            AddColumn("dbo.Pedido", "Descricao", c => c.String());
            AlterColumn("dbo.Pedido", "Largura", c => c.Single(nullable: false));
            AlterColumn("dbo.Pedido", "Comprimento", c => c.Single(nullable: false));
            AlterColumn("dbo.Pedido", "Expessura", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pedido", "Expessura", c => c.String());
            AlterColumn("dbo.Pedido", "Comprimento", c => c.String());
            AlterColumn("dbo.Pedido", "Largura", c => c.String());
            DropColumn("dbo.Pedido", "Descricao");
            DropColumn("dbo.Pedido", "Valor");
            DropColumn("dbo.Pedido", "DataPedido");
        }
    }
}
