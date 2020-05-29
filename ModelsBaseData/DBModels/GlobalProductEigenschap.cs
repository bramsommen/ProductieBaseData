using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;


namespace ModelsBaseData
{
    [Table("GlobalProductEigenschap")]
    public partial class GlobalProductEigenschap
    {
        public GlobalProductEigenschap()
        {

        }

        [Key]
        [Column("Id")]
        public long Id { get; set; }

        [MaxLength(50)]
        [ForeignKey("GlobalProduct")]
        [Column("ArtikelCode")]
        public string ArtikelCode { get; set; }

        [Column("Sort")]
        public int Sort { get; set; }

        [Column("Naam")]
        public string Naam { get; set; }

        [Column("Omschrijving")]
        public string Omschrijving { get; set; }

        [Column("DataType")]
        public string DataType { get; set; }

        [Column("Waarde")]
        public string Waarde { get; set; }


        [JsonIgnore]
        public virtual GlobalProduct GlobalProduct { get; set; }


    }
}
