using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EComm.Entities;

[Table("product")]
public class Product
{
    [Column("id")]
    public int Id { get; set; }
    [Column("product_name")]
    public string ProductName { get; set; } = String.Empty;
    [Column("unit_price")]
    public decimal? UnitPrice { get; set; }
    [Column("package")]
    public string? Package { get; set; }
    [Column("is_discontinued")]
    public bool IsDiscontinued { get; set; }
}
