namespace ConsertoDeLivro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InsertRecusadoOnPedido : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pedido", "Recusado", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pedido", "Recusado");
        }
    }
}
