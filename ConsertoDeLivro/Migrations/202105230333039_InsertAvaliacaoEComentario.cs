namespace ConsertoDeLivro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InsertAvaliacaoEComentario : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pedido", "Avaliacao", c => c.Int(nullable: false));
            AddColumn("dbo.Pedido", "Comentario", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pedido", "Comentario");
            DropColumn("dbo.Pedido", "Avaliacao");
        }
    }
}
