using EcommerceEjemploApi.Enums;

namespace EcommerceEjemploApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public UserRole UserRole { get; set; }
        // Relación de uno a muchos: un usuario puede tener varias reseñas (reviews)
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
