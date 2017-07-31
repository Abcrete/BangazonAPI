using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BangazonAPI.Models
{
    //SUMMARY
    /*This class is authored by Azim. 
    EmployeeTraining Class is connection between Employee and TrainingProgram*/
  public class EmployeeTraining
  {
    // This property will be created by the database and it should be unique
    // This class is authored by Azim.
    [Key]
    public int EmployeeTrainingId { get; set; }

    // This property is a reference to the employee table in the database
    // This class is authored by Azim.
    [Required]
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; }

    // This property is a reference to the TrainingProgram table in the database
    // This class is authored by Azim.
    [Required]
    public int TrainingProgramId { get; set; }
    public TrainingProgram TrainingProgram { get; set; }
  }
}