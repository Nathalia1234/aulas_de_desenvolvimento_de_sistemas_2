using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MoveisPlanejados.Models;

namespace MoveisPlanejados.Controllers
{
    public class MoveisController : Controller
    {
        private MoveisPlanejadosContext db = new MoveisPlanejadosContext();



        // GET: Moveis
        public ActionResult Index()
        {
            var movels = db.Movels.Include(m => m.Funcionario);
            return View(movels.ToList());
        }

        // GET: Moveis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movel movel = db.Movels.Find(id);
            if (movel == null)
            {
                return HttpNotFound();
            }
            return View(movel);
        }

        // GET: Moveis/Create
        public ActionResult Create()
        {
            ViewBag.FK_Funcionario = new SelectList(db.Funcionarios, "Pk_Funcionario", "Nome");
            return View();
        }

        // POST: Moveis/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Pk_Movel,Nome,Link,Cor,Medidas,Status,FK_Funcionario")] Movel movel)
        {
            if (ModelState.IsValid)
            {
                db.Movels.Add(movel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FK_Funcionario = new SelectList(db.Funcionarios, "Pk_Funcionario", "Nome", movel.FK_Funcionario);
            return View(movel);
        }

        // GET: Moveis/ Construcao
        public ActionResult Construcao (int? id)
        {
            // Caso o ID na URL seja retirado, vai cair nesta condição 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Encontrar o ID do objeto no banco de dados (Context) e vai carregar todas as informações 
            // do movel para renderizar o nome do movel no input readonly e depois passar no POST o ID 
            // do objeto que foi carregado nesta variavel movel 
            Movel movel = db.Movels.Find(id);
            // Se não encontrar este item no banco de dados retorna 404 not found 
            if (movel == null)
            {
                return HttpNotFound();
            }
            // Está é a Viewbag que leva a informação dos funcionários para a view Construção e mostrar num dropdown 
            // que está com o mesmo nome: FK_funcionário. 
            // OBS: O segundo parametro do selectlist é PK_Funcionario, que vai para o POST. 
            ViewBag.FK_Funcionario = new SelectList(db.Funcionarios, "Pk_Funcionario", "Nome");
            // este móvel é aquele que a professora comentou lá em cima, que ja esta com os dados no banco e vai levar para o POST 
            // o que precisar. 
            return View(movel);
        }


        // POST: Construcao

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Construcao ([Bind(Include = "Pk_Movel,FK_Funcionario")] Movel movel)
        {

            if (ModelState.IsValid)
            {
                // Criar uma variavel para resgatar o objeto funcionario lá
                // do banco.
                // Buscar no context pelo ID do móvel. 
                Funcionario funcionario = db.Funcionarios.Find(movel.FK_Funcionario);
                funcionario.Disponivel = false;

                //Vamos modificar o status do movel antes de salvar 
                movel.Status = "Em construção";

                // Exatamente a mesma coisa do edit, eu pego o item que trouxe da tela e monto o objeto 
                // com o novo valor (chave estrangeira de funcionario) e salvo essa informação no banco de dados. 
                db.Entry(movel).State = EntityState.Modified;
                // precisamos avisar para o banco que houve modificação tbm em funcionário 
                db.Entry(funcionario).State = EntityState.Modified;
                // esse comando atualiza tudo que tiver para atualizar no banco de dados 
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_Funcionario = new SelectList(db.Funcionarios, "Pk_Funcionario", "Nome", movel.FK_Funcionario);
            return View(movel);
        }



        // GET: Moveis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movel movel = db.Movels.Find(id);
            if (movel == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_Funcionario = new SelectList(db.Funcionarios, "Pk_Funcionario", "Nome", movel.FK_Funcionario);
            return View(movel);
        }

        // POST: Moveis/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Pk_Movel,Nome,Link,Cor,Medidas,Status,FK_Funcionario")] Movel movel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_Funcionario = new SelectList(db.Funcionarios, "Pk_Funcionario", "Nome", movel.FK_Funcionario);
            return View(movel);
        }

        // GET: Moveis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movel movel = db.Movels.Find(id);
            if (movel == null)
            {
                return HttpNotFound();
            }
            return View(movel);
        }

        // POST: Moveis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movel movel = db.Movels.Find(id);
            db.Movels.Remove(movel);
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
