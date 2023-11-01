using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YemekDünyasi.Models;

namespace YemekDünyasi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly YemekDünyasContext _dbContext;

        public CategoryController(YemekDünyasContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]

        public async Task<ActionResult<IEnumerable<Category>>> GetKategoriler()
        {
            if (_dbContext.UsersTable == null)
            {
                return NotFound();
            }
            return await _dbContext.CategoryTable.ToListAsync();

        }

        [HttpGet("{id}")]

        public async Task<ActionResult<Category>> GetKategori(int id)
        {
            if (_dbContext.CategoryTable== null)
            {
                return NotFound();
            }
            var kategori = await _dbContext.CategoryTable.FindAsync(id);
            if (kategori == null)
            {
                return NotFound();
            }
            return kategori;

        }

        [HttpPost]
        public async Task<ActionResult<Category>> PostUser(string name, string kategoriResimUrl)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Required fields are missing.");
            }

            Category kategry = new Category
            {
                Name = name,
                KategoriResimUrl = kategoriResimUrl
                

            };

            _dbContext.CategoryTable.Add(kategry);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetKategori), new { id = kategry.Id }, kategry);
        }

        private bool CategoryExists(int id)
        {
            return _dbContext.CategoryTable.Any(x => x.Id == id);
        }

        [HttpPut("{id}")] // UPDATE
        public async Task<IActionResult> PutKategori(int id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }
             var existingKategori= await _dbContext.CategoryTable.FindAsync(id);

            if(existingKategori == null)
            {
                return NotFound();
            }

            existingKategori.Name = category.Name;
            existingKategori.KategoriResimUrl=category.KategoriResimUrl;
           
            try
            {
                _dbContext.CategoryTable.Update(existingKategori);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
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

        public async Task<IActionResult> DeleteKategori(int id)
        {
            if (_dbContext.CategoryTable == null)
            {
                return NotFound();
            }
            var kategori = await _dbContext.CategoryTable.FindAsync(id);
            if (kategori == null)
            {
                return NotFound();
            }
            _dbContext.Remove(kategori);

            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
