using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models
{
    public class Livro
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required(ErrorMessage = "Campo {0} é Obrigatório")]
        [Column(TypeName = "varchar(70)")]
        public string nome { get; set; }

        [Required(ErrorMessage = "Campo {0} é Obrigatório")]
        public double edicao { get; set; }

        [Required(ErrorMessage = "Campo {0} é Obrigatório")]
        public int pagina { get; set; }

        [Required(ErrorMessage = "Campo {0} é Obrigatório")]
        [Column(TypeName = "varchar(20)")]
        public string tipoCapa { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Campo {0} é Obrigatório")]
        public DateTime dataPublicacao { get; set; }

        [ForeignKey("Segmento")]
        [Required(ErrorMessage = "Campo {0} é Obrigatório")]
        public int segmentoId { get; set; }
        public Segmento segmento { get; set; }

        [ForeignKey("Marca")]
        [Required(ErrorMessage = "Campo {0} é Obrigatório")]
        public int marcaId { get; set; }
        public Marca marca { get; set; }

        [ForeignKey("Autor")]
        [Required(ErrorMessage = "Campo {0} é Obrigatório")]
        public int autorId { get; set; }
        public Autor autor { get; set; }

        [ForeignKey("Classificacao")]
        [Required(ErrorMessage = "Campo {0} é Obrigatório")]
        public int classificacaoId { get; set; }
        public Classificacao classificacao { get; set; }

        [ForeignKey("Genero")]
        [Required(ErrorMessage = "Campo {0} é Obrigatório")]
        public int generoId { get; set; }
        public Genero genero { get; set; }

        [Required]
        [RegularExpression(@".*\.(gif|jpe?g|bmp|png)$", ErrorMessage = "Não é uma imagem válida. (gif, jpg, jpeg, bmp ou png)")]
        public string imagemURL { get; set; }

        public IEnumerable<CompraItem> compraItems { get; set; }

        public Livro(int id, string nome, double edicao, int pagina, string tipoCapa, DateTime dataPublicacao, int segmentoId, int marcaId, int autorId, int classificacaoId, int generoId, string imagemURL)
        {
            this.id = id;
            this.nome = nome;
            this.edicao = edicao;
            this.pagina = pagina;
            this.tipoCapa = tipoCapa;
            this.dataPublicacao = dataPublicacao;
            this.segmentoId = segmentoId;
            this.marcaId = marcaId;
            this.autorId = autorId;
            this.classificacaoId = classificacaoId;
            this.generoId = generoId;
            this.imagemURL = imagemURL;
        }
    }
}