using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppRuleta.Modelo
{
    public class Jugador
    {
        public int Id { get; set; }
        public string Documento { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public decimal CantidadDinero { get; set; }
    }
}