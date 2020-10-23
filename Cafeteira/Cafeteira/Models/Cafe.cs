using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cafeteira.Models
{
    public class Cafe
    {
        public int CafeId { get; set; }

        public string Marca { get; set; }

        public double  Preco { get; set; }

        public bool Descafeinado { get; set; }
        public string  Validade { get; set; }

    }
}