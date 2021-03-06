using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BangazonAPI.Models
{
  //SUMMARY
    /*This class is authored by Azim. 
      TrainingProgram Class identifies the properties for the entries to the TrainingProgram Tabel in database*/
  public class TrainingProgram
  {
    /*This is the primary key for an entry on the Customer table. It is generated by the database and doesn't
      need to be submitted by the user*/
    [Key]
    public int TrainingProgramId { get; set; }
    [Required]
    public string Name { get; set; }

    /*These two properties will be entered by the user and it has to be in correct format 2017-09-22*/
    [Required]
    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime EndDate { get; set; }

    // This property has to be entered by the user 
    [Required]
    public int MaxCapacity { get; set; }
  }
}