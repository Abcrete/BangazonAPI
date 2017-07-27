//Authored by: Jason Smith
//Purpose: create a department object to represent items in the database and items being posted
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BangazonAPI.Models
{
    // Department class represents a department in the company which can contain many employees
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Budget { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
