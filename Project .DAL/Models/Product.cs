using Project_.DAL.Migrations;
using Project_.DAL.Models;
public class Product
{
    public int Id { get; set; }
    public string Name_Product { get; set; }
   
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public ICollection<Item> Items { get; set;}

 
   
}
