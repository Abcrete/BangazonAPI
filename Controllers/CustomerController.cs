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
    public class CustomerController : Controller
    {
        private BangazonContext _context;
        public CustomerController(BangazonContext ctx)
        {
            _context = ctx;
        }

        /*This internal method checks for the existence of a customer based on the customerId argument.
        This method was authored by Jordan Dhaenens */        
        private bool CustomerExists(int customerId)
        {
          return _context.Customer.Count(e => e.CustomerId == customerId) > 0;
        }

        /*This method is a GET request which takes zero arguments and returns all customers
        from Customer Table of database.
        This method was authored by Jordan Dhaenens*/
        // GET path~ api/customer
        [HttpGet]
        public IActionResult Get()
        {
            IQueryable<object> customers = _context.Customer;
            // IQueryable<object> customers = from person in _context.Customer select person;

            if (customers == null)
            {
                return NotFound();
            }

            return Ok(customers);

        }

        /*This overload method is a GET request which takes one argument, "id", and returns a single customer
        from Customer Table.
        Argument: this is the CustomerId of the customer you wish to search for.
        This method was authored by Jordan Dhaenens*/
        // GET path~ api/customer/5
        [HttpGet("{id}", Name = "GetCustomer")]
        public IActionResult Get([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Customer person = _context.Customer.Single(m => m.CustomerId == id);

                if (person == null)
                {
                    return NotFound();
                }
                
                return Ok(person);
            }
            catch (System.InvalidOperationException)
            {
                return NotFound();
            }
        }

        /*This is a POST request which creates and adds a customer to the Customer Table from the arguments passed in.
        --ARGUMENTS--
        "FirstName": "Jane" --This sets the FirstName property of the customer instance--
        "LastName": "Doe",  --This sets the LastName property of the customer instance--
        "IsActive": 0 or 1  --This sets the IsActive property of the customer instance. 0 represents false--

        This method was authored by Jordan Dhaenens
        */
        //POST path~ api/values
        [HttpPost]
        public IActionResult Post([FromBody] Customer person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Customer.Add(person);
            
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CustomerExists(person.CustomerId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetCustomer", new { id = person.CustomerId }, person);
        }

        
        /*This is a PUT request which amends a customer on the Customer Table from the arguments passed in. You 
        cannot modify the value of the following properties unless otherwise noted:
        --ARGUMENTS--
            {integer} this is the CustomerId of the customer you wish to search for. 
        --Body--
        "products": null, --This should not be modified here--
        "orders": null,  --This should not be modified here--
        "paymentTypes": null,  --This should not be modified here--
        "customerId": 2,  --This should not be modified EVER--
        "FirstName": "Jane" --This value can be modified--
        "LastName": "Doe",  --This value can be modified--
        "acctCreatedOn": "2017-07-26T15:58:00",  --This should not be modified here--
        "lastLogin": "2017-07-26T15:58:00",  --This should not be modified here--
        "IsActive": 0 or 1  --This value can be modified--
        */
        //This method was authored by Jordan Dhaenens
        //PUT path~ api/customer/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Customer person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != person.CustomerId)
            {
                return BadRequest();
            }

            _context.Entry(person).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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
        
    }
}