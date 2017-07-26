using System;
using System.Collections.Generic;
using System.Linq;
using BangazonAPI.Data;
using BangazonAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BangazonAPI.Controllers
{

    [Route ("api/[controller]")]
    public class EmployeeController : Controller
    {

        private BangazonContext _context;

        public EmployeeController(BangazonContext ctx)
        {
            _context = ctx;
        }

        //Returns collection of employees
        [HttpGet]
        public IActionResult Get()
        {
            IQueryable<object> employees = from person in _context.Employee select person;

            if (employees == null) {
                return NotFound();
            }

            return Ok(employees);
        }

        // GET api/values/id
        // Returns single Employee
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

        // api/values
        [HttpPost]
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

            return CreatedAtRoute("GetEmployee", new {id = employee.EmployeeId}, employee);
        } 

        private bool EmployeeExists(int EmployeeID)
        {
            return _context.Employee.Count(e => e.EmployeeId == EmployeeID) > 0;
        }

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

        //Delete api/employee/id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Employee employee = _context.Employee.Single(p => p.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employee.Remove(employee);
            _context.SaveChanges();

            return Ok(employee);

        }
    }
}
