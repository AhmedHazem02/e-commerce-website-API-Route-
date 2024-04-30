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
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(p=>p.SubTotal).HasColumnType("decimal(18,2)");
            builder.OwnsOne(p => p.ShippingAddress, o => o.WithOwner());
            builder.Property(U=>U.Status).HasConversion(OItem=>OItem.ToString(),
                   OStatus=>(OrderStatus)Enum.Parse(typeof(OrderStatus),OStatus));
            builder.HasOne(O => O.DeliveryMethod)
                   .WithMany()
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
