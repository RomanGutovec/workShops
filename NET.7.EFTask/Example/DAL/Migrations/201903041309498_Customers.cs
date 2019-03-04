namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Customers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbl_customers",
                c => new
                    {
                        cln_customer_id = c.Int(nullable: false, identity: true),
                        cln_customer_name = c.String(),
                        cln_customer_address = c.String(),
                        cln_customer_city = c.String(),
                        cln_customer_state = c.String(),
                    })
                .PrimaryKey(t => t.cln_customer_id);
            
            AddColumn("dbo.tbl_orders", "cln_order_customer_id", c => c.Int(nullable: true));
            CreateIndex("dbo.tbl_orders", "cln_order_customer_id");
            AddForeignKey("dbo.tbl_orders", "cln_order_customer_id", "dbo.tbl_customers", "cln_customer_id", cascadeDelete: true);

            Sql(@"SET IDENTITY_INSERT tbl_customers ON;" +
               @"insert into tbl_customers (cln_customer_id, cln_customer_name, cln_customer_address, cln_customer_city, cln_customer_state) values

               (56, 'Foo, Inc', '23 Main St., Thorpleburg', 'Thropleburg', 'TX'),

               (2, 'Freens R US', '1600 Pensnsylvania Avenue', 'Washington', 'DC')" +

               @"update tbl_orders
               set cln_order_customer_id=56 where cln_order_id=125" +

               @"update tbl_orders
               set cln_order_customer_id=2 where cln_order_id=126"
               );
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tbl_orders", "cln_order_customer_id", "dbo.tbl_customers");
            DropIndex("dbo.tbl_orders", new[] { "cln_order_customer_id" });
            DropColumn("dbo.tbl_orders", "cln_order_customer_id");
            DropTable("dbo.tbl_customers");
        }
    }
}
