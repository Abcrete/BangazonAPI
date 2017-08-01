using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangazonAPI.Data;
using BangazonAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BangazonAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    // This class was authored by Azim
    public class OrderController : Controller
    {
        private BangazonContext _context;
        public OrderController(BangazonContext ctx)
        {
            _context = ctx;
        }
         /*This method is a GET request which takes zero arguments and returns all orders
        in Order Table of database.*/
        //This method is authored by Azim.
        // GET api/Order
        [HttpGet]
        public IActionResult Get()
        {
            IQueryable<object> orders = from order in _context.Order.Include("OrderProducts.Product") select order;

            if (orders == null)
            {
                return NotFound();
            }
            
            return Ok(orders);
            
        }
         /*This method is a GET request which takes id as a OrderId and returns requested order
        in Order Table of database.*/
        //This method is authored by Azim.
        // GET api/Order/2
        [HttpGet("{id}", Name = "GetOrder")]
        public IActionResult Get([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Order orders = _context.Order.Single(m => m.OrderId == id);

                if (orders == null)
                {
                    return NotFound();
                }
                
                return Ok(orders);
            }
            catch (System.InvalidOperationException)
            {
                return NotFound();
            }
        }
        /*This is a POST request which creates and adds a order to the Order Table from the arguments passed in.*/
        //This method is authored by Azim.
        // POST api/Order
        [HttpPost]
        public IActionResult Post([FromBody] Order orders)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Order.Add(orders);
            
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (OrderExists(orders.OrderId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetOrder", new { id = orders.OrderId }, orders);
        }

        private bool OrderExists(int orderId)///
        {
            return _context.Order.Count(e => e.OrderId == orderId) > 0; ///
        }


        /*This is a PUT request which modifies a order in the Order Table from the arguments passed in.*/
        //This method is authored by Azim.
        // PUT api/Order/2
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Order orders)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != orders.OrderId)
            {
                return BadRequest();
            }

            _context.Entry(orders).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (OrderExists(orders.OrderId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return new StatusCodeResult(StatusCodes.Status204NoContent);
        }
        /*This is a DELETE request which deletes a order in the Order Table from the id it passed in.*/
        //This method is authored by Azim.
        // DELETE api/Order/2
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Order orders  = _context.Order.Single(m => m.OrderId == id);
            if (orders == null)
            {
                return NotFound();
            }

            _context.Order.Remove(orders);
            _context.SaveChanges();

            return Ok(orders);
        }
    }
}
