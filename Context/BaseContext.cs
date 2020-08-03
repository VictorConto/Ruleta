using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WebAppRuleta.Modelo;

namespace WebAppRuleta.Context
{
    public class BaseContext : DbContext
    {
        public BaseContext()
        : base("BaseContextBase")
        {
        }


        public DbSet<Jugador> Jugador { get; set; }

        
    }
}