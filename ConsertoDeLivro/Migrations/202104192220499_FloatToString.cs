namespace ConsertoDeLivro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FloatToString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Material", "Codigo", c => c.String());
            AlterColumn("dbo.Material", "ValorMetro", c => c.String());
            AlterColumn("dbo.Pedido", "Largura", c => c.String());
            AlterColumn("dbo.Pedido", "Comprimento", c => c.String());
            AlterColumn("dbo.Pedido", "Expessura", c => c.String());
            AlterColumn("dbo.Pedido", "Valor", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pedido", "Valor", c => c.Single(nullable: false));
            AlterColumn("dbo.Pedido", "Expessura", c => c.Single(nullable: false));
            AlterColumn("dbo.Pedido", "Comprimento", c => c.Single(nullable: false));
            AlterColumn("dbo.Pedido", "Largura", c => c.Single(nullable: false));
            AlterColumn("dbo.Material", "ValorMetro", c => c.Single(nullable: false));
            AlterColumn("dbo.Material", "Codigo", c => c.Int(nullable: false));
        }
    }
}
