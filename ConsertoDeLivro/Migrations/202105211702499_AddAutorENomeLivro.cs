namespace ConsertoDeLivro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAutorENomeLivro : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pedido", "NomeLivro", c => c.String());
            AddColumn("dbo.Pedido", "AutorLivro", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pedido", "AutorLivro");
            DropColumn("dbo.Pedido", "NomeLivro");
        }
    }
}
