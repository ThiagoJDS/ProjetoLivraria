using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace BackEnd.Models
{
    public class Classificacao
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required(ErrorMessage = "Campo {0} é Obrigatório")]
        [Column(TypeName = "varchar(100)")]
        public string descricao { get; set; }

        [Required(ErrorMessage = "Campo {0} é Obrigatório")]
        public int anos { get; set; }

        public IEnumerable<Livro> Livros { get; set; }

        public Classificacao(int id, string descricao, int anos)
        {
            this.id = id;
            this.descricao = descricao;
            this.anos = anos;
        }
    }
}