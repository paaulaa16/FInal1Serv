using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Influencers.Models
{
    public class Influencer
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int InfluencerId { get; set; }
        public string Manager { get; set; }
        [JsonIgnore]
        public List<Contrato> Contratos { get; } = new List<Contrato>();
      
        public override string ToString() => $"#{InfluencerId} {Manager} ({Contratos.Count} Contratos)";
    }
    public class Producto
    {
        [Key]
        public string ProductoId { get; set; }
        public int ValorEconomico { get; set; }
        [JsonIgnore]

        public List<Contrato> Contratos { get; } = new List<Contrato>();

        public override string ToString() => $"#{ProductoId} {ValorEconomico} ({Contratos.Count} Contratos)";
    }
    public class Contrato
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ContratoId { get; set; }
        public int InfluencerId { get; set; }
        public string ProductoId { get; set; }        
        public int Cantidad { get; set; }
        [JsonIgnore]
        public Influencer Influencers { get; set; }
        [JsonIgnore]
        public Producto Productos { get; set; }

        public override string ToString() => $"{InfluencerId}x{ProductoId}";
    }

}