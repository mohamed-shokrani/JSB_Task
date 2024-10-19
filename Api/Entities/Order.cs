using Api.Data;

namespace Api.Entities;

public class Order
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public int CustomerId { get; set; }
    public List<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();
}
