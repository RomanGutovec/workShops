using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configurations
{
    public class OrderItemConfig: EntityTypeConfiguration<OrderItem>
    {
        public OrderItemConfig()
        {
            this.ToTable("tbl_order_items").HasKey(oi => oi.Id);
            this.Property(oi => oi.Id).HasColumnName("cln_order_item_id");
            this.Property(oi => oi.ItemId).HasColumnName("cln_item_id");
            this.Property(oi => oi.OrderId).HasColumnName("cln_order_id");
            this.Property(oi => oi.Quantity).HasColumnName("cln_order_item_quantity");
        }
    }
}
