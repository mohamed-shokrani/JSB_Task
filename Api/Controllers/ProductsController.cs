using Api.Data;
using Api.Dto_s;
using Api.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;
[Route("api/products")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public ProductsController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    [HttpGet]
    public async Task<ActionResult<List<ProductDto>>> GetAll()
    {
        var products =await _context.Products.AsNoTracking()
                                             .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                                             .ToListAsync();

        return Ok(products);
    }
    [HttpGet("{productId}")]
    public async Task<ActionResult<ProductDto>> GetProductById(int productId)
    {
        var product = await _context.Products.AsNoTracking()
                                             .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                                             .FirstOrDefaultAsync(x => x.ProductId == productId);   
        
        return product != null ? Ok(product) : NotFound();
    }
    [HttpPost]
    public async Task<ActionResult> Create(CreateProductDto productDto)
    {
        var product = _mapper.Map<Product>(productDto);
        _context.Products.Add(product);
        var result= await _context.SaveChangesAsync()>0;
  
        return result ? CreatedAtAction(nameof(GetProductById),new { product.ProductId }, _mapper.Map<ProductDto>(product))
                      : BadRequest("Could not Save changes to Database");
    }
    [HttpPut("{productId}")]
    public async Task<ActionResult> Update(UpdateProductDto productDto,int productId)
    {
        var product = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == productId);

        if (product == null) 
              return NotFound();
        
           product.Description = productDto.Description ?? product.Description;
           product.Price = productDto.Price ?? product.Price;
           product.Stock = productDto.Stock ?? product.Stock;
           product.Name = productDto.Name ?? product.Name;
           var result =await  _context.SaveChangesAsync() >0;

         return  result ? Ok(product) : BadRequest("Could not Update Item from Database");
    }
    [HttpDelete("{productId}")]
    public async Task<ActionResult> Delete(int productId)
    {
        var product = await _context.Products.FindAsync(productId);

        if (product == null)
            return NotFound();

         _context.Products.Remove(product);
        var result = await _context.SaveChangesAsync() >0;
        return result ? Ok() : BadRequest("Could not remove Item from Database");

    }
}
