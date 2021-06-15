/* -----------------------------------------------
Javier Cañadulce H.
Copyright: Junio-2021
----------------------------------------------- */
/* -----------------------------------------------
   Prueba Técnica Ingeneo SAS 
   Código C# para mapear la tabla Clima.
   Servicio para la Conexión con la Base de Datos.
----------------------------------------------- */
namespace ServicioWebDatos.Modelo
{
    using System;
    using System.Collections.Generic;
    
    public partial class Clima
    {
        public int Id { get; set; }
        public string Ciudad { get; set; }
        public decimal Celsius { get; set; }
        public Nullable<decimal> Fahrenheit { get; set; }
        public Nullable<decimal> Latitud { get; set; }
        public Nullable<decimal> Longitud { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
    }
}
