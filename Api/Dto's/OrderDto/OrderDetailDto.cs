using System.ComponentModel.DataAnnotations;

namespace Api.Dto_s.OrderDto;

public class OrderDetailDto
{
    public int? OrderId { get; set; }
    public int? ProductId { get; set; }
    [Range(0.01, (double)decimal.MaxValue, ErrorMessage = "Quantity must be a positive value.")]

    public decimal Quantity { get; set; }
    [Range(0.01, (double)decimal.MaxValue, ErrorMessage = "Price must be a positive value.")]

    public decimal UnitPrice { get; set; }
    public string? ProductName { get; set; }

}
