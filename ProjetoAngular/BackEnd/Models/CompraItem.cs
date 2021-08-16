using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models
{
    public class CompraItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required(ErrorMessage = "Campo {0} é Obrigatório")]
        public double valorUnitario { get; set; }

        [Required(ErrorMessage = "Campo {0} é Obrigatório")]
        public int quantidade { get; set; }
        
        [ForeignKey("Livro")]
        [Required(ErrorMessage = "Campo {0} é Obrigatório")]
        public int livroId { get; set; }
        public Livro livro { get; set; }

        [ForeignKey("Compra")]
        [Required(ErrorMessage = "Campo {0} é Obrigatório")]
        public int compraId { get; set; }
        public Compra compra { get; set; }


        public CompraItem(int id, double valorUnitario, int quantidade, int livroId, int compraId)
        {
            this.id = id;
            this.valorUnitario = valorUnitario;
            this.quantidade = quantidade;
            this.livroId = livroId;
            this.compraId = compraId;
        }
    }
}