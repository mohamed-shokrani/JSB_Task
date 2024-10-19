using Api.Data;
using Api.Dto_s;
using Api.Dto_s.OrderDto;
using Api.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;
[Route("api/orders")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public OrdersController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    [HttpGet]
    public async Task<ActionResult<List<OrderDto>>> GetAll()
    {
        var products = await _context.Orders.Include(a => a.OrderDetails)
                                            .ThenInclude(p => p.Product)
                                            .AsNoTracking()
                                            .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
                                            .ToListAsync();
        return Ok(products);
    }
    [HttpGet("{orderId}")]
    public async Task<ActionResult> GetOrderById(int orderId)
    {
        var order = await _context.Orders.Include(od=>od.OrderDetails)
                                         .ThenInclude(p=>p.Product)
                                         .AsNoTracking()
                                         .FirstOrDefaultAsync(x=>x.OrderId ==orderId);
        if (order == null)
            return NotFound();
        return Ok(_mapper.Map<OrderDto>(order));
    }
    [HttpPost]
    public async Task<IActionResult> Create(OrderDto orderDto)
    {
        var order = _mapper.Map<Order>(orderDto);
        order.TotalAmount = order.OrderDetails.Sum(x => x.Quantity * x.UnitPrice);
        await _context.Orders.AddAsync(order);
        var result = await _context.SaveChangesAsync() > 0;
        return result ? CreatedAtAction(nameof(GetOrderById), new {order.OrderId}, _mapper.Map<OrderDto>(order))
                      :BadRequest();
    }
    [HttpPut("{orderId}")]
    public async Task<ActionResult> Update(UpdateOrderDto orderDto, int orderId)
    {
        var order = await _context.Orders.Include(x => x.OrderDetails).FirstOrDefaultAsync(x => x.OrderId == orderId);
        if (order == null)
            return NotFound();


        foreach (var detailDto in orderDto.OrderDetailsDto)
        {
            var existingDetail = order.OrderDetails.FirstOrDefault(od => od.ProductId == detailDto.ProductId);

            if (existingDetail == null)
            {
                return NotFound();
            }
            existingDetail.Quantity = detailDto.Quantity;
            existingDetail.UnitPrice = detailDto.UnitPrice;
        }

        order.CustomerId = orderDto.CustomerId ?? order.CustomerId;

        var totalNotUpdated = order.OrderDetails.Where(x => orderDto.OrderDetailsDto
                                                .Any(a => a.ProductId != x.ProductId))
                                                .Sum(a => a.Quantity * a.UnitPrice);

        order.TotalAmount = orderDto.OrderDetailsDto
                                    .Sum(x => x.UnitPrice * x.Quantity) + totalNotUpdated;

        var result = await _context.SaveChangesAsync() > 0;
        return result ? Ok() : BadRequest();

    }
    [HttpDelete("{orderId}")]
    public async Task<ActionResult> Delete(int orderId)
    {

        var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId);
        if (order == null) return NotFound();
        _context.Orders.Remove(order);
        var result = await _context.SaveChangesAsync()>0;
        return result ? Ok() : BadRequest();
    }
}
