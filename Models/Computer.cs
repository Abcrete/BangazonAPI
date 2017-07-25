using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BangazonAPI.Models
{
    public class Computer
    {
        [Key]
        public int ComputerId {get; set;}

        [Required]
        [DataType(DataType.Date)]
        public DateTime DatePurchased {get; set;}

        [Required]
        [DataType(DataType.Date)]
        public DateTime? DateDecomissioned {get; set;}

        // public ICollection<EmployeeComputer> EmployeeComputer;


    }
}