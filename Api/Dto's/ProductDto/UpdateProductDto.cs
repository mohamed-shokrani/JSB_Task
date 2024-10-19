using System.ComponentModel.DataAnnotations;

namespace Api.Dto_s;

public class UpdateProductDto
{
    public  string? Name { get; set; }
    public  string? Description { get; set; }
    [Range(0.01, (double)decimal.MaxValue, ErrorMessage = "Price must be a positive value.")]
    public decimal? Price { get; set; }
    [Range(0.01, (double)decimal.MaxValue, ErrorMessage = "Stock must be a positive value.")]
    public decimal? Stock { get; set; }
}
