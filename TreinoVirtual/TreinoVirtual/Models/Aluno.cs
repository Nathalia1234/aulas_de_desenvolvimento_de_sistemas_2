using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TreinoVirtual.Models
{
    public class Aluno
    {
        // O aluno só poderá estar em uma turma 
        public int AlunoId { get; set; }
        public string Nome { get; set; }
        public float   Peso { get; set; }
        public float Altura { get; set; }
        public string Objetivo { get; set; }

        //Associação com a classe turma
        public int TurmaId { get; set; }

        // Quando coloca "virtual" significa que 
        // quando esse objeto for carregado todas as referências serão carregadas também.
        public virtual Turma Turma{ get; set; }
    }
}