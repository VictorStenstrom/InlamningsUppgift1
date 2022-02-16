#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;
using WebApi.Models.Entities;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly SqlDbContext _context;

        public ProductsController(SqlDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductModel>>> GetProducts()
        {
            var list = new List<ProductModel>();
            foreach (var product in await _context.Products.Include(x => x.Category).ToListAsync())
                list.Add(new ProductModel(product.Id, product.Name, product.Description, product.Price, product.Category.Name, product.CategoryId));

            return list;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<SortByCategoryModel>>> GetProductEntity(int id)
        {
            var list = new List<SortByCategoryModel>();
            foreach (var product in await _context.Products.ToListAsync())
                if (product.CategoryId == id)
                {
                    list.Add(new SortByCategoryModel(product.Name, product.Description, product.Price));
                }

            if (list.Count < 1)
                return NotFound();

            return list;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductEntity(int id, UpdateProductModel model)
        {
            if (id != model.Id)
                return BadRequest();

            var productEntity = await _context.Products.FindAsync(id);
            if (productEntity == null)
                return NotFound();

            productEntity.Name = model.Name;
            productEntity.Description = model.Description;
            productEntity.Price = model.Price;
            productEntity.CategoryId = model.CategoryId;

            _context.Entry(productEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductEntityExists(id))
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
        public async Task<ActionResult<ProductEntity>> PostProductEntity(CreateProductModel model)
        {
            var productEntity = await _context.Products.Include(x => x.Category).FirstOrDefaultAsync(x => x.Name == model.Name);
            if (productEntity != null)
                return new ConflictObjectResult(new ProductModel(productEntity.Id, productEntity.Name, productEntity.Description, productEntity.Price, productEntity.Category.Name));

            var category = await _context.Categories.FindAsync(model.CategoryId);
            if (category == null)
                return new BadRequestObjectResult(new ErrorMessage { StatusCode = 400, Error = "Invalid or no category id provided." });

            productEntity = new ProductEntity(model.Name, model.Description, model.Price, category.Id);
            _context.Products.Add(productEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductEntity", new { id = productEntity.Id }, new ProductModel(productEntity.Id, productEntity.Name, productEntity.Description, productEntity.Price, productEntity.Category.Name));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductEntity(int id)
        {
            var productEntity = await _context.Products.FindAsync(id);
            if (productEntity == null)
            {
                return NotFound();
            }

            _context.Products.Remove(productEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductEntityExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}