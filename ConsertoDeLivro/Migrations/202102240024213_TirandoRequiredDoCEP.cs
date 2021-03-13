namespace ConsertoDeLivro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TirandoRequiredDoCEP : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tbl_endereco", "CEP", c => c.String(maxLength: 8));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tbl_endereco", "CEP", c => c.String(nullable: false, maxLength: 8));
        }
    }
}
