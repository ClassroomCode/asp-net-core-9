namespace EComm.Entities;

public class Product
{
    public int Id { get; set; }
    public string ProductName { get; set; } = String.Empty;
    public decimal? UnitPrice { get; set; }
    public string? Package { get; set; }
    public bool IsDiscontinued { get; set; }
}
