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
    public class PaymentTypeController : Controller
    {
        private BangazonContext _context;
        public PaymentTypeController(BangazonContext ctx)
        {
            _context = ctx;
        }

        private bool PaymentTypeExists(int paymentTypeId)
        {
          return _context.PaymentType.Count(e => e.PaymentTypeId == paymentTypeId) > 0;
        }

        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            IQueryable<object> paymentTypes = from payment in _context.PaymentType select payment;

            if (paymentTypes == null)
            {
                return NotFound();
            }

            return Ok(paymentTypes);

        }

        // GET api/values/5
        [HttpGet("{id}", Name = "GetPaymentType")]
        public IActionResult Get([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                PaymentType money = _context.PaymentType.Single(m => m.PaymentTypeId == id);

                if (money == null)
                {
                    return NotFound();
                }
                
                return Ok(money);
            }
            catch (System.InvalidOperationException ex)
            {
                return NotFound();
            }
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] PaymentType money)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.PaymentType.Add(money);
            
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PaymentTypeExists(money.PaymentTypeId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetPaymentType", new { id = money.PaymentTypeId }, money);
        }

        

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] PaymentType money)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != money.PaymentTypeId)
            {
                return BadRequest();
            }

            _context.Entry(money).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentTypeExists(id))
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

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PaymentType money = _context.PaymentType.Single(m => m.PaymentTypeId == id);
            if (money == null)
            {
                return NotFound();
            }

            _context.PaymentType.Remove(money);
            _context.SaveChanges();

            return Ok(money);
        }
        
    }
}