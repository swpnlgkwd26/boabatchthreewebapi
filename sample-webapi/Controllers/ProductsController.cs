using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sample_webapi.Filters;
using sample_webapi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sample_webapi.Controllers
{
    // Async : Can have return type .. void, Task, Task<Type>
    // CRUD
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    [AddHeader("BOA","ProductInformation")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductDBContext _context;

        public ProductsController(ProductDBContext context)
        {
            _context = context;
        }
        //  Get: all Products
        [HttpGet]
        [Produces("application/json")]
        public IAsyncEnumerable<Product> GetProducts()
        {
            return _context.Products;
        }

        // Get: Products By ID
        [HttpGet("{id}")]

        [Produces("application/json", "application/xml")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(new { ProductName = product.Name, Price = product.Price });
        }

        // POST :Add New Product
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [CustomActionFilter]
        public async Task<IActionResult> SaveProduct([FromBody] ProductBindigTarget target)
        {

            await _context.Products.AddAsync(target.ToProduct());
            await _context.SaveChangesAsync();
            return Ok();
        }
        // PUT :Update Product
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [CustomActionFilter]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // DELETE :  Product BY ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            _context.Products.Remove(new Product { ProductID = id });
            await _context.SaveChangesAsync();
            return Ok();
        }

        


    }
}
