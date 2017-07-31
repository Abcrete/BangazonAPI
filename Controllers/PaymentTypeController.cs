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
    public class PaymentTypeController : Controller
    {
        private BangazonContext _context;
        public PaymentTypeController(BangazonContext ctx)
        {
            _context = ctx;
        }

        /*This internal method checks for the existence of a paymentType based on the paymentTypeId argument.
        This method was authored by Jordan Dhaenens */
        private bool PaymentTypeExists(int paymentTypeId)
        {
          return _context.PaymentType.Count(e => e.PaymentTypeId == paymentTypeId) > 0;
        }

        /*This method is a GET request which takes zero arguments and returns all paymentTypes
        in PaymentType Table of database.
        This method was authored by Jordan Dhaenens*/
        // GET path~ api/paymenttype
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

        /*This overload method is a GET request which takes one argument, "id", and returns a single paymentType
        from PaymentType Table.
        Argument: this is the PaymentTypeId of the paymentType you wish to search for.
        This method was authored by Jordan Dhaenens*/
        // GET path~ api/paymenttype/5
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
            catch (System.InvalidOperationException)
            {
                return NotFound();
            }
        }


        /*This is a POST request which creates and adds a paymentType to the PaymentType Table from the arguments passed in.
        --ARGUMENTS--
        "AccountNumber": 123456789 --This sets the AccountNumber property of the paymentType instance--
        "Type": "Visa",  --This sets the Type property of the paymentType instance--
        "CustomerId": 1 --This sets the CustomerId property of the paymentType instance--

        This method was authored by Jordan Dhaenens
        */
        //POST path~ api/paymenttype
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

        

        /*This is a PUT request which amends a paymentType on the PaymentType Table from the arguments passed in. You 
        cannot modify the value of the following properties unless otherwise noted:
        --ARGUMENTS--
            {integer} this is the PaymentTypeId of the paymentType you wish to search for. 
        --Body--
        "orders": null,  --This should not be modified here--
        "paymentTypeId": 2,  --This should not be modified EVER--
        "accountNumber": 1234567899,  --This value can be modified--
        "type": "MasterCard",  --This value can be modified--
        "customerId": 2,  --This should not be modified EVER--
        "customer": null  --This should not be modified EVER--
        */
        //This method was authored by Jordan Dhaenens
        //PUT path~ api/paymenttype/5
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


        /*This is a DELETE request which accepts one argument and completely removes the selected instance of
        paymentType from the PaymentType Table. 
        --Argument--
            {integer} this is the PaymentTypeId of the paymentType you wish to search for.
        */
        // DELETE api/paymenttype/5
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
