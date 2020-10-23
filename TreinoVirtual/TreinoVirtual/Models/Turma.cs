using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TreinoVirtual.Models
{
    public class Turma
    {
        //Construtor 
        public Turma()
        {
            QuantParticipantes = 0;
        }

        // Uma turma terá varios alunos.
        // Relacionamento de 1.. para muitos
        public int TurmaId { get; set; }
        public string Nome{ get; set; }
        public string Horario { get; set; }
        public int QuantParticipantes { get; set; }

        // 
        public virtual ICollection<Aluno> Alunos { get; set; }


    }
}