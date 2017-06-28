namespace HoangGia.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeXmlFields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Services", "Images", c => c.String());
            AlterColumn("dbo.Services", "ChooseUsValues", c => c.String());
            AlterColumn("dbo.Settings", "WhyChooseUsTitles", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Settings", "WhyChooseUsTitles", c => c.String(storeType: "xml"));
            AlterColumn("dbo.Services", "ChooseUsValues", c => c.String(storeType: "xml"));
            AlterColumn("dbo.Services", "Images", c => c.String(storeType: "xml"));
        }
    }
}
