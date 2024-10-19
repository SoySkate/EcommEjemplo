using System.ComponentModel.DataAnnotations;
using EcommerceEjemploApi.Enums;

namespace EcommerceEjemploApi.Dto
{

    //El dto se usa para poder simplificar la comunicacion, proteger integridad, mejorar la performance, 
    //y facil serializar

    //hay etiquetas de validacion predeterminadas, y tambien se pueden generar etiquetas personales (nose como)
    //mirar "google"
    //ETIQUETAS EJEMPLO: [requirede] / [ EmailAddress]...
    public class UserDto
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Address { get; set; }
        [Phone]
        public string Phone { get; set; }
        [MaxLength(30)]
        [MinLength(6)]
        public string PasswordHash { get; set; }
        public UserRole UserRole { get; set; }
    }
}
