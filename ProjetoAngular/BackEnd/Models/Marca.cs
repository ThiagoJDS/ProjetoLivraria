using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace BackEnd.Models
{
    public class Marca
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required(ErrorMessage = "Campo {0} é Obrigatório")]
        [Column(TypeName = "varchar(70)")]
        public string descricao { get; set; }

        public IEnumerable<Livro> Livros { get; set; }

        public Marca(int id, string descricao)
        {
            this.id = id;
            this.descricao = descricao;
        }
    }
}