//Authored by: Jason Smith
//Purpose: Create methods for controlling department objects, GET, POST, and PUT methods  (not delete)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BangazonAPI.Data;
using BangazonAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Cors;

namespace BangazonAPI.Controllers
{
    // controller for "api/department" calls
    //Authored by: Jason Smith
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class DepartmentController : Controller
    {
        private BangazonContext _context;
        public DepartmentController(BangazonContext ctx)
        {
            _context = ctx;
        }

        // Get() accepts no arguments and returns a list of all departments
        //Authored by: Jason Smith
        [HttpGet]
        public IActionResult Get()
        {
            IQueryable<object> department = from dept in _context.Department select dept;

            if (department == null)
            {
                return NotFound();
            }
            return Ok(department);
        }

        // Get(id) accepts an id, which corresponds to the id of a department, returning the department specified.  The "Name" is a shortcut 
        // for calling the method later in the program, specifically for returning a valuable dataset on post and put methods
        //Authored by: Jason Smith
        [HttpGet("{id}", Name = "GetDepartment")]
        public IActionResult Get(int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Department department = _context.Department.Single(p => p.DepartmentId == id);
                if(department == null){ 
                    return NotFound();
                }
                return Ok(department);
            }
            catch (System.InvalidOperationException)
            {
                return NotFound();
            }
        }

        // Post method accepts a "Department" object formed from the body of the caller and adds it the database.  The argument forms a 
        // department object from the body of the request.  Returns callback of "GetDepartment" method named in the HttpGet from above
        //Authored by: Jason Smith
        [HttpPost]
        public IActionResult Post([FromBody] Department department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Department.Add(department);
            
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (DepartmentExists(department.DepartmentId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetDepartment", new { id = department.DepartmentId }, department);
        }

        // Checks whether a department with the given id exists in the Dbcontext.  Returns true if there are one or more, false if not
        //Authored by: Jason Smith
        public bool DepartmentExists(int id)
        {
            return _context.Department.Count(d => d.DepartmentId == id) > 0;
        }

        // Put method must have id in http request.  This method accepts that id as an argument as well as a Department object from the body.
        // Enters the department into the dbcontext then saves it to the db file
        // Returns callback of "GetDepartment" method named in the HttpGet, same as in Post method
        //Authored by: Jason Smith
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Department department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != department.DepartmentId)
            {
                return BadRequest();
            }

            _context.Entry(department).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentExists(id))
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
