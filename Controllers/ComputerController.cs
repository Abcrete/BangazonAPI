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
    // <summary>
    // Route sets the path the server will be listening for
    // EnableCors sets the allowed sites that can request data using the methods established in this Controller
    [Route ("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class ComputerController : Controller
    {
        // <summary>
        // Property _context sets public constructer to make requests and set data
        private BangazonContext _context;

        public ComputerController (BangazonContext ctx) 
        {
            _context = ctx;
        }

        // <summary>
        // Overloaded Method that can return a complete Computer list or a single computer 
        [HttpGet]
        public IActionResult Get()
        {
            // <summary>
            // IQueryable allows a query against a specific data source where type of data is not specified
            // This also allows iteration
            // <values>
            // Returns an object for each instance
            IQueryable<object> computer = from comp in _context.Computer select comp;
            if (computer == null)
            {
                return NotFound();
            }

            return Ok(computer);

        }

        [HttpGet("{id}", Name = "GetComputer")]
        public IActionResult Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Computer computer = _context.Computer.Single(c => c.ComputerId == id);

                if (computer == null)
                {
                    return NotFound();
                }
                return Ok(computer);
            }
            catch(System.InvalidOperationException)
            {
                return NotFound();
            }
        }

        // <summary>
        // post api/value, requires all columns in table to be applied in action
        [HttpPost]
        public  IActionResult Post([FromBody] Computer computer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Console.WriteLine("Add computer");
            _context.Computer.Add(computer);
            // <summary>
            //If no error will save changes to DB
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if(ComputerExists(computer.ComputerId))
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
            return CreatedAtRoute("GetComputer", new {id = computer.ComputerId}, computer);
        }

        // <value>
        // returns True if ComputerId exists
        private bool ComputerExists(int ComputerID)
        {
            return _context.Computer.Count(c => c.ComputerId == ComputerID) > 0;
        }

        // <summary>
        // allows table values to be updated
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Computer computer)
        {

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(id != computer.ComputerId)
            {
                return BadRequest();
            }

            _context.Entry(computer).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch(DbUpdateConcurrencyException)
            {
                if(!ComputerExists(id))
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

        // <summary>
        // delete computer api/value/id, no value needed to pass in testing. Apply ComputerId in URI
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Computer computer = _context.Computer.Single(c => c.ComputerId == id);

            if (computer == null)
            {
                return NotFound();
            }
            // <value>
            // removes object from DB if no error occurs
            _context.Computer.Remove(computer);
            _context.SaveChanges();
            // <value>
            // returns Ok on success
            return Ok(computer);
        }
    }
}