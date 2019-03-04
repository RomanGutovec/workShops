namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class OrderItem : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.tbl_order_items");
            AddColumn("dbo.tbl_order_items", "cln_order_item_id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.tbl_order_items", "cln_order_item_quantity", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.tbl_order_items", "cln_order_item_id");

            Sql(@"update tbl_order_items
                set cln_order_item_quantity=4 where cln_order_id=125 and cln_item_id=563"+
                @"update tbl_order_items
                set cln_order_item_quantity=500 where cln_order_id=126 and cln_item_id=563"+
                @"update tbl_order_items
                set cln_order_item_quantity=5 where cln_order_id=125 and cln_item_id=625"+
                @"update tbl_order_items
                set cln_order_item_quantity=750 where cln_order_id=126 and cln_item_id=625"+
                @"update tbl_order_items
                set cln_order_item_quantity=32 where cln_order_id=125 and cln_item_id=851
                ");

        }

        public override void Down()
        {
            DropPrimaryKey("dbo.tbl_order_items");
            DropColumn("dbo.tbl_order_items", "cln_order_item_quantity");
            DropColumn("dbo.tbl_order_items", "cln_order_item_id");
            AddPrimaryKey("dbo.tbl_order_items", new[] { "cln_order_id", "cln_item_id" });
        }
    }
}
