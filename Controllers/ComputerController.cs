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
    [Route ("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class ComputerController : Controller
    {
        private BangazonContext _context;

        public ComputerController (BangazonContext ctx) 
        {
            _context = ctx;
        }

        //return Computers
        [HttpGet]
        public IActionResult Get()
        {
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

        //post api/value
        [HttpPost]
        public  IActionResult Post([FromBody] Computer computer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Console.WriteLine("Add computer");
            _context.Computer.Add(computer);

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

            return CreatedAtRoute("GetComputer", new {id = computer.ComputerId}, computer);
        }

        private bool ComputerExists(int ComputerID)
        {
            return _context.Computer.Count(c => c.ComputerId == ComputerID) > 0;
        }


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


        //delete computer api/value/id
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

            _context.Computer.Remove(computer);
            _context.SaveChanges();

            return Ok(computer);
        }
    }
}