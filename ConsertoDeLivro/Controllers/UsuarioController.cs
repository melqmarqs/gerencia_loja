using ConsertoDeLivro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Web.Security;
using System.Web.UI;
using System.Web.Script.Serialization;

namespace ConsertoDeLivro.Controllers {
    public class UsuarioController : Controller {
        ConsertoDeLivroContext db = new ConsertoDeLivroContext();

        // GET: Usuario
        [Authorize]
        public ActionResult Lista() {
            var userLogado = Session["Usuario"] as Usuario;
            List<Usuario> usuarios = new List<Usuario>();
            if (userLogado != null) {
                if (userLogado.Dev)
                    usuarios = db.Usuarios.ToList();
                else if (userLogado.Adm)
                    usuarios = db.Usuarios.Where(user => !user.Dev).ToList();
                else
                    return RedirectToAction("Index", "ConsertoDeLivro");

                return View(usuarios);
            }

            return RedirectToAction("Index", "ConsertoDeLivro");
        }

        public ActionResult ListaUsuarios() {
            var userLogado = Session["Usuario"] as Usuario;
            List<Usuario> usuarios = new List<Usuario>();
            if (userLogado != null) {
                if (userLogado.Dev)
                    usuarios = db.Usuarios.ToList();
                else if (userLogado.Adm)
                    usuarios = db.Usuarios.Where(user => !user.Dev).ToList();
                else
                    return RedirectToAction("Index", "ConsertoDeLivro");

                return Json(usuarios, JsonRequestBehavior.AllowGet);
            }
            return Json(usuarios, JsonRequestBehavior.AllowGet);
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
                var usuariosBD = db.Usuarios.ToList();
                bool email = false;
                bool cpf = false;
                bool celular = false;
                foreach (var item in usuariosBD) {
                    if (!email) {
                        email = item.Email == usuario.Email;
                        if (email) { ViewBag.EmailDuplicado = "Esse Email já está em uso."; }
                    }

                    if (!cpf) {
                        cpf = item.CPF == usuario.CPF;
                        if (cpf) { ViewBag.CPFDuplicado = "Esse CPF já está em uso."; }
                    }

                    if (!celular) {
                        celular = item.Celular == usuario.Celular;
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
                    TempData["userNome"] = usuario.Nome + " " + usuario.UltimoNome;
                    TempData["userEmail"] = usuario.Email;
                    return RedirectToAction("Login", "ConsertoDeLivro");
                }
            }

            ViewBag.EstadoID = new SelectList(db.Estados, "EstadoID", "UF", usuario.EstadoID);
            return View(usuario);
        }

        [Authorize]
        public ActionResult Atualizar(int id) {
            Usuario SessionUsuario = Session["Usuario"] as Usuario;
            
            if (SessionUsuario.Adm || SessionUsuario.Dev) {
                Usuario usuario = db.Usuarios.Find(id);
                ViewBag.EstadoID = new SelectList(db.Estados, "EstadoID", "UF", usuario.EstadoID);
                return View(usuario);
            } else {
                ViewBag.EstadoID = new SelectList(db.Estados, "EstadoID", "UF", SessionUsuario.EstadoID);
                return View(SessionUsuario);
            }

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
                var usuariosBD = db.Usuarios.AsNoTracking().ToList();
                bool email = false;
                bool cpf = false;
                bool celular = false;
                foreach (var item in usuariosBD) {
                    if (usuario.UsuarioID != item.UsuarioID) {
                        if (!email) {
                            email = item.Email == usuario.Email;
                            if (email) { ViewBag.EmailDuplicado = "Esse Email já está em uso."; }
                        }

                        if (!cpf) {
                            cpf = item.CPF == usuario.CPF;
                            if (cpf) { ViewBag.CPFDuplicado = "Esse CPF já está em uso."; }
                        }

                        if (!celular) {
                            celular = item.Celular == usuario.Celular;
                            if (celular) { ViewBag.CelularDuplicado = "Esse numero de celular já está sendo usado."; }
                        }
                    }
                }
                #endregion

                if (ViewBag.CPFDuplicado != String.Empty ||
                    ViewBag.CelularDuplicado != String.Empty ||
                    ViewBag.EmailDuplicado != String.Empty) {
                    ViewBag.EstadoID = new SelectList(db.Estados, "EstadoID", "UF");
                    return View(usuario);
                }

                Usuario usuarioSessao = Session["Usuario"] as Usuario;

                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                if (usuarioSessao.Adm) {
                    return RedirectToAction("Lista");
                } else {
                    return RedirectToAction("InfoUser", "Usuario", new { id = usuarioSessao.UsuarioID });
                }
            }

            ViewBag.EstadoID = new SelectList(db.Estados, "EstadoID", "UF");
            return View(usuario);
        }

        [HttpPost]
        [Authorize]
        public String ExcluirConfirmacao(int id) {
            Usuario usuarioBd = db.Usuarios.Find(id);

            db.Usuarios.Remove(usuarioBd);
            db.SaveChanges();

            Usuario userLogado = Session["Usuario"] as Usuario;
            if (User.Identity.IsAuthenticated && (userLogado.UsuarioID == id)) {
                FormsAuthentication.SignOut();
                Session.Abandon();
            }

            JavaScriptSerializer serializer = new JavaScriptSerializer();

            return serializer.Serialize("sucesso");
        }

        [HttpGet]
        [Authorize]
        public ActionResult InfoUser() {
            int userId = (Session["Usuario"] as Usuario).UsuarioID;
            Usuario usuario = db.Usuarios.Find(userId);
            usuario.Estado = new Estado();
            usuario.Estado = db.Estados.Find(usuario.EstadoID);
            return View(usuario);
        }
    }
}