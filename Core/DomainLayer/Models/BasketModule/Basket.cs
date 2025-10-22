﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.BasketModule
{
    public class Basket
    {
        public string Id { get; set; }

        public ICollection<BasketItem> Items { get; set; } = [];
    }
}
