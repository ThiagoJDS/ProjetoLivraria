using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models
{
    public class Administrador
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required(ErrorMessage = "Campo {0} é Obrigatório")]
        [Column(TypeName = "varchar(70)")]
        public string nome { get; set; }

        [Required(ErrorMessage = "Campo {0} é Obrigatório")]
        [Column(TypeName = "varchar(100)")]
        [Display(Name = "e-mail")]
        [EmailAddress(ErrorMessage = "É necessário ser um {0} válido")]
        public string email { get; set; }

        [Required(ErrorMessage = "Campo {0} é Obrigatório")]
        [Column(TypeName = "varchar(20)")]
        public string senha { get; set; }

        public Administrador(int id, string nome, string email, string senha)
        {
            this.id = id;
            this.nome = nome;
            this.email = email;
            this.senha = senha;
        }
    }
}