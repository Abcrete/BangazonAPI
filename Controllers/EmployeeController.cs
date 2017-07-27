using System;
using System.Collections.Generic;
using System.Linq;
using BangazonAPI.Data;
using BangazonAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;

namespace BangazonAPI.Controllers
{

    // Route sets the path the server will be listening for
    // EnableCors sets the allowed sites that can request data using the methods established in this Controller
    // Authored by: Tamela Lerma
    [Route ("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]

    public class EmployeeController : Controller
    {
        // Private Property _context sets public constructer to make requests and set data
        // Authored by: Tamela Lerma
        private BangazonContext _context;

        public EmployeeController(BangazonContext ctx)
        {
            _context = ctx;
        }

        // Method that can return all Employee objects. This Method does not take any Arguements
        // Authored by: Tamela Lerma
        [HttpGet]
        public IActionResult Get()
        {
            // IQueryable allows a query against a specific data source where type of data is not specified
            // This also allows iteration
            // <values>
            // Returns an object for each instance
            // Authored by: Tamela Lerma
            IQueryable<object> employees = from person in _context.Employee select person;

            if (employees == null) {
                return NotFound();
            }

            return Ok(employees);
        }

        // Over-Loaded Method that can return a single Employee Object.
        // Arguments : This Method takes 1 argument of EmployeeId
        // Authored by: Tamela Lerma
        [HttpGet("{id}", Name = "GetEmployee")]
        public IActionResult Get(int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Employee employee = _context.Employee.Single(p => p.EmployeeId == id);

                if (employee == null)
                {
                    return NotFound();
                }
                return Ok(employee);
            }
            catch (System.InvalidOperationException)
            {
                return NotFound();
            }
        }

        // Post path is api/employee
        // EmployeeId is Auto-Generated, value does not have to be applied when creating
        // Required-
        // DepartmentId : FK-DepartmentId from Department Table
        // FirstName : string that sets Employee's First Name
        // LastName : string that sets Employee's Last Name
        // IsSupervisor: int to set True or False, 0= False, 0=True;
        // Authored by: Tamela Lerma        
        public IActionResult Post([FromBody] Employee employee)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }
            Console.WriteLine("Add employee");
            _context.Employee.Add(employee);

            try
            {
                //If no error will save changes to DB
                // Authored by: Tamela Lerma
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (EmployeeExists(employee.EmployeeId))
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
            return CreatedAtRoute("GetEmployee", new {id = employee.EmployeeId}, employee);
        } 

        // internal Method to check if EmployeeId exists, 
        // if Count is greater than 0 returns True
        // ensures duplicates are not made and correct error is given
        // Authored by: Tamela Lerma
        private bool EmployeeExists(int EmployeeID)
        {
            return _context.Employee.Count(e => e.EmployeeId == EmployeeID) > 0;
        }



        // Put Method,  path is api/employee
        // Method requires all columns in table- Joined Tables Not yet written
        // allows Employee table values to be updated
        // Required-
        // DepartmentId : FK-DepartmentId from Department Table, must be existing Table
        // FirstName : string that sets Employee's First Name
        // LastName : string that sets Employee's Last Name
        // IsSupervisor: int to set True or False, 0= False, 0=True;
        // Authored by: Tamela Lerma 
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            if (id != employee.EmployeeId)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
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


        // Delete not yet added to Sprint may be added at later date
        // Authored by: Tamela Lerma
        //Delete api/employee/id
        // [HttpDelete("{id}")]
        // public IActionResult Delete(int id)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         return BadRequest(ModelState);
        //     }

        //     Employee employee = _context.Employee.Single(p => p.EmployeeId == id);
        //     if (employee == null)
        //     {
        //         return NotFound();
        //     }

        //     _context.Employee.Remove(employee);
        //     _context.SaveChanges();

        //     return Ok(employee);

        // }
    }
}
