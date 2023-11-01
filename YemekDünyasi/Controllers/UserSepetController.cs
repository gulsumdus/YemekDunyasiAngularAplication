using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YemekDünyasi.Models;

namespace YemekDünyasi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSepetController : ControllerBase
    {
        private readonly YemekDünyasContext _dbContext;

        public UserSepetController(YemekDünyasContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]

        public async Task<ActionResult<IEnumerable<UserSepet>>> GetUserSepets()
        {
            if (_dbContext.UserSepetTable == null)
            {
                return NotFound();
            }
            return await _dbContext.UserSepetTable.ToListAsync();

        }

        [HttpGet("{id}")]

        public async Task<ActionResult<UserSepet>> GetUserSepet(int id)
        {
            if (_dbContext.UserSepetTable == null)
            {
                return NotFound();
            }
            var usersepet = await _dbContext.UserSepetTable.FindAsync(id);
            if (usersepet == null)
            {
                return NotFound();
            }
            return usersepet;

        }
        [HttpPost]
        public async Task<ActionResult<UserSepet>> PostUserSepet(int userId, int productId, int quantity)
        {
            if (userId <= 0 || productId <= 0 || quantity <= 0)
            {
                return BadRequest("Invalid user ID, product ID, or quantity.");
            }

            User user = await _dbContext.UsersTable.FindAsync(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            Product product = await _dbContext.ProductTable.FindAsync(productId);
            if (product == null)
            {
                return NotFound("Product not found.");
            }

            UserSepet userSepet = new UserSepet
            {
                UserId = userId,
                ProductId = productId,
                Quantity = quantity,
                User = user,
                Product = product
            };

            _dbContext.UserSepetTable.Add(userSepet);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserSepet), new { id = userSepet.UserId }, userSepet);
        }
        private bool UserSepetExists(int id)
        {
            return _dbContext.UserSepetTable.Any(x => x.UserId == id);
        }



        [HttpPut("{id}")] // UPDATE
        public async Task<IActionResult> PutUserSepet(int id, UserSepet updatedUserSepet)
        {
            if (id != updatedUserSepet.UserId)
            {
                return BadRequest();
            }

            var userSepet = await _dbContext.UserSepetTable.FindAsync(id);

            if (userSepet == null)
            {
                return NotFound();
            }

            userSepet.Quantity = updatedUserSepet.Quantity;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserSepetExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserSepet(int id)
        {
            var userSepet = await _dbContext.UserSepetTable.FindAsync(id);
            if (userSepet == null)
            {
                return NotFound();
            }

            _dbContext.UserSepetTable.Remove(userSepet);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }




    }
}
