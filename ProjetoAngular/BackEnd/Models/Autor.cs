using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace BackEnd.Models
{
    public class Autor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required(ErrorMessage = "Campo {0} é Obrigatório")]
        [Column(TypeName = "varchar(70)")]
        public string nome { get; set; }

        public IEnumerable<Livro> Livros { get; set; }

        public Autor(int id, string nome)
        {
            this.id = id;
            this.nome = nome;
        }

    }
}