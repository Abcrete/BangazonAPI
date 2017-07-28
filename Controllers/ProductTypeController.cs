using System;
using System.Collections.Generic;
using System.Linq;
using BangazonAPI.Data;
using BangazonAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BangazonAPI.Controllers
{

    // Route sets the path the server will be listening for
    // EnableCors sets the allowed sites that can request data using the methods established in this Controller
    // Authored by: Tamela Lerma
    [Route ("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class ProductTypeController : Controller
    {
        // Internal property _context sets public constructer to make requests and set data
        // Authored by: Tamela Lerma
        private BangazonContext _context;

        public ProductTypeController(BangazonContext ctx)
        {
            _context = ctx;
        }

        // Method that can return all Employee objects. This Method does not take any Arguements
        // Path is api/productType
        // Authored by: Tamela Lerma
        [HttpGet]
        public IActionResult Get()
        {
            // IQueryable allows a query against a specific data source where type of data is not specified
            // This also allows iteration
            // <values>
            // Returns an object for each instance
            // Authored by: Tamela Lerma
            IQueryable<object> productType = from prod in _context.ProductType select prod;

            if(!ModelState.IsValid)
            {
                return NotFound();
            }

            return Ok(productType);
        }

        // Over-Loaded Method that can return a single ProductType Object.
        // Arguments : This Method takes 1 argument of ProductTypeId
        // Path is api/productType
        // Authored by: Tamela Lerma
        [HttpGet("{id}", Name = "GetProductType")]
        public IActionResult Get([FromRoute] int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try 
            {
                ProductType productType = _context.ProductType.Single(p => p.ProductTypeId == id);
                if(productType == null)
                {
                    return NotFound();
                }
                return Ok(productType);
            }
            catch(System.InvalidOperationException)
            {
                return NotFound();
            }
        }


        // Post path is api/productType No FK
        // ProductTypeId is Auto-Generated, value does not have to be applied when creating
        // Required-
        // Name : string that sets name of ProductType;
        // Authored by: Tamela Lerma 
        [HttpPost]
        public IActionResult Post([FromBody] ProductType productType)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ProductType.Add(productType);

            try
            {
                //If no error will save changes to DB
                // Authored by: Tamela Lerma
                _context.SaveChanges();
            }
            catch(DbUpdateException)
            {
                if(ProductTypeExists(productType.ProductTypeId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            // <value>
            // returns new object instance
            // Authored by: Tamela Lerma
            return CreatedAtRoute("GetProductType", new {id = productType.ProductTypeId}, productType);

        }

        // internal Method to check if ProductTypeId exists, 
        // if Count is greater than 0 returns True
        // ensures duplicates are not made and correct error is given
        // Authored by: Tamela Lerma
        private bool ProductTypeExists(int ProductTypeID)
        {
            return _context.ProductType.Count(p => p.ProductTypeId == ProductTypeID) > 0;
        }


        // Put Method,  path is api/productType
        // Method requires all columns in table
        // allows ProductType table values to be updated
        // Required-
        // ProductTypeId : string to set Name value of ProductType
        // Authored by: Tamela Lerma
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ProductType productType)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(id != productType.ProductTypeId)
            {
                return BadRequest();
            }

            _context.Entry(productType).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if(!ProductTypeExists(id))
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


        // Delete ProductType Method api/productType/id. Apply ComputerId in URI
        // Arguments - accepts ProductTypeId as Argument
        // Authored by: Tamela Lerma
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ProductType productType = _context.ProductType.Single(p => p.ProductTypeId == id);
            if(productType == null)
            {
                return NotFound();
            }

            // removes object from DB if no error occurs
            // Authored by: Tamela Lerma
            _context.ProductType.Remove(productType);
            _context.SaveChanges();

            // returns Ok on success
            return Ok(productType);
        }

    }

}