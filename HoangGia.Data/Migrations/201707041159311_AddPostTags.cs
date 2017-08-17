namespace HoangGia.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPostTags : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "Tags", c => c.String(maxLength: 256));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "Tags");
        }
    }
}
