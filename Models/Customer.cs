using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BangazonAPI.Models
{
    //SUMMARY
    /*This class is authored by Jordan Dhaenens. 
      Customer Class identifies the properties for the entries to the Customer Tabel in database*/
    public class Customer
    {
        /*This is the primary key for an entry on the Customer table. It is generated by the database and doesn't
        need to be submitted by the user*/
        [Key]
        public int CustomerId { get; set; }
       
        [Required]
        public string FirstName { get; set; }
       
        [Required]
        public string LastName { get; set; }
       
        /*This property will be auto-generated by the database when it creates the customer entry
        and doesn't need to be filled in by the user.
        This property was authored by Jordan Dhaenens*/
        [Required]
        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime AcctCreatedOn { get; set; }
        
        /*This property will be auto-generated by the database when it creates the customer entry
        and doesn't need to be filled in by the user.
        This property was authored by Jordan Dhaenens*/
        [Required]
        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime LastLogin { get; set; }
        
        [Required]
        public int IsActive { get; set; }

        //The following 3 collections represent tables in which CustomerId is a Foreign Key
        public ICollection<Product> Products;
        public ICollection<Order> Orders;
        public ICollection<PaymentType> PaymentTypes;


    }
}
