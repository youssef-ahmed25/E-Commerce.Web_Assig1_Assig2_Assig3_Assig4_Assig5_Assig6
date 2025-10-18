﻿using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.BasketModule
{
    public class BasketItemDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = null!;
        public string ProductUrl { get; set; } = null!;
        [Range(1,double.MaxValue)]
        public decimal Price { get; set; }
        [Range(1, 100)]
        public int Quantity { get; set; }
    }
}