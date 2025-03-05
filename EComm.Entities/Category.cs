using System.ComponentModel.DataAnnotations.Schema;

namespace EComm.Entities;

[Table("category")]
public class Category
{
    [Column("id")]
    public int Id { get; set; }
    [Column("category_name")]
    public string CategoryName { get; set; } = string.Empty;

    public List<Product>? Products { get; set; }
}
