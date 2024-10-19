namespace Api.Entities;

public class Order
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public int ProductId { get; set; }
    public ICollection<Product> Products { get; set; }
}
