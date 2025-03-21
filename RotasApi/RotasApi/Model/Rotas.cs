using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace RotasApi.Model
{
    [Table("rotas")]
    public class Rotas
    {
        [Key]
        [JsonIgnore]
        [Column("id", TypeName = "int")]
        public int id { get; set; }

        [Column("origem", TypeName = "string")]
        public string Origem { get; set; }

        [Column("destino", TypeName = "string")]
        public string Destino { get; set; }

        [Column("valor", TypeName = "decimal")]
        public decimal Valor { get; set; }

        public Rotas(string origem, string destino, decimal valor)
        {
            Origem = origem;
            Destino = destino;
            Valor = valor;
        }
    }
}
