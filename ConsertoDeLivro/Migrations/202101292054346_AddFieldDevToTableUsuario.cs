namespace ConsertoDeLivro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFieldDevToTableUsuario : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_usuarios", "Dev", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbl_usuarios", "Dev");
        }
    }
}
