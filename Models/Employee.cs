using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BangazonAPI.Models
{
  public class Employee
  {
    [Key]
    public int EmployeeId { get; set; }
    
    [Required]
    public string FirstName {get; set;}
    [Required]
    public string LastName {get; set;}

    [Required]
    public int DepartmentId {get; set;}
    public Department Department {get; set;}


    [Required]
    public int IsSupervisor {get; set;}

    // public Employee()
    // {
    //   IsSupervisor = 0;
    // }

    // public ICollection<EmployeeComputer> EmployeeComputer;

    // public ICollection<EmployeeTraining> EmployeeTraining;

  }
}