using System;
using System.Collections.Generic;
using System.Text;

namespace ModelsBaseData
{
    public partial class ProductVersieCyclus
    {
        public override string ToString()
        {
            if (this.ProductVersie != null && this.Cyclus!=null)
            {
                if (this.ProductVersie.Product!=null)
                {
                    return $"{ProductVersie.Product.ArtikelCode} - {ProductVersie.Versie} :: {Cyclus.Naam}";
                }             
            }

            return base.ToString(); 
        }
    }
}
