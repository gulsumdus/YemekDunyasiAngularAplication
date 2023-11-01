using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YemekDünyasi.Models;

namespace YemekDünyasi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly YemekDünyasContext _dbContext;

        public UserController(YemekDünyasContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]

        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            if(_dbContext.UsersTable == null)
            {
                return NotFound();
            }
            return await _dbContext.UsersTable.ToListAsync();

        }
      
        [HttpGet("{id}")]

        public async Task<ActionResult<User>> GetUser(int id)
        {
            if (_dbContext.UsersTable == null)
            {
                return NotFound();
            }
            var users = await _dbContext.UsersTable.FindAsync(id);
            if(users == null)
            {
                return NotFound();
            }
            return users;

        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUser(string name, string surname,string password, string email, string address, string telNo, string userResimUrl)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(surname) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(address) || string.IsNullOrEmpty(telNo))
            {
                return BadRequest("Required fields are missing.");
            }

            User user = new User
            {
                UserResimUrl=userResimUrl,
                Name = name,
                Surname = surname,
                Password = password,
                Email = email,
                Adress = address,
                TelNo = telNo
            };

            _dbContext.UsersTable.Add(user);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }
        private bool UserExists(int id)
        {
            return _dbContext.UsersTable.Any(x => x.Id == id);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User updatedUser)
        {
            if (id != updatedUser.Id)
            {
                return BadRequest();
            }

            var existingUser = await _dbContext.UsersTable.FindAsync(id);

            if (existingUser == null)
            {
                return NotFound();
            }
            existingUser.UserResimUrl = updatedUser.UserResimUrl;
            existingUser.Password=updatedUser.Password;
            existingUser.Name = updatedUser.Name;
            existingUser.Surname = updatedUser.Surname;
            existingUser.Email = updatedUser.Email;
            existingUser.Adress = updatedUser.Adress;
            existingUser.TelNo = updatedUser.TelNo;

            try
            {
                _dbContext.UsersTable.Update(existingUser);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        public async Task<IActionResult> DeleteUser(int id)
        {
            if(_dbContext.UsersTable== null)
            {
                return NotFound();
            }
            var user = await _dbContext.UsersTable.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            _dbContext.Remove(user);

            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }

}



/************ NOTLAR*************************
 * "IActionResult" sınıfı, ASP.NET Core web uygulamalarında bir eylemin sonucunu temsil eden
    bir dönüş değerini temsil eder. Bu sınıf, bir eylemin sonucunu belirlemek ve döndürmek için kullanılır.
 
 */