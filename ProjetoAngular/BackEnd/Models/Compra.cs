using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace BackEnd.Models
{
    public class Compra
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [ForeignKey("Cliente")]
        [Required(ErrorMessage = "Campo {0} é Obrigatório")]
        public int clienteId { get; set; }
        public Cliente cliente { get; set; }

        [Required(ErrorMessage = "Campo {0} é Obrigatório")]
        public double total { get; set; }
        
        public IEnumerable<CompraItem> CompraItems { get; set; }

        public Compra(int id, int clienteId, double total)
        {
            this.id = id;
            this.clienteId = clienteId;
            this.total = total;
        }
    }
}