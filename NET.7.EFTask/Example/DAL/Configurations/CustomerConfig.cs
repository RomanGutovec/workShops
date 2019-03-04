using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configurations
{
    public class CustomerConfig : EntityTypeConfiguration<Customer>
    {
        public CustomerConfig()
        {
            this.ToTable("tbl_customers").HasKey(c => c.Id);
            this.Property(c => c.Id).HasColumnName("cln_customer_id");
            this.Property(c => c.Name).HasColumnName("cln_customer_name");
            this.Property(c => c.Address).HasColumnName("cln_customer_address");
            this.Property(c => c.City).HasColumnName("cln_customer_city");
            this.Property(c => c.State).HasColumnName("cln_customer_state");
            this.HasMany(c => c.Orders)
                .WithRequired(o => o.Customer)
                .HasForeignKey(o => o.CustomerId);
        }
    }
}
