using System.ComponentModel.DataAnnotations;

namespace CustomersAPI.Dtos
{
    public class CrearClienteDto
    {
        /* [Required (ErrorMessage = mensaje)] : Indica que esa propiedad es requerida
            y si no se coloca se mostrara dicho mensaje */
        [Required (ErrorMessage = "El nombre debe ser ingresado")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El apellido debe ser ingresado")]
        public string Apellidos { get; set; }

        /* [RegularExpression (expresion, ErrorMessage = mensaje)] : Valida el email mediante 
           una expresion regular y si esta incorrecto se envia dicho mensaje */
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "El email no es correcto")]
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
    }
}
