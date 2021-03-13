namespace ConsertoDeLivro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ColunasEstadoCidadeEmEndereco : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_endereco", "Estado", c => c.String());
            AddColumn("dbo.tbl_endereco", "Cidade", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbl_endereco", "Cidade");
            DropColumn("dbo.tbl_endereco", "Estado");
        }
    }
}
