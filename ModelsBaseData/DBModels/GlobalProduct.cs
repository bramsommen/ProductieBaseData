using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace ModelsBaseData
{
    [Table("GlobalProduct")]
    public partial class GlobalProduct
    {
        public GlobalProduct()
        {
            Eigenschappen = new HashSet<GlobalProductEigenschap>();
        }

        [Key]
        [MaxLength(50)]
        [Column("ArtikelCode")]
        public string ArtikelCode { get; set; }

        [Column("Naam")]
        public string Naam { get; set; }

        [Column("Omschrijving")]
        public string Omschrijving { get; set; }

        public virtual ICollection<GlobalProductEigenschap> Eigenschappen { get; set; }
    }
}
