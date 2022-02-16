#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;
using WebApi.Models.Entities;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly SqlDbContext _context;

        public CategoriesController(SqlDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryModel>>> GetCategoryEntity()
        {
            var list = new List<CategoryModel>();
            foreach (var category in await _context.Categories.ToListAsync())
                list.Add(new CategoryModel(category.Id, category.Name));

            return list;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductEntity(int id, UpdateCategoryModel model)
        {
            if (id != model.Id)
                return BadRequest();

            var categoryEntity = await _context.Categories.FindAsync(id);
            if (categoryEntity == null)
                return NotFound();

            categoryEntity.Name = model.Name;

            _context.Entry(categoryEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryEntityExists(id))
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

        [HttpPost]
        public async Task<ActionResult<CategoryEntity>> PostCategoryEntity(CreateCategoryModel model)
        {
            var categoryEntity = await _context.Categories.FirstOrDefaultAsync(x => x.Name == model.Name);
            if (categoryEntity != null)
                return new ConflictObjectResult(new CategoryModel(categoryEntity.Id, categoryEntity.Name));

            categoryEntity = new CategoryEntity(model.Name);
            _context.Categories.Add(categoryEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategoryEntity", new { id = categoryEntity.Id }, new CategoryModel(categoryEntity.Id, categoryEntity.Name));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryEntity(int id)
        {
            var categoryEntity = await _context.Categories.FindAsync(id);
            if (categoryEntity == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(categoryEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryEntityExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}