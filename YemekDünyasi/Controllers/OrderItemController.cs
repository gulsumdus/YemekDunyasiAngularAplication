using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YemekDünyasi.Models;

namespace YemekDünyasi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly YemekDünyasContext _dbContext;

        public OrderItemController(YemekDünyasContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/OrderItem
        [HttpGet]
        public ActionResult<IEnumerable<OrderItem>> GetOrderItems()
        {
            var orderItems = _dbContext.OrderItemTable.ToList();
            return Ok(orderItems);
        }

        // GET: api/OrderItem/{id}
        [HttpGet("{id}")]
        public ActionResult<OrderItem> GetOrderItem(int id)
        {
            var orderItem = _dbContext.OrderItemTable.FirstOrDefault(o => o.OrderId == id);

            if (orderItem == null)
            {
                return NotFound();
            }

            return Ok(orderItem);
        }

        // POST: api/OrderItem
        [HttpPost]
        public async Task<ActionResult<OrderItem>> PostOrderItem(OrderItem orderItem)
        {
            _dbContext.OrderItemTable.Add(orderItem);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrderItem), new { id = orderItem.OrderId }, orderItem);
        }

        // PUT: api/OrderItem/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderItem(int id, OrderItem updatedOrderItem)
        {
            if (id != updatedOrderItem.OrderId)
            {
                return BadRequest();
            }

            var orderItem = await _dbContext.OrderItemTable.FindAsync(id);

            if (orderItem == null)
            {
                return NotFound();
            }

            orderItem.ProductId = updatedOrderItem.ProductId;
            orderItem.Quantity = updatedOrderItem.Quantity;

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/OrderItem/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderItem(int id)
        {
            var orderItem = await _dbContext.OrderItemTable.FindAsync(id);

            if (orderItem == null)
            {
                return NotFound();
            }

            _dbContext.OrderItemTable.Remove(orderItem);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
