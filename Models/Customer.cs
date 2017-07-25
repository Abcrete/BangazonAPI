using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BangazonAPI.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
       
        [Required]
        public string FirstName { get; set; }
       
        [Required]
        public string LastName { get; set; }
       
        [Required]
        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime AcctCreatedOn { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime LastLogin { get; set; }
        
        [Required]
        public bool IsActive { get; set; }

        public ICollection<Product> Products;
        public ICollection<Order> Orders;
        public ICollection<PaymentType> PaymentTypes;


    }
}
