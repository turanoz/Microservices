﻿using MediatR;
using Order.Application.Commands;
using Order.Application.Dtos;
using Order.Domain;
using Order.Infrastructure;
using Shared.Dtos;

namespace Order.Application.Handlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Response<CreatedOrderDto>>
    {
        private readonly AppDbContext _context;

        public CreateOrderCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Response<CreatedOrderDto>> Handle(CreateOrderCommand request,
            CancellationToken cancellationToken)
        {
            var newAddress = new Address(request.Address.Province, request.Address.District, request.Address.Street,
                request.Address.ZipCode, request.Address.Line);

            Domain.Order newOrder = new Domain.Order(request.BuyerId, newAddress, request.TotalPrice);

            request.OrderItems.ForEach(x =>
            {
                newOrder.AddOrderItem(x.ProductId, x.ProductName, x.Quantity, x.Price, x.PictureUrl);
            });

            await _context.Orders.AddAsync(newOrder, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return Response<CreatedOrderDto>.Success(new CreatedOrderDto { OrderId = newOrder.Id }, 200);
        }
    }
}