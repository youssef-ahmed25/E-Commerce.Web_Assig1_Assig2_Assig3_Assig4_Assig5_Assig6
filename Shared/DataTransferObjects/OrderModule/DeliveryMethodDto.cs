﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.OrderModule
{
    public class DeliveryMethodDto
    {
        public int Id { get; set; }
        public string ShortName { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string DeliveryTime { get; set; } = default!;
        public decimal Price { get; set; }
    }
}
