﻿using MassTransit;
using Microsoft.EntityFrameworkCore;
using Order.Infrastructure;
using Shared.Messages;

namespace Order.Application.Consumers
{
    public class ProductNameChangedEventConsumer : IConsumer<ProductNameChangedEvent>
    {
        private readonly AppDbContext _context;

        public ProductNameChangedEventConsumer(AppDbContext context)
        {
            _context = context;
        }

        public async Task Consume(ConsumeContext<ProductNameChangedEvent> context)
        {
            var orderItems = await _context.OrderItems.Where(x => x.ProductId == context.Message.ProductId)
                .ToListAsync();

            orderItems.ForEach(x => { x.UpdateOrderItem(context.Message.UpdatedName, x.PictureUrl, x.Price); });

            await _context.SaveChangesAsync();
        }
    }
}