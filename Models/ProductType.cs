using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Use Models in namespace for reference to ProductType.cs location
// Authored by: Tamela Lerma
namespace BangazonAPI.Models
{
  // Key will auto-increment new entries for ProductTypeId 
  // Authored by: Tamela Lerma
  public class ProductType
  {
    [Key]
    public int ProductTypeId { get; set; }

    // String to get and set the name of the ProductType
    // Authored by : Tamela Lerma
    [Required]
    public string Name {get; set;}

  }
}