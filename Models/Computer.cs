using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Use Models in namespace for reference to Computer.cs location
// Authored by: Tamela Lerma
namespace BangazonAPI.Models
{

    public class Computer
    {
        // Key will auto-increment new entries for ComputerId, ComputerId allows get and set of the ID
        // Authored by: Tamela Lerma
        [Key]
        public int ComputerId {get; set;}

        // SqLite stores as string, to testing requests formating uses standard IOS DateTime format, ex. "yyyy-MM-dd 'at' HH:mm"
        // Authored by: Tamela Lerma
        [Required]
        [DataType(DataType.Date)]
        public DateTime DatePurchased {get; set;}


        // SqLite stores as string, to testing requests formating uses standard IOS DateTime format, ex. "yyyy-MM-dd 'at' HH:mm"
        // Authored by: Tamela Lerma
        [DataType(DataType.Date)]
        public DateTime? DateDecommissioned {get; set;}

        // FK for Joined Table EmployeeComputer
        // public ICollection<EmployeeComputer> EmployeeComputer;
        // Authored by: Tamela Lerma
    }
}