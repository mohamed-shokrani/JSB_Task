namespace Api.Dto_s.OrderDto;
public class OrderDto
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; } 
    public int  CustomerId { get; set; }

    public List<OrderDetailDto> OrderDetailsDto  { get; set; }=new List<OrderDetailDto>();
}
