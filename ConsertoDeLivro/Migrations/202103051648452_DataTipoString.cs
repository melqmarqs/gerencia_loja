namespace ConsertoDeLivro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataTipoString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Pedido", "DataPedido", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pedido", "DataPedido", c => c.DateTime(nullable: false));
        }
    }
}
