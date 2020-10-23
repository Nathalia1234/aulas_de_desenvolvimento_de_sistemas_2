using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Cafeteira.Data
{
    public class CafeteiraContext : DbContext
    {

    
        public CafeteiraContext() : base("name=CafeteiraContext")
        {
        }


        public System.Data.Entity.DbSet<Cafeteira.Models.Cafe> Cafes { get; set; }
    }
}
