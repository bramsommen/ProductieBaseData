using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ModelsBaseData
{
    public partial class CyclusMaakInstelling
    {
        public long Id { get; set; }
        public long CyclusId { get; set; }
        public long MaakInstellingId { get; set; }
        public int Stap { get; set; }
        public int ChildStapVolgorde { get; set; }
        public int ChildStap { get; set; }
        public long? ProductEigenschapId { get; set; }
        public string StaticWaarde { get; set; }
        public bool Check { get; set; }

        [JsonIgnore]
        public virtual Cyclus Cyclus { get; set; }

        public virtual MaakInstelling MaakInstelling { get; set; }

        public virtual Eigenschap ProductEigenschap { get; set; }
    }    
}
