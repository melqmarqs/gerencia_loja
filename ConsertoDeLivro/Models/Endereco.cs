using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsertoDeLivro.Models {
    public class Endereco {
        private String Logradouro = "Rua Maria Candida";
        private int Numero = 73;
        private String Bairro = "Sao Jose I";
        private String Cidade = "Paulínia";
        private String SiglaEstado = "SP";
        private String Estado = "São Paulo";
        private String Complemento = "";

        public String getEndCompleto() {
            return Logradouro + " - " + Numero + ", " + Bairro + " - " + Cidade + "/" + SiglaEstado; ;
        }

        public String getEndCurto() {
            return Logradouro + ", " + Numero + " - " + Bairro;
        }
    }
}