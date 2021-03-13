namespace ConsertoDeLivro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Usuario : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.tbl_usuarios", newName: "Usuario");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Usuario", newName: "tbl_usuarios");
        }
    }
}
