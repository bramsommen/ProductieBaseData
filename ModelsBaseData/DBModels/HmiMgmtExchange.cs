using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ModelsBaseData
{
    public partial class HmiMgmtExchange
    {
        [Key]
        public long Id { get; set; }
        public string Machine { get; set; }
        public string Naam { get; set; }
        public string Omschrijving { get; set; }
        public string Value { get; set; }
    }
}
