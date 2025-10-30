﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.OrderModule
{
    public class ProductItemOrdered
    {
        public int ProducId { get; set; }
        public string ProductName { get; set; }= default!;
        public string PictureUrl { get; set; } = default!;
    }
}
