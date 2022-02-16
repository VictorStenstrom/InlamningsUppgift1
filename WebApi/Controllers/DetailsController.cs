using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetailsController : Controller
    {
        private readonly SqlDbContext _context;

        public DetailsController(SqlDbContext context)
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
        public async Task<ActionResult<IEnumerable<ProductModel>>> Details(int id)
        {
            var list = new List<ProductModel>();
            foreach (var product in await _context.Products.Include(x => x.Category).ToListAsync())
                if (product.Id == id)
                {
                    list.Add(new ProductModel(product.Id, product.Name, product.Description, product.Price, product.Category.Name, product.CategoryId));
                }

            if (list.Count < 1)
                return NotFound();

            return list;
        }
    }
}