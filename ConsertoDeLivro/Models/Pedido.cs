using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsertoDeLivro.Models {
    public class Pedido {
        public int PedidoID { get; set; }

        public int UsuarioID { get; set; }

        public virtual Usuario Usuario { get; set; }

        public float Largura { get; set; }

        public float Comprimento { get; set; }

        public float Expessura { get; set; }

        public int MaterialID { get; set; }

        public virtual Material Material { get; set; }

        public bool Aceito { get; set; }

        public bool Feito { get; set; }

        public bool Entregue { get; set; }

        public string DataPedido { get; set; }

        public float Valor { get; set; }

        public string Descricao { get; set; }
    }
}