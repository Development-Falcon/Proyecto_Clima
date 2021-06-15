/* -----------------------------------------------
   Javier Cañadulce H.
   Copyright: Junio-2021
----------------------------------------------- */
/* -----------------------------------------------
   Prueba Técnica Ingeneo SAS 
   Código C# para mapear la tabla Usuarios.
   Servicio para la Conexión con la Base de Datos.
----------------------------------------------- */
namespace WebAppClima.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Usuarios
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El {0} es Obligatoria"), Display(Name = "Nombre de Usuario")]
        [StringLength(20, ErrorMessage = "El {0} debe tener al menos {2} caracteres de longitud.", MinimumLength = 4)]
        public string Usuario { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "La {0} es Obligatoria"), Display(Name = "Contraseña")]
        [StringLength(20, ErrorMessage = "La {0} debe tener al menos {2} caracteres de longitud.", MinimumLength = 4)]
        public string Password { get; set; }
    }
}
