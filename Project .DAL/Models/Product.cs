using Project_.DAL.Models;
using System;
using System.Collections.Generic;

public class Product
{
    public int Id { get; set; }

    public string Name_Product { get; set; }

    // Changed Price to a proper type and made it a property
    public decimal Price { get; set; }

    public bool IsDeleted { get; set; }
    public string ImageName { get; set; }
    public DateTime CreatedAt { get; set; }

    // Changed to a property with a proper collection type
    public virtual ICollection<Item> Items { get; set; }

    // Constructor to initialize the Items collection
    public Product()
    {
        Items = new List<Item>();
    }
}
