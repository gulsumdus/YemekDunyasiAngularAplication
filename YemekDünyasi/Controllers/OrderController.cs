using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YemekDünyasi.Models;

namespace YemekDünyasi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly YemekDünyasContext _dbContext;

        public OrderController(YemekDünyasContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]

        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            if (_dbContext.OrderTable == null)
            {
                return NotFound();
            }
            return await _dbContext.OrderTable.ToListAsync();

        }

        [HttpGet("{id}")]

        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            if (_dbContext.OrderTable == null)
            {
                return NotFound();
            }
            var order = await _dbContext.OrderTable.FindAsync(id);
            if (order== null)
            {
                return NotFound();
            }
            return order;

        }
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(int userId)
        {
            if (userId <= 0)
            {
                return BadRequest("Invalid user ID.");
            }

            User user = await _dbContext.UsersTable.FindAsync(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            Order order = new Order
            {
                User = user,
                OrderDate = DateTime.Now
            };

            _dbContext.OrderTable.Add(order);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
        }
        private bool OrderExists(int id)
        {
            return _dbContext.OrderTable.Any(x => x.Id == id);
        }

        [HttpPut("{id}")] // UPDATE
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            _dbContext.Entry(order).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteOrder(int id)
        {
            if (_dbContext.OrderTable == null)
            {
                return NotFound();
            }
            var order = await _dbContext.OrderTable.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            _dbContext.Remove(order);

            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
