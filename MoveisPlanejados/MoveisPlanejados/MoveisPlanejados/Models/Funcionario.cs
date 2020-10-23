using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MoveisPlanejados.Models
{
    public class Funcionario
    {
        // construtor 
        public Funcionario()
        {
            Disponivel = true; 
        }

        [Key]
        public int Pk_Funcionario { get; set; }
        public string Nome { get; set; }
        public bool Disponivel { get; set; }
        public virtual ICollection<Movel> Moveis { get; set; }
    }
}