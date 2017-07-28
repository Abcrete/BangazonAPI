// Authored by: Jason Smith
//Purpose: Create methods for controlling product objects, GET, POST, DELETE and PUT methods
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BangazonAPI.Data;
using BangazonAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace BangazonAPI.Controllers
{

    // controller for "api/product" calls
    // Authored by: Jason Smith
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private BangazonContext _context;
        public ProductController(BangazonContext ctx)
        {
            _context = ctx;
        }

        // Get() accepts no arguments and returns a list of all products
        // Authored by: Jason Smith
        [HttpGet]
        public IActionResult Get()
        {
            IQueryable<object> products = from product in _context.Product select product;

            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);
        }

        // Get(id) accepts an id, which corresponds to the id of a prodcut, returning the specific product.  The "Name" is a shortcut 
        // for calling the method later in the program, specifically for returning a valuable dataset on post and put methods
        // Authored by: Jason Smith
        [HttpGet("{id}", Name = "GetProduct")]
        public IActionResult Get(int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Product product = _context.Product.Single(p => p.ProductId == id);
                if(product == null){ 
                    return NotFound();
                }
                return Ok(product);
            }
            catch (System.InvalidOperationException)
            {
                return NotFound();
            }
        }

        // Post method accepts a "Product" object formed from the body of the caller and adds it the database.  The argument forms a 
        // product object from the body of the request.  Returns callback of "GetProduct" method named in the HttpGet from above
        // Authored by: Jason Smith
        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Product.Add(product);
            
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ProductExists(product.ProductId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetProduct", new { id = product.ProductId }, product);
        }

        // Checks whether a product with the given id exists in the Dbcontext.  Returns true if there are one or more, false if not
        // Authored by: Jason Smith
        public bool ProductExists(int id)
        {
            return _context.Product.Count(e => e.ProductId == id) > 0;
        }

        // Put method must have id in http request.  This method accepts that id as an argument as well as a Product object from the body.
        // Enters the product into the dbcontext then saves it to the db file
        // Returns callback of "GetProduct" method named in the HttpGet, same as in Post method
        // Authored by: Jason Smith
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.ProductId)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
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

            return new StatusCodeResult(StatusCodes.Status204NoContent);
        }

        // Put method must have id in http request.  This method accepts that id as an argument as well as a Product object from the body.
        // Removes the department from the dbcontext then saves it to the db file
        // Authored by: Jason Smith
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Product product = _context.Product.Single(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Product.Remove(product);
            _context.SaveChanges();

            return Ok(product);
        }
    }
}
