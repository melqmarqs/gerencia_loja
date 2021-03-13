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
                    return View();
                } else {
                    FormsAuthentication.SetAuthCookie(user.Email, false);
                    Session["Usuario"] = user;
                    return RedirectToAction("Index");
                }
            }
            ViewBag.MsgErro = "É necessário preencher os dois campos, email e senha.";
            return View();
        }

        public ActionResult Cancelar() {
            return RedirectToAction("Index");
        }

        public ActionResult Sair() {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index");
        }
    }
}