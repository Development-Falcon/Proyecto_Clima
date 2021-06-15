/* -----------------------------------------------
   Javier Cañadulce H.
   Copyright: Junio-2021
----------------------------------------------- */
/* -----------------------------------------------
   Prueba Técnica Ingeneo SAS 
   Código C# para mapear la tabla Usuarios.
   Servicio para la Conexión con la Base de Datos.
----------------------------------------------- */
namespace ServicioWebDatos.Modelo
{
    using System;
    using System.Collections.Generic;
    
    public partial class Usuarios
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
    }
}
