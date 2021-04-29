using ConsertoDeLivro.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConsertoDeLivro.Controllers {
    public class MaterialController : Controller {
        ConsertoDeLivroContext db = new ConsertoDeLivroContext();
        // GET: Material
        public ActionResult ListaMateriais() {
            return View();
        }

        public ActionResult Materiais() {
            var materiais = db.Materiais.ToList();
            return Json(materiais, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize]
        public ActionResult CriarMaterial() {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult CriarMaterial(Material material) {
            if (!ModelState.IsValid) {
                return View(material);
            }

            db.Materiais.Add(material);
            db.SaveChanges();

            return RedirectToAction("ListaMateriais");
        }

        [HttpGet]
        [Authorize]
        public ActionResult ApagarMaterial(int id) {
            var material = db.Materiais.Find(id);
            if (material != null) {
                db.Materiais.Remove(material);
                db.SaveChanges();
            }

            return RedirectToAction("ListaMateriais");
        }

        [HttpGet]
        [Authorize]
        public ActionResult AtualizarMaterial(int id) {
            Material material = db.Materiais.Find(id);
            return View(material);
        }

        [HttpPost]
        [Authorize]
        public ActionResult AtualizarMaterial(Material material) {
            if (!ModelState.IsValid) {
                ViewBag.Erro = "Por favor, preencha os campos corretamente.";
                return View(material);
            }

            db.Entry(material).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ListaMateriais");
        }
    }
}