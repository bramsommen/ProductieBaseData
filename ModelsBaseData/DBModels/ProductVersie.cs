using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ModelsBaseData
{
    public partial class ProductVersie
    {
        public ProductVersie()
        {
            ProductEigenschap = new HashSet<ProductEigenschap>();
            ProductVersieCyclus = new HashSet<ProductVersieCyclus>();
        }

        public long Id { get; set; }
        public long ProductId { get; set; }
        public byte[] Foto { get; set; }
        public byte[] Cad3d { get; set; }
        public byte[] Cad2d { get; set; }
        public byte[] Pdf { get; set; }
        public string Naam { get; set; }
        public float Versie { get; set; }
        public int Status { get; set; }

        [JsonIgnore]
        public virtual Product Product { get; set; }

        public virtual ICollection<ProductEigenschap> ProductEigenschap { get; set; }

        public virtual ICollection<ProductVersieCyclus> ProductVersieCyclus { get; set; }
    }  
}