using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EComm.Entities;

[Table("product")]
public class Product : IValidatableObject
{
    [Column("id")]
    public int Id { get; set; }
    [Required(ErrorMessage = "Must have name")]
    [Column("product_name")]
    public string ProductName { get; set; } = String.Empty;
    [Column("unit_price")]
    public decimal? UnitPrice { get; set; }
    [Column("package")]
    public string? Package { get; set; }
    [Column("is_discontinued")]
    public bool IsDiscontinued { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var retVal = new List<ValidationResult>();
        if (ProductName.StartsWith("x"))
        {
            retVal.Add(new ValidationResult("BAD"));
        }
        return retVal;
    }
}
