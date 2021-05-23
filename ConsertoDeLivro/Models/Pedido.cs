using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsertoDeLivro.Models {
    public class Pedido {
        public int PedidoID { get; set; }

        public int UsuarioID { get; set; }

        public virtual Usuario Usuario { get; set; }

        public string Largura { get; set; }

        public string Altura { get; set; }

        public string Expessura { get; set; }

        public int MaterialID { get; set; }

        public virtual Material Material { get; set; }

        public bool Aceito { get; set; }

        public bool Feito { get; set; }

        public bool Entregue { get; set; }

        public string DataPedido { get; set; }

        public string Valor { get; set; }

        public string Descricao { get; set; }

        public bool Recusado { get; set; }

        public string NomeLivro { get; set; }

        public string AutorLivro { get; set; }

        public int Avaliacao { get; set; }

        public string Comentario { get; set; }
    }
}