//Authored by: Jason Smith
//Purpose: create a product object to represent items in the database and items being posted
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BangazonAPI.Models
{
    // Product class represents a product created by a customer, it has a many to many relationship with order
    public class Product
    {
        [Key]
        public int ProductId {get; set;}
        
        [Required]
        public string Title {get; set;}
       
        [Required]
        public double Price {get; set;}
        
        [Required]
        public int Quantity {get; set;}
       
        [Required]
        public string Description {get; set;}
        
        [Required]
        public int CustomerId {get; set;}
        
        [Required]
        public int ProductTypeId {get; set;}

        public ICollection<OrderProduct> OrderProducts;
        public Customer Customer {get; set;}
        public ProductType ProductType {get; set;}
    }
}