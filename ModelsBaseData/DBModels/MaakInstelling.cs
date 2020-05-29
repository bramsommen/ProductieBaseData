using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ModelsBaseData
{
    public partial class MaakInstelling
    {
        public MaakInstelling()
        {
            CyclusMaakInstelling = new HashSet<CyclusMaakInstelling>();
        }

        public long Id { get; set; }
        public long MachineOnderdeelId { get; set; }
        public string Naam { get; set; }
        public string Omschrijving { get; set; }
        public string DataType { get; set; }

        [JsonIgnore]
        public virtual MachineOnderdeel MachineOnderdeel { get; set; }

        [JsonIgnore]
        public virtual ICollection<CyclusMaakInstelling> CyclusMaakInstelling { get; set; }
    }
}
