namespace EcommerceEjemploApi.Models
{
    //Clase creada que se usara para crear objetos de esta clase
    //Se le añade si es necesario segun las relaciones las foreign key
    public class OrderDetail
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        //foreign keys:
        public int ProductId {  get; set; }
        public Product Product { get; set; }
        public int OrderId {  get; set; }
        public Order Order { get; set; }
    }
}
