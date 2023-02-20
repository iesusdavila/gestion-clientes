using System.ComponentModel.DataAnnotations;

namespace CustomersAPI.Dtos
{
    public class CrearClienteDto
    {
        /* [Required (propiedad = valor)] : Indica que esa propiedad es requerida
            y "propiedad = valor" lo que debe suceder en caso que no se coloque*/
        
        [Required (ErrorMessage = "El nombre debe se ingresarse")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El apellido debe se ingresarse")]
        public string Apellidos { get; set; }
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "el email no es correcto")]
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
    }
}
