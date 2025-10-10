﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class ProductBrand : BaseEntity<int>
    {
        public string Name { get; set; } = null!;

        //public ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
