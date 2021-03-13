namespace ConsertoDeLivro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Usuario1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Estado",
                c => new
                    {
                        EstadoID = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        UF = c.String(),
                    })
                .PrimaryKey(t => t.EstadoID);
            
            DropColumn("dbo.Usuario", "Estado");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Usuario", "Estado", c => c.String());
            DropTable("dbo.Estado");
        }
    }
}
