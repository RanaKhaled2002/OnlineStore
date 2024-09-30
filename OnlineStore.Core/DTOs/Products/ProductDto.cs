﻿using OnlineStore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.DTOs.Products
{
    public class ProductDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string PictureUrl { get; set; }

        public decimal Price { get; set; }

        public int? BrandId { get; set; }

        public string BrandName { get; set; }

        public int? TypeId { get; set; }

        public string TypeName { get; set; }
    }
}
