using EcommerceEjemploApi.Enums;

namespace EcommerceEjemploApi.Dto
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Details { get; set; }
        public DateTime DateTime { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public int UserId { get; set; }
    }
}
