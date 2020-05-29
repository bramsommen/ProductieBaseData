using System;
using System.Collections.Generic;
using System.Text;

namespace ModelsBaseData
{
    public partial class GlobalProduct
    {
        public override string ToString()
        {
            return $"{this.ArtikelCode} - {this.Naam} - {this.Omschrijving}";
        }
    }
}
