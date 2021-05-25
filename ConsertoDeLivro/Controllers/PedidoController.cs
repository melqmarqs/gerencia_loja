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
        public ActionResult ListaPedidos(bool exibirMsg = false) {
            if (exibirMsg)
                TempData["PedidoAguardando"] = "Seu pedido foi recebido e está na aba \"Aguardando\". Assim que ele for aceito para o ser realizado o serviço, o status dele mudará para \"Aceito\".";

            return View();

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

            if (pedido.Largura == null || pedido.Altura == null || pedido.Expessura == null || pedido.Valor == null ||
                pedido.Largura == "0" || pedido.Altura == "0" || pedido.Expessura == "0") {
                ViewBag.MensagemErro = "Todas as informações da dimensão do seu livro, precisam ser preenchidas corretamente.";
                ViewBag.MaterialID = new SelectList(db.Materiais, "Id", "Nome", pedido.MaterialID);
                return View(pedido);
            }

            pedido.Aceito = false;
            pedido.Entregue = false;
            pedido.Feito = false;
            pedido.Recusado = false;
            pedido.UsuarioID = (Session["Usuario"] as Usuario).UsuarioID;
            pedido.DataPedido = DateTime.Now.ToString("dd/MM/yyyy H:mm:ss");
            //pedido.Valor = "0";

            db.Pedidos.Add(pedido);
            db.SaveChanges();

            return RedirectToAction("ListaPedidos", new { exibirMsg = true });
        }

        public ActionResult DetalhesPedido(int idPedido) {
            Usuario usuario = Session["Usuario"] as Usuario;
            Pedido pedido = new Pedido();
            if (usuario != null) {
                if (usuario.Adm) {
                    pedido = db.Pedidos.Where(pdd => pdd.PedidoID == idPedido).First();
                } else {
                    pedido = db.Pedidos.Where(pdd => pdd.PedidoID == idPedido && usuario.UsuarioID == pdd.UsuarioID).First();
                }
            }

            return View(pedido);
        }

        public ActionResult Avaliacao() {
            List<Pedido> pedidos = db.Pedidos.Where(pdd => pdd.Avaliacao > 0).ToList();
            return View(pedidos);
        }

        public ActionResult getUltimasAvaliacoes() {
            List<Pedido> pedidos = db.Pedidos.Where(pdd => pdd.Avaliacao > 0).OrderByDescending(pdd => pdd.PedidoID).ToList();
            return Json(pedidos, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getMelhoresAvaliacoes() {
            List<Pedido> pedidos = db.Pedidos.Where(pdd => pdd.Avaliacao > 0).OrderByDescending(pdd => pdd.Avaliacao).ToList();
            return Json(pedidos, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getPioresAvaliacoes() {
            List<Pedido> pedidos = db.Pedidos.Where(pdd => pdd.Avaliacao > 0).OrderBy(pdd => pdd.Avaliacao).ToList();
            return Json(pedidos, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getPedidosAceitos() {
            Usuario usuario = Session["Usuario"] as Usuario;
            List<Pedido> pedidos = new List<Pedido>();
            if (usuario != null) {
                if (usuario.Adm)
                    pedidos = db.Pedidos.Where(pdd => pdd.Aceito && !pdd.Feito).OrderByDescending(desc => desc.PedidoID).ToList();
                else
                    pedidos = db.Pedidos.Where(pdd => pdd.Aceito && !pdd.Feito && pdd.UsuarioID == usuario.UsuarioID).OrderByDescending(desc => desc.PedidoID).ToList();
            }

            return Json(pedidos, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getPedidosAguardando() {
            Usuario usuario = Session["Usuario"] as Usuario;
            List<Pedido> pedidos = new List<Pedido>();
            if (usuario != null) {
                if (usuario.Adm)
                    pedidos = db.Pedidos.Where(pdd => !pdd.Aceito && !pdd.Recusado).OrderBy(ordem => ordem.PedidoID).ToList();
                else
                    pedidos = db.Pedidos.Where(pdd => !pdd.Aceito && !pdd.Recusado && pdd.UsuarioID == usuario.UsuarioID).OrderByDescending(desc => desc.PedidoID).ToList();
            }

            return Json(pedidos, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getPedidosRecusados() {
            Usuario usuario = Session["Usuario"] as Usuario;
            List<Pedido> pedidos = new List<Pedido>();
            if (usuario != null) {
                if (usuario.Adm)
                    pedidos = db.Pedidos.Where(pdd => pdd.Recusado).OrderByDescending(desc => desc.PedidoID).ToList();
                else
                    pedidos = db.Pedidos.Where(pdd => pdd.Recusado && pdd.UsuarioID == usuario.UsuarioID).OrderByDescending(desc => desc.PedidoID).ToList();
            }

            return Json(pedidos, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getPedidosFinalizados() {
            Usuario usuario = Session["Usuario"] as Usuario;
            List<Pedido> pedidos = new List<Pedido>();
            if (usuario != null) {
                if (usuario.Adm)
                    pedidos = db.Pedidos.Where(pdd => pdd.Feito).OrderByDescending(desc => desc.PedidoID).ToList();
                else
                    pedidos = db.Pedidos.Where(pdd => pdd.Feito && pdd.UsuarioID == usuario.UsuarioID).OrderByDescending(desc => desc.PedidoID).ToList();
            }

            return Json(pedidos, JsonRequestBehavior.AllowGet);
        }

        public ActionResult setAceitarPedido(int idPedido) {
            try {
                var pedido = db.Pedidos.Where(pdd => pdd.PedidoID == idPedido).ToArray()[0];
                if (pedido != null) {
                    pedido.Recusado = false;
                    pedido.Aceito = true;

                    db.Entry(pedido).State = EntityState.Modified;
                    db.SaveChanges();

                    return Json(Resposta.Sucesso, JsonRequestBehavior.AllowGet);
                } else {
                    return Json("Atenção: não foi encontrado nenhum pedido.", JsonRequestBehavior.AllowGet);
                }
            } catch (Exception) {
                return Json(Resposta.Erro, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult setFinalizarPedido(int idPedido) {
            try {
                var pedido = db.Pedidos.Where(pdd => pdd.PedidoID == idPedido).ToArray()[0];
                if (pedido != null) {
                    pedido.Recusado = false;
                    pedido.Aceito = true;
                    pedido.Feito = true;

                    db.Entry(pedido).State = EntityState.Modified;
                    db.SaveChanges();

                    return Json(Resposta.Sucesso, JsonRequestBehavior.AllowGet);
                } else {
                    return Json("Atenção: não foi encontrado nenhum pedido.", JsonRequestBehavior.AllowGet);
                }
            } catch (Exception) {
                return Json(Resposta.Erro, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult setEntregarPedido(int idPedido) {
            try {
                var pedido = db.Pedidos.Where(pdd => pdd.PedidoID == idPedido).ToArray()[0];
                if (pedido != null) {
                    pedido.Recusado = false;
                    pedido.Aceito = true;
                    pedido.Feito = true;
                    pedido.Entregue = true;

                    db.Entry(pedido).State = EntityState.Modified;
                    db.SaveChanges();

                    return Json(Resposta.Sucesso, JsonRequestBehavior.AllowGet);
                } else {
                    return Json("Atenção: não foi encontrado nenhum pedido.", JsonRequestBehavior.AllowGet);
                }
            } catch (Exception e) {
                return Json(Resposta.Erro, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult setRecusarPedido(int idPedido) {
            try {
                Pedido pedido = db.Pedidos.Where(pdd => pdd.PedidoID == idPedido).ToArray()[0];
                if (pedido != null) {
                    pedido.Aceito = false;
                    pedido.Feito = false;
                    pedido.Entregue = false;
                    pedido.Recusado = true;

                    db.Entry(pedido).State = EntityState.Modified;
                    db.SaveChanges();

                    return Json(Resposta.Sucesso, JsonRequestBehavior.AllowGet);
                } else {
                    return Json("Atenção: não foi encontrado nenhum pedido.", JsonRequestBehavior.AllowGet);
                }
            } catch (Exception e) {
                return Json(Resposta.Erro, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult setApagarPedido(int idPedido) {
            try {
                Pedido pedido = db.Pedidos.Where(pdd => pdd.PedidoID == idPedido).First();
                if (pedido != null) {
                    db.Entry(pedido).State = EntityState.Deleted;
                    db.SaveChanges();

                    return Json(Resposta.Sucesso, JsonRequestBehavior.AllowGet);
                } else {
                    return Json("Atenção: não foi encontrado nenhum pedido.", JsonRequestBehavior.AllowGet);
                }
            } catch (Exception) {
                return Json(Resposta.Erro, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult getPedidoById(int idPedido) {
            Pedido pedido = db.Pedidos.Where(pdd => pdd.PedidoID == idPedido).First();
            if (pedido != null) {
                return Json(pedido, JsonRequestBehavior.AllowGet);
            }

            return Json(Resposta.Falha, JsonRequestBehavior.AllowGet);
        }

        public ActionResult setAvaliacaoEComentario(int idPedido, int avaliacao, string comentario) {
            try {
                Pedido pedido = db.Pedidos.Where(pdd => pdd.PedidoID == idPedido).FirstOrDefault();
                if (pedido != null) {
                    pedido.Comentario = comentario;
                    pedido.Avaliacao = avaliacao;

                    db.Entry(pedido).State = EntityState.Modified;
                    db.SaveChanges();

                    return Json(Resposta.Sucesso, JsonRequestBehavior.AllowGet);
                } else {
                    return Json("Atenção: não foi encontrado nenhum pedido.", JsonRequestBehavior.AllowGet);
                }
            } catch (Exception e) {
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }
    }
}