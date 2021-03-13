namespace ConsertoDeLivro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CampoCEP : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Usuario", "CEP", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Usuario", "CEP", c => c.String(maxLength: 9));
        }
    }
}
