using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ModelsBaseData
{
    public partial class ProductVersie
    {
        public override string ToString()
        {
            if (Product!=null)
            {
                return $"{Product.ArtikelCode} - {Versie} - Status Code: {Status}";
            }

            return $"{Versie} - Status Code: {Status}";
        }


        // Methods
        //
        //


        // Ophalen van eigenschappen op basis van naam
        public ProductEigenschap GetEigenschapByName(string eigenschapNaam)
        {
            try
            {       

                // Query eigenschap
                ProductEigenschap pe = this.ProductEigenschap.Where(x => x.Eigenschap.Naam.Equals(eigenschapNaam)).FirstOrDefault();

                if (pe == null)
                {
                    throw new Exception("Geen eigenschap gevonden met overeenkomstige naam");
                }

                return pe;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // Ophalen van lijst maak instellingen op basis van cyclus type
        public List<CyclusMaakInstelling> GetCyclusMaakInstellingenFromCyclusType(string cyclusTypeNaam)
        {
            try
            {

                ProductVersieCyclus pvc = this.ProductVersieCyclus.Where(x => x.Cyclus.CyclusType.Naam.Equals(cyclusTypeNaam)).SingleOrDefault();

                if (pvc == null)
                {
                    throw new Exception("Geen product versie cyclys gevonden van het opgegeven cyclus type");
                }

                if (pvc.Cyclus == null)
                {
                    throw new Exception("Geen Cyclus object beschikbaar");
                }

                if (pvc.Cyclus.CyclusMaakInstelling == null)
                {
                    throw new Exception("Geen cyclus maak instellingen beschikbaar.");
                }

                return pvc.Cyclus.CyclusMaakInstelling.ToList();
            }
            catch (Exception ex)
            {
                return null;
            }

        }

    }
}
