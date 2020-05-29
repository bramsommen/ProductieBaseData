using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ModelsBaseData
{
    public partial class MachineOnderdeel
    {
        public MachineOnderdeel()
        {
            Cyclus = new HashSet<Cyclus>();
            CyclusType = new HashSet<CyclusType>();
            Eigenschap = new HashSet<Eigenschap>();
            MaakInstelling = new HashSet<MaakInstelling>();
            Product = new HashSet<Product>();
        }

        public long Id { get; set; }
        public string Machine { get; set; }
        public string Naam { get; set; }
        public string Omschrijving { get; set; }

        [JsonIgnore]
        public virtual ICollection<Cyclus> Cyclus { get; set; }

        [JsonIgnore]
        public virtual ICollection<CyclusType> CyclusType { get; set; }

        [JsonIgnore]
        public virtual ICollection<Eigenschap> Eigenschap { get; set; }

        [JsonIgnore]
        public virtual ICollection<MaakInstelling> MaakInstelling { get; set; }

        [JsonIgnore]
        public virtual ICollection<Product> Product { get; set; }
    }
}
