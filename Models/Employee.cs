using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BangazonAPI.Models
{

  // <summary>
  // Creates instance of Employee class with properties to set name, FK-DepartmentId from Department Table
  // All Properties are Required
  public class Employee
  {

    // <summary>
    // Key will auto-increment new entries for EmployeeId, EmployeeId allows get and set of the ID
    [Key]
    public int EmployeeId { get; set; }
    
    [Required]
    public string FirstName {get; set;}
    [Required]
    public string LastName {get; set;}


    // <summary>
    // FK-DepartmentId from Department Table, instance of the Class must be create
    [Required]
    public int DepartmentId {get; set;}
    public Department Department {get; set;}

    // <summary>
    // SqLite will not accept Bool int Value, Using Sql format of 0 and 1 to establish true or false
    // 0 = False
    // 1 = True
    [Required]
    public int IsSupervisor {get; set;}


    // public ICollection<EmployeeComputer> EmployeeComputer;

    // public ICollection<EmployeeTraining> EmployeeTraining;

  }
}