using EComm.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace EComm.MvcUI.Models;

public class ProductEditViewModel
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Must have name")]
    public string ProductName { get; set; } = String.Empty;
    public decimal? UnitPrice { get; set; }
    public string? Package { get; set; }
    public bool IsDiscontinued { get; set; }
    public int CategoryId { get; set; }
    public Category? Category { get; set; }

    public IEnumerable<Category> Categories { get; set; } = [];

    public IEnumerable<SelectListItem> CategoryItems =>
        Categories.Select(c => new SelectListItem
        {
            Text = c.CategoryName,
            Value = c.Id.ToString()
        })
        .OrderBy(item => item.Text);
}
