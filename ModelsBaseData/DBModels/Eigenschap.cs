using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ModelsBaseData
{
    public partial class Eigenschap
    {
        public Eigenschap()
        {
            CyclusMaakInstelling = new HashSet<CyclusMaakInstelling>();
            ProductEigenschap = new HashSet<ProductEigenschap>();
        }

        public long Id { get; set; }
        public long MachineOnderdeelId { get; set; }
        public int Sort { get; set; }
        public string Naam { get; set; }
        public string Omschrijving { get; set; }
        public string DataType { get; set; }
        public string GlobalEigenschap { get; set; }

        public virtual MachineOnderdeel MachineOnderdeel { get; set; }

        [JsonIgnore]
        public virtual ICollection<CyclusMaakInstelling> CyclusMaakInstelling { get; set; }

        [JsonIgnore]
        public virtual ICollection<ProductEigenschap> ProductEigenschap { get; set; }
    }
}
