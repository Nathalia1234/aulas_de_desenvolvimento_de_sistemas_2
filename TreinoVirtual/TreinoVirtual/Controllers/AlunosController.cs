using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TreinoVirtual.Data;
using TreinoVirtual.Models;

namespace TreinoVirtual.Controllers
{
    public class AlunosController : Controller
    {
        private TreinoVirtualContext db = new TreinoVirtualContext();

        // GET: Alunos
        public ActionResult Index()
        {
            //
            var alunoes = db.Alunoes.Include(a => a.Turma);
            return View(alunoes.ToList());
        }

        // GET: Alunos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aluno aluno = db.Alunoes.Find(id);
            if (aluno == null)
            {
                return HttpNotFound();
            }
            return View(aluno);
        }

        // GET: Alunos/Create
        public ActionResult Create()
        {
                                                              //                      , Id da turma, Nome da turma
            ViewBag.TurmaId = new SelectList(db.Turmas, "TurmaId", "Nome");
            return View();
        }

        // POST: Alunos/Create
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AlunoId,Nome,Peso,Altura,Objetivo,TurmaId")] Aluno aluno)
        {
           

            
            if (ModelState.IsValid)
            {
                // Foi pego o ID da turma,para descobrir quem é o elemento no banco de dados 
                Turma t = db.Turmas.Find(aluno.TurmaId);

                // Verificar se na turma que o aluno está inserido tem menos de 4 pessoas
                // Caso seja verdade.
                if (t.QuantParticipantes < 4)
                {
                      
                        // Acrescentar +1 na quantidade de participantes da turma que será salvo 
                        // no banco de dados e o aluno será adicionado no vetor.
                        t.QuantParticipantes = t.Alunos.Count;      // contagem de quantos participantes está cadastrado na turma 
                        t.QuantParticipantes++;                             // foi adicionado mais um na quantidade de participantes 
                        db.Entry(t).State = EntityState.Modified;   // foi editado a informação de turma no banco de dados 
                        db.Alunoes.Add(aluno);                            // adiciona o aluno no vetor  do banco de dados 
                        db.SaveChanges();                                  // salva no banco de dados e 
                        return RedirectToAction("Index");            // retorna para o index após a criação do objeto 
                    }            
                else
                {
                    // Enviar a mensagem de erro através da viewbag 
                    ViewBag.MensagemErro = "Esta turma ja esta cheia !  Por favor, selecione outra.";
                }
                }
           
               
            ViewBag.TurmaId = new SelectList(db.Turmas, "TurmaId", "Nome", aluno.TurmaId);
            return View(aluno);
        }

        // GET: Alunos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aluno aluno = db.Alunoes.Find(id);
            if (aluno == null)
            {
                return HttpNotFound();
            }
            ViewBag.TurmaId = new SelectList(db.Turmas, "TurmaId", "Nome", aluno.TurmaId);
            return View(aluno);
        }

        // POST: Alunos/Edit/5
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AlunoId,Nome,Peso,Altura,Objetivo,TurmaId")] Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aluno).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TurmaId = new SelectList(db.Turmas, "TurmaId", "Nome", aluno.TurmaId);
            return View(aluno);
        }

        // GET: Alunos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aluno aluno = db.Alunoes.Find(id);
            if (aluno == null)
            {
                return HttpNotFound();
            }
            return View(aluno);
        }

        // POST: Alunos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Aluno aluno = db.Alunoes.Find(id);
            db.Alunoes.Remove(aluno);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
