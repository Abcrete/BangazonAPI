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
    // Route sets the path the server will be listening for
    // EnableCors sets the allowed sites that can request data using the methods established in this Controller
    // Authored by: Tamela Lerma
    [Route ("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class ComputerController : Controller
    {
        // Private Property _context sets public constructer to make requests and set data
        // Authored by: Tamela Lerma
        private BangazonContext _context;

        public ComputerController (BangazonContext ctx) 
        {
            _context = ctx;
        }

        // Method that can return all Computer objects. This Method does not take any Arguements
        // Authored by: Tamela Lerma
        [HttpGet]
        public IActionResult Get()
        {
            // <summary>
            // IQueryable allows a query against a specific data source where type of data is not specified
            // This also allows iteration
            // <values>
            // Returns an object for each instance
            // Authored by: Tamela Lerma
            IQueryable<object> computer = from comp in _context.Computer select comp;
            if (computer == null)
            {
                return NotFound();
            }

            return Ok(computer);

        }

        // Over-Loaded Method that can return a single Computer Object.
        // Arguments : This Method takes 1 argument of ComputerId
        // Authored by: Tamela Lerma
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

        // Post path is api/computer, Method requires all columns in table
        // ComputerId is Auto-Generated, value does not have to be applied when creating
        // Required-
        // DateDecomissioned : accepts standard IOS Date and Time Format, ex. "yyyy-MM-dd 'at' HH:mm"
        // DatePurchased : accepts standard IOS Date and Time Format, ex. "yyyy-MM-dd 'at' HH:mm"
        // Authored by: Tamela Lerma
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
            // Authored by: Tamela Lerma
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
            // Authored by: Tamela Lerma
            return CreatedAtRoute("GetComputer", new {id = computer.ComputerId}, computer);
        }

        // internal Method to check if ComputerId exists, 
        // if Count is greater than 0 returns True
        // ensures duplicates are not made and correct error is given
        // Authored by: Tamela Lerma
        private bool ComputerExists(int ComputerID)
        {
            return _context.Computer.Count(c => c.ComputerId == ComputerID) > 0;
        }

        // Put Method,  path is api/computer
        // Method requires all columns in table
        // allows table values to be updated
        // Required -
        // ComputerId : Must be ID in Table
        // DateDecomissioned : must be in "yyyy-MM-dd 'at' HH:mm" format
        // DatePurchased : mmust be in "yyyy-MM-dd 'at' HH:mm" format
        // Arguments-
        // Excepts ComputerId as an Arguments
        // Authored by: Tamela Lerma
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
                // if ModelState is Valid and if Computer does exist
                // chnges will be saved to DB
                // Authored by: Tamela Lerma
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
            // returns Status of 204 No Content after successful Update
            // Authored by: Tamela Lerma
            return new StatusCodeResult(StatusCodes.Status204NoContent);
        }

        // Delete computer Method api/computer/id, no value needed to pass in testing. Apply ComputerId in URI
        // Arguments - accepts ComputerId as Argument
        // Authored by: Tamela Lerma
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
            // removes object from DB if no error occurs
            // Authored by: Tamela Lerma
            _context.Computer.Remove(computer);
            _context.SaveChanges();
            // returns Ok on success
            return Ok(computer);
        }
    }
}