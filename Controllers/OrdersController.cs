using coffee_api.Dtos.Order;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using thoeun_coffee.Data;
using thoeun_coffee.Models;

namespace thoeun_coffee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet("orders/{userId}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders(int userId)
        {
            var orders = await _context.Orders
                .OrderByDescending(o => o.OrderId)
                .Where(o => o.UserId == userId)  // Filter by UserId
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                .ToListAsync();

            if (orders == null || !orders.Any())
            {
                return NotFound(new { message = "No orders found for this user." });
            }

            return Ok(orders);
        }


        [HttpPut("orders/{orderId}")]
        public async Task<IActionResult> EditOrder(int orderId, [FromBody] EditOrderDto updatedOrder)
        {
            // Validate orderId
            if (orderId <= 0)
            {
                return BadRequest(new { message = "Invalid orderId provided." });
            }

            // Find the order
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (order == null)
            {
                return NotFound(new { message = "Order not found." });
            }

            // Update order fields
            order.OrderStatus = updatedOrder.OrderStatus ?? order.OrderStatus;
            // order.TotalAmount = updatedOrder.TotalAmount ?? order.TotalAmount;
            // order.OrderDate = updatedOrder.OrderDate ?? order.OrderDate;
            order.DeliveryAddress = updatedOrder.DeliveryAddress ?? order.DeliveryAddress;

            // Handle User and Payment (optional fields)
            if (updatedOrder.UserId.HasValue)
            {
                var user = await _context.Users.FindAsync(updatedOrder.UserId.Value);
                if (user == null)
                {
                    return NotFound(new { message = "User not found." });
                }
                order.UserId = updatedOrder.UserId;
                order.User = user;
            }

            // if (updatedOrder.PaymentId.HasValue)
            // {
            //     var payment = await _context.Payments.FindAsync(updatedOrder.PaymentId.Value);
            //     if (payment == null)
            //     {
            //         return NotFound(new { message = "Payment not found." });
            //     }
            //     order.PaymentId = updatedOrder.PaymentId;
            //     order.Payment = payment;
            // }

            // Clear and re-add order items if provided
            if (updatedOrder.OrderItems != null && updatedOrder.OrderItems.Any())
            {
                // Remove existing items
                _context.OrderItems.RemoveRange(order.OrderItems);

                // Add updated items
                order.OrderItems = updatedOrder.OrderItems.Select(oi => new OrderItem
                {
                    ProductId = oi.ProductId,
                    Quantity = oi.Quantity,
                    Price = oi.Price
                }).ToList();
            }

            // Save changes
            await _context.SaveChangesAsync();

            return Ok(new { message = "Order updated successfully." });
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Orders.Include(o => o.User).Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.OrderId == id);
            if (order == null) return NotFound();
            return order;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetOrder), new { id = order.OrderId }, order);
        }



        [HttpDelete("orders/{orderId}")]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            if (orderId <= 0)
            {
                return BadRequest(new { message = "Invalid orderId provided." });
            }

            // Find the order
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (order == null)
            {
                return NotFound(new { message = "Order not found." });
            }

            // Remove related order items and the order itself
            _context.OrderItems.RemoveRange(order.OrderItems);
            _context.Orders.Remove(order);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Failed to delete order.", error = ex.Message });
            }

            return Ok(new { message = "Order deleted successfully." });
        }


        private bool OrderExists(int id) => _context.Orders.Any(e => e.OrderId == id);
    }
}
