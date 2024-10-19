using Api.Entities;
using System.ComponentModel.DataAnnotations;

namespace Api.Data
{
    public class OrderDetails
    {
        public int OrderDetailsId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        [Range(0.01, (double)decimal.MaxValue, ErrorMessage = "Quantity must be a positive value.")]

        public decimal Quantity { get; set; }
        [Range(0.01, (double)decimal.MaxValue, ErrorMessage = "Price must be a positive value.")]

        public decimal UnitPrice { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
