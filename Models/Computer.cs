using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// <summary> 
// Use Models in namespace for reference to Computer.cs location
namespace BangazonAPI.Models
{
    public class Computer
    {
        // <summary>
        // Key will auto-increment new entries for ComputerId, ComputerId allows get and set of the ID
        [Key]
        public int ComputerId {get; set;}

        // <summary>
        // SqLite stores as string, to testing requests formating uses standard IOS DateTime format, ex. "yyyy-MM-dd 'at' HH:mm"
        [Required]
        [DataType(DataType.Date)]
        public DateTime DatePurchased {get; set;}


        // <summary>
        // SqLite stores as string, to testing requests formating uses standard IOS DateTime format, ex. "yyyy-MM-dd 'at' HH:mm"
        [Required]
        [DataType(DataType.Date)]
        public DateTime? DateDecomissioned {get; set;}

        // public ICollection<EmployeeComputer> EmployeeComputer;


    }
}