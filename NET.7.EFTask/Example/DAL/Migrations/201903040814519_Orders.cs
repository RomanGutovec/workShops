namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class Orders : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbl_orders",
                c => new
                {
                    cln_order_id = c.Int(nullable: false, identity: true),
                    cln_order_date = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.cln_order_id);

            CreateTable(
                "dbo.tbl_order_items",
                c => new
                {
                    cln_order_id = c.Int(nullable: false),
                    cln_item_id = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.cln_order_id, t.cln_item_id })
                .ForeignKey("dbo.tbl_orders", t => t.cln_order_id, cascadeDelete: true)
                .ForeignKey("dbo.tbl_items", t => t.cln_item_id, cascadeDelete: true)
                .Index(t => t.cln_order_id)
                .Index(t => t.cln_item_id);

            Sql(@"SET IDENTITY_INSERT tbl_orders ON;" + @"insert into tbl_orders (cln_order_id, cln_order_date) values

                 (125, '09/13/2002'),

                 (126, '09/14/2002')

                ");

            Sql(@"insert into tbl_order_items (cln_order_id, cln_item_id) values

                 (125, 563),

                 (125, 851),

                 (125, 625),

                 (126, 563),

                 (126, 625)

                ");
        }

        public override void Down()
        {
            DropForeignKey("dbo.tbl_order_items", "cln_item_id", "dbo.tbl_items");
            DropForeignKey("dbo.tbl_order_items", "cln_order_id", "dbo.tbl_orders");
            DropIndex("dbo.tbl_order_items", new[] { "cln_item_id" });
            DropIndex("dbo.tbl_order_items", new[] { "cln_order_id" });
            DropTable("dbo.tbl_order_items");
            DropTable("dbo.tbl_orders");
        }
    }
}
