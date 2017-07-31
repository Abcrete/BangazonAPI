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
    // This class was authored by Azim
    public class TrainingProgramController : Controller
    {
        private BangazonContext _context;
        public TrainingProgramController(BangazonContext ctx)
        {
            _context = ctx;
        }
         /*This method is a GET request which takes zero arguments and returns all trainingprogram
        in TrainingProgram Table of database.*/
        //This method is authored by Azim.
        // GET api/trainingprogram
        [HttpGet]
        public IActionResult Get()
        {
            IQueryable<object> trainProgs = from trainingprogram in _context.TrainingProgram select trainingprogram;

            if (trainProgs == null)
            {
                return NotFound();
            }
            
            return Ok(trainProgs);
            
        }
        /*This method is a GET request which takes id as a TPId and returns requested trainingprogram
        in TrainingProgram Table of database.*/
        //This method is authored by Azim.
        // GET api/trainingprogram/2
        [HttpGet("{id}", Name = "GetTrainingProgram")]
        public IActionResult Get([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                TrainingProgram trainingProgram = _context.TrainingProgram.Single(m => m.TrainingProgramId == id);

                if (trainingProgram == null)
                {
                    return NotFound();
                }
                
                return Ok(trainingProgram);
            }
            catch (System.InvalidOperationException)
            {
                return NotFound();
            }
        }
        /*This is a POST request which creates and adds a trainingProgram to the TrainingProgram Table from the arguments passed in.*/
        //This method is authored by Azim.
        // POST api/trainingprogram
        [HttpPost]
        public IActionResult Post([FromBody] TrainingProgram trainProgs)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.TrainingProgram.Add(trainProgs);
            
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (TrainingProgramExists(trainProgs.TrainingProgramId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetTrainingProgram", new { id = trainProgs.TrainingProgramId }, trainProgs);
        }

        private bool TrainingProgramExists(int trainingProgramId)
        {
            return _context.TrainingProgram.Count(e => e.TrainingProgramId == trainingProgramId) > 0;
        }
        /*This is a PUT request which modifies a trainingProgram in the TrainingProgram Table from the arguments passed in.*/
        //This method is authored by Azim.
        // PUT api/trainingprogram/2
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] TrainingProgram trainProgs)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != trainProgs.TrainingProgramId)
            {
                return BadRequest();
            }

            _context.Entry(trainProgs).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (TrainingProgramExists(trainProgs.TrainingProgramId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return new StatusCodeResult(StatusCodes.Status204NoContent);
        }
        /*This is a DELETE request which deletes a trainingProgram in the TrainingProgram Table from the id it passed in.*/
        //This method is authored by Azim. 
        // DELETE api/trainingprogram/2
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TrainingProgram trainProgs = _context.TrainingProgram.Single(m => m.TrainingProgramId == id);
            if (trainProgs == null)
            {
                return NotFound();
            }

            //Getting local time and compare the data in the database and it will delete if it has not started yet
            DateTime localDate = DateTime.Now;
            int result = DateTime.Compare(localDate, trainProgs.StartDate);

            if (result < 0)
            {
                _context.TrainingProgram.Remove(trainProgs);     
            }
            else
            {
                return BadRequest(ModelState);
            }
            _context.SaveChanges();

            return Ok(trainProgs);
        }
    }
}
