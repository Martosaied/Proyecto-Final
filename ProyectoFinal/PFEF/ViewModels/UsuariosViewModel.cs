using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PFEF.ViewModels
{
    public class RegistracionViewModel
    {
        [Required(ErrorMessage = "Campo obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email no valido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        [RegularExpression("^[a-zA-Z0-9]{8,10}$", ErrorMessage = "La contraseña debe tener de 8 a 10 caracteres, al menos un numero y una mayuscula")]
        [DataType(DataType.Password)]
        public string Contraseña { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        [Compare("Contraseña", ErrorMessage = "Las contraseñas no coinciden ")]
        public string confirmarContraseña { get; set; }
    }
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Campo obligatorio")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        [DataType(DataType.Password)]
        public string Contraseña { get; set; }
    }
}