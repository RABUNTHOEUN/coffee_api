using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coffee_api.Dtos.Order
{
    public class CreateOrderDto
    {
        public int UserId { get; set; }
        public string DeliveryAddress { get; set; }
        public List<CreateOrderItemDto> OrderItems { get; set; }
    }
}