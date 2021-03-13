using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsertoDeLivro.Models {
    public class Material {
        public int Id { get; set; }

        public int Codigo { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }

    }
}