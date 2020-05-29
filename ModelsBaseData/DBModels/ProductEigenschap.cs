using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ModelsBaseData
{
    public partial class ProductEigenschap
    {
        public long Id { get; set; }
        public long ProductVersieId { get; set; }
        public long EigenschapId { get; set; }
        public string Waarde { get; set; }
        public bool Check { get; set; }

        public virtual Eigenschap Eigenschap { get; set; }

        [JsonIgnore]
        public virtual ProductVersie ProductVersie { get; set; }
    }
}
