using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models
{
    public class Cliente
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

        [Required(ErrorMessage = "Campo {0} é Obrigatório")]
        [Column(TypeName = "varchar(11)")]
        public string cpf { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        [Required(ErrorMessage = "Campo {0} é Obrigatório")]
        [DataType(DataType.Date)]
        public DateTime dataNascimento { get; set; }

        [Required(ErrorMessage = "Campo {0} é Obrigatório")]
        [Column(TypeName = "varchar(20)")]
        public string sexo { get; set; }

        [Required(ErrorMessage = "Campo {0} é Obrigatório")]
        [Column(TypeName = "varchar(20)")]
        [Phone(ErrorMessage = "O campo {0} está com número inválido")]
        public string celular { get; set; }

        [Required(ErrorMessage = "Campo {0} é Obrigatório")]
        [Column(TypeName = "varchar(100)")]
        public string estado { get; set; }

        [Required(ErrorMessage = "Campo {0} é Obrigatório")]
        [Column(TypeName = "varchar(100)")]
        public string cidade { get; set; }

        [Required(ErrorMessage = "Campo {0} é Obrigatório")]
        [Column(TypeName = "varchar(100)")]
        public string endereco { get; set; }
        

        public Cliente(int id, string nome, string email, string senha,string cpf, DateTime dataNascimento, string sexo, string celular, string estado, string cidade, string endereco)
        {
            this.id = id;
            this.nome = nome;
            this.email = email;
            this.senha = senha;
            this.cpf = cpf;
            this.dataNascimento = dataNascimento;
            this.sexo = sexo;
            this.celular = celular;
            this.estado = estado;
            this.cidade = cidade;
            this.endereco = endereco;
        }
    }
}