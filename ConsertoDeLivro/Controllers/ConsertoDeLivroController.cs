using ConsertoDeLivro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ConsertoDeLivro.Controllers {
    public class ConsertoDeLivroController : Controller {
        ConsertoDeLivroContext db = new ConsertoDeLivroContext();


        // GET: ConsertoDeLivro
        public ActionResult Index() {
            TempData["End"] = "Rua: Rua 1, nº 999 - Nome do Bairro - Cidade, Estado";
            return View();
        }

        public ActionResult Login() {
            return View();
        }

        [HttpPost, ActionName("Login")]
        public ActionResult Login([Bind(Include = "Email, Senha")] Usuario usuario) {
            if (usuario.Email != null && usuario.Senha != null) {
                List<Usuario> usuariosDb = db.Usuarios.ToList();
                var user = usuariosDb.Find(userdb => userdb.Email == usuario.Email && userdb.Senha == usuario.Senha);
                if (user == null) {
                    ViewBag.MsgErro = "Email e/ou senha incorretos. Por favor, verifique-os.";
                    return View(usuario);
                } else {
                    FormsAuthentication.SetAuthCookie(user.Email, false);
                    Session["Usuario"] = user;
                    return RedirectToAction("Index");
                }
            }
            ViewBag.MsgErro = "É necessário preencher os dois campos, email e senha.";
            return View(usuario);
        }

        public ActionResult Cancelar() {
            return RedirectToAction("Index");
        }

        public ActionResult Sair() {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult TrocarSenha() {
            return View();
        }

        [HttpPost]
        public ActionResult TrocarSenha(string cpf, string email, string senha) {
            if (cpf != null && email != null && senha != null) {
                var user = db.Usuarios.First(User => User.Email == email && User.CPF == cpf);
                if (user != null) {
                    user.Senha = senha;
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }
            }
            return View();
        }

        public ActionResult Endereco() {
            return View();
        }
    }
}