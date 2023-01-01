﻿using MediatR;
using Order.Application.Dtos;
using Shared.Dtos;

namespace Order.Application.Commands
{
    public class CreateOrderCommand : IRequest<Response<CreatedOrderDto>>
    {
        public string BuyerId { get; set; }

        public List<OrderItemDto> OrderItems { get; set; }

        public AddressDto Address { get; set; }
        public Decimal TotalPrice { get; set; }
    }
}
