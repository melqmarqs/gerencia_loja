using ConsertoDeLivro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Web.Security;

namespace ConsertoDeLivro.Controllers {
    public class UsuarioController : Controller {
        ConsertoDeLivroContext db = new ConsertoDeLivroContext();

        // GET: Usuario
        [Authorize]
        public ActionResult Lista() {
            var usuarios = db.Usuarios.ToList();
            return View(usuarios);
        }

        public ActionResult Criar() {
            ViewBag.EstadoID = new SelectList(db.Estados, "EstadoID", "UF");
            return View();
        }

        [HttpPost]
        public ActionResult Criar(Usuario usuario) {
            ViewBag.CPFDuplicado = "";
            ViewBag.CelularDuplicado = "";
            ViewBag.EmailDuplicado = "";

            if (ModelState.IsValid) {
                #region Removendo traços, parenteses e pontos dos numeros de celular e CPF
                if (usuario.CPF.Contains(".")) usuario.CPF = usuario.CPF.Replace(".", "");
                if (usuario.CPF.Contains("-")) usuario.CPF = usuario.CPF.Replace("-", "");

                if (usuario.Celular.Contains("(")) usuario.Celular = usuario.Celular.Replace("(", "");
                if (usuario.Celular.Contains(")")) usuario.Celular = usuario.Celular.Replace(")", "");
                if (usuario.Celular.Contains("-")) usuario.Celular = usuario.Celular.Replace("-", "");
                if (usuario.Celular.Contains(" ")) usuario.Celular = usuario.Celular.Replace(" ", "");
                #endregion

                #region Verificando se há duplicidade no CPF, no numero do celular ou no Email
                var userDb = db.Usuarios.ToList();
                bool email = false;
                bool cpf = false;
                bool celular = false;
                foreach (var item in userDb) {
                    if (email) {
                        email = item.Email == usuario.Email ? true : false;
                        if (email) { ViewBag.EmailDuplicado = "Esse Email já está em uso."; }
                    }

                    if (cpf) {
                        cpf = item.CPF == usuario.CPF ? true : false;
                        if (cpf) { ViewBag.CPFDuplicado = "Esse CPF já está em uso."; }
                    }

                    if (celular) {
                        celular = item.Celular == usuario.Celular ? true : false;
                        if (celular) { ViewBag.CelularDuplicado = "Esse numero de celular já está sendo usado."; }
                    }
                }
                #endregion

                if (ViewBag.CPFDuplicado != String.Empty ||
                    ViewBag.CelularDuplicado != String.Empty ||
                    ViewBag.EmailDuplicado != String.Empty) {
                    ViewBag.EstadoID = new SelectList(db.Estados, "EstadoID", "UF");
                    return View(usuario);
                }

                usuario.Email = usuario.Email.ToLower();

                db.Usuarios.Add(usuario);
                db.SaveChanges();
                var userSession = Session["Usuario"] as Usuario;
                if (userSession != null && userSession.Adm) {
                    return RedirectToAction("Lista");
                } else {
                    return RedirectToAction("Login", "ConsertoDeLivro");
                }
            }

            ViewBag.EstadoID = new SelectList(db.Estados, "EstadoID", "UF", usuario.EstadoID);
            return View(usuario);
        }

        [Authorize]
        public ActionResult Atualizar(int id) {
            Usuario usuario = db.Usuarios.Find(id);
            ViewBag.EstadoID = new SelectList(db.Estados, "EstadoID", "UF", usuario.EstadoID);
            return View(usuario);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Atualizar(Usuario usuario) {
            ViewBag.CelularDuplicado = "";
            ViewBag.CPFDuplicado = "";
            ViewBag.EmailDuplicado = "";

            if (ModelState.IsValid) {
                if (usuario.Celular.Contains("(")) usuario.Celular = usuario.Celular.Replace("(", "");
                if (usuario.Celular.Contains(")")) usuario.Celular = usuario.Celular.Replace(")", "");
                if (usuario.Celular.Contains("-")) usuario.Celular = usuario.Celular.Replace("-", "");
                if (usuario.Celular.Contains(" ")) usuario.Celular = usuario.Celular.Replace(" ", "");

                if (usuario.CPF.Contains(".")) usuario.CPF = usuario.CPF.Replace(".", "");
                if (usuario.CPF.Contains("-")) usuario.CPF = usuario.CPF.Replace("-", "");

                #region Verificando se há duplicidade no CPF, no numero do celular ou no Email
                var userDb = db.Usuarios.ToList();
                bool email = false;
                bool cpf = false;
                bool celular = false;
                foreach (var item in userDb) {
                    if (email) {
                        email = item.Email == usuario.Email ? true : false;
                        if (email) { ViewBag.EmailDuplicado = "Esse Email já está em uso."; }
                    }

                    if (cpf) {
                        cpf = item.CPF == usuario.CPF ? true : false;
                        if (cpf) { ViewBag.CPFDuplicado = "Esse CPF já está em uso."; }
                    }

                    if (celular) {
                        celular = item.Celular == usuario.Celular ? true : false;
                        if (celular) { ViewBag.CelularDuplicado = "Esse numero de celular já está sendo usado."; }
                    }
                }
                #endregion

                if (ViewBag.CPFDuplicado != String.Empty ||
                    ViewBag.CelularDuplicado != String.Empty ||
                    ViewBag.EmailDuplicado != String.Empty) {
                    ViewBag.EstadoID = new SelectList(db.Estados, "EstadoID", "UF");
                    return View(usuario);
                }


                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                if ((Session["Usuario"] as Usuario).Adm) {
                    return RedirectToAction("Index", "ConsertoDeLivro");
                } else {
                    return RedirectToAction("Lista");
                }
            }

            ViewBag.EstadoID = new SelectList(db.Estados, "EstadoID", "UF");
            return View(usuario);
        }

        [Authorize]
        public ActionResult Excluir(int id) {
            Usuario usuario = db.Usuarios.Find(id);
            return View(usuario);
        }

        [HttpPost, ActionName("Excluir")]
        [Authorize]
        public ActionResult ExcluirConfirmacao(int id) {
            Usuario usuarioBd = db.Usuarios.Find(id);

            db.Usuarios.Remove(usuarioBd);
            db.SaveChanges();

            Usuario userLogado = Session["Usuario"] as Usuario;
            if (User.Identity.IsAuthenticated && (userLogado.UsuarioID == id)) {
                FormsAuthentication.SignOut();
                Session.Abandon();
            }

            return RedirectToAction("Lista");
        }

        [HttpGet]
        [Authorize]
        public ActionResult InfoUser(int id) {
            Usuario usuario = db.Usuarios.Find(id);
            usuario.Estado = new Estado();
            usuario.Estado = db.Estados.Find(usuario.EstadoID);
            return View(usuario);
        }
    }
}