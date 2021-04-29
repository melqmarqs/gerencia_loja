using ConsertoDeLivro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace ConsertoDeLivro.Controllers {
    public class PedidoController : Controller {
        ConsertoDeLivroContext db = new ConsertoDeLivroContext();
        // GET: Pedido
        public ActionResult ListaPedidos() {
            Usuario userLogado = (Session["Usuario"] as Usuario);
            List<Pedido> pedidos = new List<Pedido>();
            
            if (userLogado.UsuarioID != 0 && !userLogado.Adm) {
                pedidos = db.Pedidos.Where(v => v.UsuarioID == userLogado.UsuarioID).ToList();                
            } else if (userLogado.Adm) {
                pedidos = db.Pedidos.Include(i => i.Material).ToList();
            }

            return View(pedidos);

        }

        [HttpGet]
        public ActionResult CriarNovoPedido() {
            ViewBag.MaterialID = new SelectList(db.Materiais, "Id", "Nome");
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult CriarNovoPedido(Pedido pedido) {
            if (!ModelState.IsValid) {
                ViewBag.MaterialID = new SelectList(db.Materiais, "Id", "Nome");
                return View(pedido);
            }

            pedido.Aceito = false;
            pedido.Entregue = false;
            pedido.Feito = false;
            pedido.UsuarioID = (Session["Usuario"] as Usuario).UsuarioID;
            pedido.DataPedido = DateTime.Now.ToString("dd/MM/yyyy H:mm:ss");
            pedido.Valor = "0";

            db.Pedidos.Add(pedido);
            db.SaveChanges();

            return RedirectToAction("ListaPedidos", new { id = pedido.UsuarioID });
        }
    }
}