using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ModelsBaseData
{
    public partial class CyclusType
    {
        public CyclusType()
        {
            Cyclus = new HashSet<Cyclus>();
        }

        public long Id { get; set; }
        public long MachineOnderdeelId { get; set; }
        public string Naam { get; set; }

        public virtual MachineOnderdeel MachineOnderdeel { get; set; }
        
        [JsonIgnore]
        public virtual ICollection<Cyclus> Cyclus { get; set; }
    }   
}
