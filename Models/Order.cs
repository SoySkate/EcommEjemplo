using EcommerceEjemploApi.Enums;

namespace EcommerceEjemploApi.Models
{
    //Clase creada que se usara para crear objetos de esta clase
    //Se le añade si es necesario segun las relaciones las foreign key
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
        
        //foreign keys:
        public int UserId {  get; set; }
        public User User { get; set; }


    }
}
