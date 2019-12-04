using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

//paginas de ayuda para expresiones regulares
//https://www.debuggex.com/r/ah_JuglZGAIK_gqO
//http://www.regexplanet.com/advanced/dotnet/index.html

namespace WebAppCargadorRips.Models
{
    public class CuentaViewModel
    {
    }

    public class LoginViewModel
    {
        [Required]
        [StringLength(12, ErrorMessage = "El número de caracteres de {0} debe ser de {2}.", MinimumLength = 12)]
        [Display(Name = "Código Prestador (*)")]
        public string Usuario { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser mínimo {2}.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña (*)")]
        public string Password { get; set; }

        //[Display(Name = "¿Recordar cuenta?")]
        //public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {

        [Required]
        [EmailAddress]
        [Display(Name = "Correo electrónico (*)")]
        public string Email { get; set; }


        [EmailAddress]
        [Display(Name = "Confirmar Correo electrónico (*)")]
        [Compare("Email", ErrorMessage = "El correo y el correo de confirmación no coinciden.")]
        public string ConfirmEmail { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser mínimo {2}.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[a-z])(?=.*\W){8}.*$", ErrorMessage = "La contraseña debe contener: mínimo 8 caracteres una letra mayúscula, una letra minúsculas, un número y caracteres especiales (por ejemplo,! @ # $% ^ & *)")]
        [Display(Name = "Contraseña (*)")]
        public string Pasword { get; set; }


        [Required]
        [DataType(DataType.Password)]        
        [Display(Name = "Confirmar contraseña (*)")]
        [Compare("Pasword", ErrorMessage = "La contraseña y la contraseña de confirmación no coinciden.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Nombres (*)")]
        public string Nombres { get; set; }

        [StringLength(100)]
        [Display(Name = "Apellidos")]
        public string Apellidos { get; set; }

        [Required]
        [Display(Name = "Razón Social (*)")]
        public string RazonSocial { get; set; }

        [MaxLength(10, ErrorMessage = "El número de caracteres de {0} debe ser maximo de 10.")]
        //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{7})?([0-9]{7})*$", ErrorMessage = "Para {0} ingrese valores numericos")]
        //[RegularExpression(@"^[+-]?\d+(\.\d+)?$")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Para {0} ingrese solo valores numéricos")]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }

        
        [Required]
        [StringLength(12, ErrorMessage = "El número de caracteres de {0} debe ser al menos de {2}.", MinimumLength = 12)]
        [RegularExpression(@"^(11001.[0-9]{6})*$", ErrorMessage = "Para {0} debe empezar con 11001, solo valores numéricos, sin espacios y de 12 caracteres")]
        [Display(Name = "Código Prestador (*)")]
        public string CodPrestador { get; set; }

        [Required]
        [Display(Name = "Al registrarme, verifico que he leído y entiendo los")]
        public bool aceptarTerminos { get; set; }
    }

    public class ActualizarDatosViewModel
    {

        [Required]
        public int usuario_id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser mínimo de {2}.", MinimumLength = 1)]
        [Display(Name = "Codigó Prestador (*)")]
        public string codigo { get; set; }

        [Required]
        public string nombres { get; set; }

        public string apellidos { get; set; }

        [Required]
        public string razon_social { get; set; }

        [Required]
        [EmailAddress]
        public string correo { get; set; }

        public string telefono { get; set; }

        public int id_rol { get; set; }

        public int id_estado { get; set; }

    }

    public class CambiarContraseniaViewModel
    {

        [Required]
        public int idUsuario { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos de {2}.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[a-z])(?=.*\W){8}.*$", ErrorMessage = "La contraseña debe contener: mínimo 8 caracteres una letra mayúscula, una letra minúsculas, un número y caracteres especiales (por ejemplo,! @ # $% ^ & *)")]
        [Display(Name = "Contraseña (*)")]
        public string contrasenia { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("contrasenia", ErrorMessage = "La contraseña y su confirmación no coinciden.")]
        [Display(Name = "Confirmación de Contraseña (*)")]
        public string contraseniaconfirm { get; set; }

    }

    public class RecuperarContraseniaViewModel
    {
        [Required]
        [StringLength(12, ErrorMessage = "El número de caracteres de {0} debe ser mínimo de {2}.", MinimumLength = 12)]
        [RegularExpression(@"^(11001.[0-9]{6})*$", ErrorMessage = "Para {0} debe empezar con 11001, solo valores numericos, sin espacios y de 12 caracteres")]
        [Display(Name = "Código Prestador (*)")]
        //[RegularExpression("^[0-9]?(11001.{7})*$", ErrorMessage = "Para {0} debe empezar con 11001, solo valores numericos y sin espacios")]
        public string Usuario { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Correo electrónico (*)")]
        public string Email { get; set; }
       
    }

    public class validarContraseniaModel
    {
        [Required]
        //[RegularExpression("^[0-9]?(11001.{7})*$", ErrorMessage = "Para {0} debe empezar con 11001, solo valores numericos y sin espacios")]
        public string id { get; set; }

        [Required]
        public string token { get; set; }
    }


}