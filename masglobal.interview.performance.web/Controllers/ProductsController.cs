using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using masglobal.interview.performance.web.Models;

namespace masglobal.interview.performance.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly InterviewContext _context;

        public ProductsController(InterviewContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
        {
            return await _context.Products.ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FullProduct>> GetProduct(Guid id)
        {
            var product = await GetFullProducts(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(Guid id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return product;
        }

        private bool ProductExists(Guid id)
        {
            return _context.Products.Any(e => e.Id == id);
        }



        private async Task<FullProduct> GetFullProducts(Guid productIGuid)
        {
            Product p = await _context.Products
                .Include(c => c.ProductCategories)
                .ThenInclude(e => e.Category)
                .FirstAsync(c => c.Id == productIGuid);

            return new FullProduct
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Categories = p.ProductCategories.Select(c => new Category { Id = c.CategoryId, Name = c.Category.Name }).ToList()
            };
        }

        public class FullProduct
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }

            public List<Category> Categories { get; set; }
        }
    }




}
