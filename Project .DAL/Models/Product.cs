using Project_.DAL.Models;
using System;
using System.Collections.Generic;

public class Product
{
    public int Id { get; set; }

    public string Name_Product { get; set; }

  
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Item> Items { get; set; }

}
