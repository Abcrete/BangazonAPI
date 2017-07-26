using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BangazonAPI.Data;
using BangazonAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace BangazonAPI.Controllers
{
    [Route("api/[controller]")]
    public class DepartmentController : Controller
    {
        private BangazonContext _context;
        public DepartmentController(BangazonContext ctx)
        {
            _context = ctx;
        }
        public IActionResult Get()
        {
            IQueryable<object> department = from dept in _context.Department select dept;

            if (department == null)
            {
                return NotFound();
            }
            return Ok(department);
        }

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

        public bool DepartmentExists(int id)
        {
            return _context.Department.Count(d => d.DepartmentId == id) > 0;
        }

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
