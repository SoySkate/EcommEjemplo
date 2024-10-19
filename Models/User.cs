using EcommerceEjemploApi.Enums;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace EcommerceEjemploApi.Models
{
    //Clase creada que se usara para crear objetos de esta clase
    //Se le añade si es necesario segun las relaciones las foreign key
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string PasswordHash { get; set; } // Store the hashed password here
        public UserRole UserRole { get; set; }

        // Relación de uno a muchos: un usuario puede tener varias reseñas (reviews)
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Order> Orders { get; set; }
    }

}
