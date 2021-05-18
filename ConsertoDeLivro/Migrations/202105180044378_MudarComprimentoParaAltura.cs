namespace ConsertoDeLivro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MudarComprimentoParaAltura : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pedido", "Altura", c => c.String());
            DropColumn("dbo.Pedido", "Comprimento");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pedido", "Comprimento", c => c.String());
            DropColumn("dbo.Pedido", "Altura");
        }
    }
}
