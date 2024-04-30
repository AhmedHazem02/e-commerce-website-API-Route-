﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.OrderAggragate
{
    public class OrderIteml:BaseEntity
    {
        public OrderIteml()
        {
            
        }
        public OrderIteml(ProductItemOrder product, decimal price, int quantity)
        {
            Product = product;
            Price = price;
            Quantity = quantity;
        }

        public ProductItemOrder Product  { get; set; }
        public decimal Price { get; set;}
        public int Quantity {  get; set;}
    }
}
