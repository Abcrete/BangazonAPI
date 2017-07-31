using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


// ERD shows this class as a Joined Table
// Requires:
// EmployeeComputerID
// EmployeeId   FK- from Employee Table
// ComputerId   FK- from Computer Table
// Non-required properties in this table are:
// StartDate
// EndDate
// Authored by : Tamela Lerma
namespace BangazonAPI.Models
{
    public class EmployeeComputer
    { 
        // PK will auto-increment Joined Table
        // authored by : Tamela Lerma
        [Key]
        public int EmployeeComputerId {get; set;}

        // EmployeeId is required and FK from Employee Table
        // An Employee can have many Computers but a computer can only have oe Employee assigned
        // Must make an instance of the Employee class to retrieve the EmployeeId
        // Authored by : Tamela Lerma
        [Required]
        public int EmployeeId {get; set;}
        public Employee employee {get; set;}


        // ComputerId is required and FK from Computer Table
        // An Employee can have many Computers but a computer can only have oe Employee assigned
        // Must make an instance of the Computer class to retrieve the ComputerId
        // Authored by : Tamela Lerma
        [Required]
        public int ComputerId {get; set;}
        public Computer computer {get; set;}

        // SqLite stores as string, to testing requests formating uses standard OS DateTime format, ex. "yyyy-MM-dd 'at' HH:mm"
        // Authored by: Tamela Lerma
        [DataType(DataType.Date)]
        public DateTime? StartDate {get; set;}


        // SqLite stores as string, to testing requests formating uses standard OS DateTime format, ex. "yyyy-MM-dd 'at' HH:mm"
        // Authored by: Tamela Lerma
        [DataType(DataType.Date)]
        public DateTime? EndDate {get; set;}

    }
}