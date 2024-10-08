using EcommerceEjemploApi.Enums;

namespace EcommerceEjemploApi.Models
{
    public class Order
    {
        public int Id { get; set; }
        //aqui nose si pasar el user.address o como
        public string Address { get; set; }
        public string Details { get; set; }
        public DateTime DateTime { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
        public User User { get; set; }


    }
}
