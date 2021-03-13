using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ConsertoDeLivro.Models {
    //[Table("tbl_usuarios")]
    public class Usuario {
        [Display(Name = "ID")]
        public int UsuarioID {get;set;}

        public bool Adm { get; set; }

        public bool Dev { get; set; }

        [Required(ErrorMessage = "Esse campo é obrigatório.")]
        public string Nome { get; set; }

        [Display(Name ="Último nome")]
        public string UltimoNome { get; set; }

        [MinLength(5, ErrorMessage = "O campo precisa ter no mínimo, 5 caracteres.")]
        [RegularExpression(@"[a-z0-9.]+\@[a-z0-9]+\.([a-z]+(\.[a-z]+){0,})", ErrorMessage = "Insira um valor de Email válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Esse campo é obrigatório.")]
        [RegularExpression(@"^([0-9]{3}[.]{0,1}){3}[-]{0,1}[0-9]{2}$", ErrorMessage = "O número de CPF informado não é válido.")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "Esse campo é obrigatório.")]
        [RegularExpression(@"^\({0,1}[1-9]{2}\){0,1}([ ]{0,1})(9[1-9]([0-9]{3}))\-{0,1}[0-9]{4}$", ErrorMessage = "Insira um número de celular válido.")]
        public string Celular { get; set; }

        [Required(ErrorMessage = "Esse campo é obrigatório.")]
        [DataType(DataType.Password)]
        [MaxLength(13, ErrorMessage = "O limite de 13 caracteres foi excedido.")]
        [MinLength(6, ErrorMessage = "É necessário ter pelo menos 6 caracteres.")]
        public string Senha { get; set; }

        [RegularExpression(@"[0-9]{5}\-{0,1}[0-9]{3}", ErrorMessage = "Insira um CEP válido.")]
        public string CEP { get; set; }

        public string Logradouro { get; set; }

        public string Numero { get; set; }

        public string Bairro { get; set; }

        public string Cidade { get; set; }

        public string Complemento { get; set; }

        public int EstadoID { get; set; }

        public virtual Estado Estado { get; set; }
    }
}