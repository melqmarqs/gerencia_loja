namespace ConsertoDeLivro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveStringLengthFromEmailInUsuarios : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tbl_usuarios", "CPF", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tbl_usuarios", "CPF", c => c.String(nullable: false, maxLength: 11));
        }
    }
}
