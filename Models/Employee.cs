using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BangazonAPI.Models
{

  // Use Models in namespace for reference to Employee.cs location
  // Authored by: Tamela Lerma

  public class Employee
  {
     // Key will auto-increment new entries for EmployeeId, PK
    // Authored by: Tamela Lerma
    [Key]
    public int EmployeeId { get; set; }
    
    // Get and Set Employee First Name
    // Authored by: Tamela Lerma
    [Required]
    public string FirstName {get; set;}

    // Get and Set Employee Last Name 
    // Authored by: Tamela Lerma
    [Required]
    public string LastName {get; set;}


    // Get and Set Employee DepartmentId
    // FK-DepartmentId from Department Table
    // Must create an instance of the Class Department to retrieve FK 
    // Authored by: Tamela Lerma
    [Required]
    public int DepartmentId {get; set;}
    public Department Department {get; set;}

    // Get and set IsSupervisor 0 = False, 1 = True,
    // SqLite will not accept Bool int Value
    // 0 = False
    // 1 = True
    [Required]
    public int IsSupervisor {get; set;}


    // Joined Tables build for when it is Required
    // Authored by : Tamela Lerma
    // public ICollection<EmployeeComputer> EmployeeComputer;

    // Joined Tables build for when it is Required
    // Authored by : Tamela Lerma
    // public ICollection<EmployeeTraining> EmployeeTraining;

  }
}