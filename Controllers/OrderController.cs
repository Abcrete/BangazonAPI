using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangazonAPI.Data;
using BangazonAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BangazonAPI.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private BangazonContext _context;
        public OrderController(BangazonContext ctx)
        {
            _context = ctx;
        }
        // GET api/Order
        [HttpGet]
        public IActionResult Get()
        {
            IQueryable<object> orders = from item in _context.Order select item;

            if (orders == null)
            {
                return NotFound();
            }
            
            return Ok(orders);
            
        }

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
