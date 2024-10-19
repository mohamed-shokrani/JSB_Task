using System.ComponentModel.DataAnnotations;

namespace Api.Dto_s.OrderDto;

public class UpdateOrderDto
{
    public int? CustomerId { get; set; }

    public List<OrderDetailDto> OrderDetailsDto { get; set; } = new List<OrderDetailDto>();
}
