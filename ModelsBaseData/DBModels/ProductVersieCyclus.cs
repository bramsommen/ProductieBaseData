using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ModelsBaseData
{
    public partial class ProductVersieCyclus
    {
        public long Id { get; set; }
        public long ProductVersieId { get; set; }
        public long CyclusId { get; set; }

        public virtual Cyclus Cyclus { get; set; }

        [JsonIgnore]
        public virtual ProductVersie ProductVersie { get; set; }
    }
}
