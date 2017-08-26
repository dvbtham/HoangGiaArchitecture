namespace HoangGia.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSlideTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Slides",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Image = c.String(),
                        Url = c.String(),
                        IsActived = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Slides");
        }
    }
}
