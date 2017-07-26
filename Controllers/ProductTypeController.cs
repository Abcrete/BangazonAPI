using System;
using System.Collections.Generic;
using System.Linq;
using BangazonAPI.Data;
using BangazonAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BangazonAPI.Controllers
{

    [Route("api/[controller]")]
    public class ProductTypeController : Controller
    {
        private BangazonContext _context;

        public ProductTypeController(BangazonContext ctx)
        {
            _context = ctx;
        }

        //Get api/values
        [HttpGet]
        public IActionResult Get()
        {
            IQueryable<object> productType = from prod in _context.ProductType select prod;

            if(!ModelState.IsValid)
            {
                return NotFound();
            }

            return Ok(productType);
        }

        //get api/values/5
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


        //Post api/values
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

            return CreatedAtRoute("GetProductType", new {id = productType.ProductTypeId}, productType);

        }

        private bool ProductTypeExists(int ProductTypeID)
        {
            return _context.ProductType.Count(p => p.ProductTypeId == ProductTypeID) > 0;
        }


        //PUT api/values/5
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


        // Delete api/values/2
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

            _context.ProductType.Remove(productType);
            _context.SaveChanges();

            return Ok(productType);
        }

    }

}