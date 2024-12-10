using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;
public class Item
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? Name { get; set; }
    public decimal Price { get; set; }
}
