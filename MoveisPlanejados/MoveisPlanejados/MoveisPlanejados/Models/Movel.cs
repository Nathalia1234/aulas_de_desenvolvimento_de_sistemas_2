using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MoveisPlanejados.Models
{
    [Table("moveis")]
    public class Movel
    {
        // construtor para definir o status inicial do móvel 
        public Movel()
        {
            Status = "Solicitado";
        }

        [Key]
        public int Pk_Movel { get; set; }
        public string Nome { get; set; }
        public string Link { get; set; }
        public string Cor { get; set; }
        public string Medidas { get; set; }
        public string Status { get; set; }
        [ForeignKey("Funcionario")]
        public int? FK_Funcionario { get; set; }
        public virtual Funcionario Funcionario { get; set; }
    }
}