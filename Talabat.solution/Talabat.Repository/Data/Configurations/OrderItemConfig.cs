using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.OrderAggragate;

namespace Talabat.Repository.Data.Configurations
{
    public class OrderItemConfig : IEntityTypeConfiguration<OrderIteml>
    {
        public void Configure(EntityTypeBuilder<OrderIteml> builder)
        {
             builder.Property(O=>O.Price).HasColumnType("decimal(18,2)");
            builder.OwnsOne(p => p.Product, o => o.WithOwner());
        }
    }
}
