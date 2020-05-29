using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsBaseData
{
    public partial class Product
    {
        public override string ToString()
        {
            if (this.MachineOnderdeel != null)
            {
                return $"{ArtikelCode} - Machine Onderdeel: {this.MachineOnderdeel.Naam}";
            }

            return $"{ArtikelCode}";
        }


        // Methods
        //
        //


        // Ophalen van eigenschappen op basis van naam
        public ProductEigenschap GetEigenschapByName(string eigenschapNaam)
        {
            try
            {
                if (this.ProductVersie.Count > 1)
                // Fail als er meer dan 1 versie is
                {
                    throw new Exception("Deze functie kan enkel gebruikt worden als er maar één versie in het artikel wordt meegegeven. Ref functie: GetLaatsteVersie");
                }

                if (this.ProductVersie.Count.Equals(0))
                // FAIL als er geen versies zijn
                {
                    throw new Exception("Geen versie beschikbaar");
                }

                // FAIL geen product eigenschappen in Versie
                if (this.ProductVersie.ToList()[0].ProductEigenschap == null)
                {
                    throw new Exception("Geen product eigenschappen in versie");
                }

                // Lees de lijst ven eigenschappen uit versie
                List<ProductEigenschap> _eigenschappen = this.ProductVersie.ToList()[0].ProductEigenschap.ToList();

                // Query eigenschap
                ProductEigenschap pe = _eigenschappen.Where(x => x.Eigenschap.Naam.Equals(eigenschapNaam)).FirstOrDefault();

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
                if (this.ProductVersie.Count > 1)
                // Fail als er meer dan 1 versie is
                {
                    throw new Exception("Deze functie kan enkel gebruiket worden als er maar één versie in het artikel wordt meegegeven. Ref functie: GetLaatsteVersie");
                }

                if (this.ProductVersie.Count.Equals(0))
                // FAIL als er geen versies zijn
                {
                    throw new Exception("Geen versie beschikbaar");
                }

                ProductVersieCyclus pvc = this.ProductVersie.ToList()[0].ProductVersieCyclus.Where(x => x.Cyclus.CyclusType.Naam.Equals(cyclusTypeNaam)).SingleOrDefault();

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
