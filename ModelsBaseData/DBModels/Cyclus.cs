using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ModelsBaseData
{
    public partial class Cyclus
    {
        public Cyclus()
        {
            CyclusMaakInstelling = new HashSet<CyclusMaakInstelling>();
            ProductCyclus = new HashSet<ProductVersieCyclus>();
        }

        public long Id { get; set; }
        public long MachineOnderdeelId { get; set; }
        public string Naam { get; set; }
        public long CyclusTypeId { get; set; }

        public virtual CyclusType CyclusType { get; set; }

        public virtual MachineOnderdeel MachineOnderdeel { get; set; }

        public virtual ICollection<CyclusMaakInstelling> CyclusMaakInstelling { get; set; }

        [JsonIgnore]
        public virtual ICollection<ProductVersieCyclus> ProductCyclus { get; set; }
    }
}
