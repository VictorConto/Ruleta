using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppRuleta.Modelo
{
    public class JugadorApuesta
    {
        public int Id { get; set; }
        public string Documento { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NombreCompleto { get; set; }
        public string Color { get; set; }
        public string Apuesta { get; set; }
        public decimal CantidadDinero { get; set; }
        public int ValorApuesta { get; set; }
    }
}