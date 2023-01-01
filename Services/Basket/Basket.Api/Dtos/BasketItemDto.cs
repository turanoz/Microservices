﻿namespace Basket.Api.Dtos
{
    public class BasketItemDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
    }
}