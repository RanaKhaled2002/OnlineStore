﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.Entities.Basket_Module
{
    public class BasketItem
    {
        public string Id { get; set; }
        public string ProductName { get; set; }
        public string PicuterUrl { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}