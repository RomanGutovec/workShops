namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbl_items",
                c => new
                {
                    cln_item_id = c.Int(nullable: false, identity: true),
                    cln_item_description = c.String(),
                    cln_item_price = c.Decimal(nullable: false, precision: 18, scale: 2),
                })
                .PrimaryKey(t => t.cln_item_id);

            Sql(@"SET IDENTITY_INSERT tbl_items ON;" + @"insert into tbl_items (cln_item_id, cln_item_description, cln_item_price) values

                 (563, '56'' Blue Freen', 3.50),

                 (851, 'Spline End (Xtra Large)', 0.25),

                 (625, '3'' Red Freen', 12.00)

                ");

        }

        public override void Down()
        {
            DropTable("dbo.tbl_items");
        }
    }
}
