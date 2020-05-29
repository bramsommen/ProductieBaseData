using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace ModelsBaseData

{
    public partial class GlobalProductEigenschap
    {
        public override string ToString()
        {
            return $"{this.ArtikelCode} PROP:  {this.Naam} - {this.DataType} - Value: {this.Waarde}";
        }
    }
}
