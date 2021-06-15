/* -----------------------------------------------
Javier Cañadulce H.
Copyright: Junio-2021
----------------------------------------------- */
/* -----------------------------------------------
   Prueba Técnica Ingeneo SAS 
   Código C# para mapear la tabla Clima.
   Servicio para la Conexión con la Base de Datos.
----------------------------------------------- */
namespace WebAppClima.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class Clima
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "La {0} es Requerida"), Display(Name = "Nombre de la Ciudad")]
        [StringLength(40, ErrorMessage = "La {0} debe tener al menos {2} caracteres de longitud.", MinimumLength = 4)]
        public string Ciudad { get; set; }
        [DataType(DataType.Currency)]
        [Range(-99.99, 99.99)]
        [Required(ErrorMessage = "La {0} es Obligatoria"), Display(Name = "Temperatura ºC")]
        [DisplayFormat(DataFormatString = "{0:###.00}", ApplyFormatInEditMode = false, ConvertEmptyStringToNull = true)]
        public decimal Celsius { get; set; }
        [DataType(DataType.Currency)]
        [Range(-99.99, 99.99)]
        [Display(Name = "Temperatura ºF")]
        [DisplayFormat(DataFormatString = "{0:###.00}", ApplyFormatInEditMode = false, ConvertEmptyStringToNull = true)]
        public Nullable<decimal> Fahrenheit { get; set; }
        [Range(-99999.999, 99999.999)]
        [Display(Name = "Latitud")]
        [DisplayFormat(DataFormatString = "{0:#####.000}", ApplyFormatInEditMode = false, ConvertEmptyStringToNull = true)]
        public Nullable<decimal> Latitud { get; set; }
        public Nullable<decimal> Longitud { get; set; }
        [Display(Name = "Fecha de Actualización")]
        // Dejar de esta Forma [DataType(DataType.Date)] para Edición con el Control de Fechas.
        [DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> Fecha { get; set; }
    }
}
