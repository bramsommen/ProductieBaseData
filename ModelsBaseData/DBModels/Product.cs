using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ModelsBaseData
{
    public partial class Product
    {
        public Product()
        {
            ProductVersie = new HashSet<ProductVersie>();
        }

        public long Id { get; set; }

        [ForeignKey("GlobalProduct")]
        public string ArtikelCode { get; set; }
        public virtual GlobalProduct GlobalProduct { get; set; }


     //   public string Omschrijving { get; set; }
        public long MachineOnderdeelId { get; set; }

        public virtual MachineOnderdeel MachineOnderdeel { get; set; }
        public virtual ICollection<ProductVersie> ProductVersie { get; set; }
    }   
}
