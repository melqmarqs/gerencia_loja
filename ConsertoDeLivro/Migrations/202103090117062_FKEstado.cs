namespace ConsertoDeLivro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FKEstado : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Usuario", "EstadoID", c => c.Int(nullable: false));
            CreateIndex("dbo.Usuario", "EstadoID");
            AddForeignKey("dbo.Usuario", "EstadoID", "dbo.Estado", "EstadoID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Usuario", "EstadoID", "dbo.Estado");
            DropIndex("dbo.Usuario", new[] { "EstadoID" });
            DropColumn("dbo.Usuario", "EstadoID");
        }
    }
}
