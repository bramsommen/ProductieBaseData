using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DAL;
using ModelsBaseData;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using BaseDataValidators.MainLogic;

namespace BLL
{
    public class ProductVersieService : IProductVersieService
    {
        // PROPERTIES
        private readonly IProductVersieRepository repository;

        private readonly ProductieBaseDataContext DB;

        // Source Services
        private readonly IEigenschapService eigenschapService;



        // CONSTRUCTOR
        public ProductVersieService(
            IProductVersieRepository _repository,
            ProductieBaseDataContext db,
            IEigenschapService _eigenschapService)
        {
            repository = _repository;
            DB = db;

            eigenschapService = _eigenschapService;
        }


        // CREATE
        public async Task Create(ProductVersie obj)
        // Maak een 
        {
            try
            {
                repository.Create(obj);
                await DB.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task CreateFromTemplate(Product product)
        // Maak nieuw element aan op basis van Template
        {
            // 1 Zoek alle bestaande versies
            List<ProductVersie> versies = await repository.GetFromProduct(product.Id);

            if (versies == null)
            {
                return;
            }

            float hoogteVersie = 0; // Present Hoogste nummer

            if (versies.Count > 0)
            {
                hoogteVersie = versies[versies.Count - 1].Versie; // Hoogste versie zoeken in lijst
            }

            // Creëer nieuw product versie
            await this.Create(await NewVersieFromTemplate(product, hoogteVersie)); // Nieuwe versie aanmaken
        }


        public async Task<ProductVersie> NewVersieFromTemplate(Product product, float hoogteVersie)
        // Maak een nieuwe product versie op basis van de template eigenschappen
        {

            // Maak nieuwe versie aan
            ProductVersie newProductVersie = new ProductVersie();
            newProductVersie.Id = 0;
            newProductVersie.ProductId = product.Id;
            newProductVersie.Naam = "Nieuw";
            newProductVersie.Versie = hoogteVersie + 1;
            newProductVersie.Status = 0;


            //  Zoek alle Eigenschappen van deze pool
            List<Eigenschap> tmplEigenschappen = await this.eigenschapService.GetFromMachineOnderdeel(product.MachineOnderdeelId);

            foreach (Eigenschap item in tmplEigenschappen)
            // Voeg Alle templates toe als Blanco Product Eigensap
            {
                ProductEigenschap pe = new ProductEigenschap();
                pe.Id = 0;
                pe.ProductVersieId = newProductVersie.Id;
                pe.EigenschapId = item.Id;
                pe.Waarde = "";
                pe.Check = false;

                newProductVersie.ProductEigenschap.Add(pe);
            }

            return newProductVersie;

        }

        public async Task CreateNewFromOtherVersion(ProductVersie obj)
        {
            try
            {
                // 1 Zoek alle bestaande versies
                List<ProductVersie> versies = await repository.GetFromProduct(obj.ProductId);

                if (versies == null)
                {
                    return;
                }

                float hoogteVersie = 0; // Present Hoogste nummer

                if (versies.Count > 0)
                {
                    hoogteVersie = versies[versies.Count - 1].Versie; // Hoogste versie zoeken in lijst
                }


                // Maak nieuwe versie aan
                ProductVersie newProductVersie = await this.Copy(obj, obj.ProductId);
                // Aanpassingen tov Copy methode
                newProductVersie.Naam = obj.Naam + "(COPY)";
                newProductVersie.Versie = hoogteVersie + 1;

                await this.Create(newProductVersie);

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<ProductVersie> Copy(ProductVersie sourceProductVersie, long productID)
        // Maak een nieuw product versie aan en copy alle onderliggende eigenschappen en cyclus instellingen
        {
            try
            {
                ProductVersie newPV = new ProductVersie();
                newPV.Id = 0;
                newPV.ProductId = productID;
                newPV.Foto = sourceProductVersie.Foto;
                newPV.Cad3d = sourceProductVersie.Cad3d;
                newPV.Cad2d = sourceProductVersie.Cad2d;
                newPV.Pdf = sourceProductVersie.Pdf;
                newPV.Naam = sourceProductVersie.Naam;
                newPV.Versie = 1;
                newPV.Status = 0;

                newPV.Product = null;



                // Eigenschappen kopieren
                if (sourceProductVersie.ProductEigenschap != null)
                // Er zijn product eigenschappen in de source versie
                {
                    foreach (ProductEigenschap pe in sourceProductVersie.ProductEigenschap)
                    {
                        ProductEigenschap newPe = new ProductEigenschap();
                        newPe.Id = 0;
                        newPe.ProductVersieId = 0;
                        newPe.EigenschapId = pe.EigenschapId;
                        newPe.Waarde = pe.Waarde;
                        newPe.Check = pe.Check;

                        newPe.Eigenschap = null;
                        newPe.ProductVersie = null;

                        newPV.ProductEigenschap.Add(newPe); // Nieuwe product eigenschap toevoegen aan product versie
                    }
                }



                // Versie Maak Instellingen
                if (sourceProductVersie.ProductVersieCyclus != null)
                {
                    foreach (ProductVersieCyclus pvc in sourceProductVersie.ProductVersieCyclus)
                    {
                        ProductVersieCyclus newpvc = new ProductVersieCyclus();
                        newpvc.Id = 0;
                        newpvc.ProductVersieId = 0;
                        newpvc.CyclusId = pvc.CyclusId;

                        newpvc.Cyclus = null;

                        newpvc.ProductVersie = null;

                        newPV.ProductVersieCyclus.Add(newpvc); // Nieuwe product versie cyclus  toevoegen aan product versie
                    }
                }


                return newPV;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // READ
        public async Task<ProductVersie> GetFrom(long productVersieID)
        {
            try
            {
                // Lees Actuele DataBase
                ProductVersie tmpResult = await repository.GetFrom(productVersieID);

                this.UpdateVersie(ref tmpResult);

                return tmpResult; ;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // Validate Versie
        public async Task<string> ValidateVersie(long productVersieID)
        // Valideer product versie
        {
            try
            {
                // Haal versie op
                ProductVersie pv = await this.GetFrom(productVersieID);

                if (pv == null)
                {
                    return "Producte Versie niet gevonden";
                }

                if (pv.Product == null)
                {
                    return "Fout bij inladen onderliggend product van Product Versie";
                }

                Ivalidator validator = ValidatorCollection.GetValidators().Where(x => x.MachineOnderdeelID.Equals(pv.Product.MachineOnderdeelId)).FirstOrDefault();

                if (validator == null)
                {
                    return $"Geen validator gevonden voor machine onderdeel met id: {pv.Product.MachineOnderdeelId}";
                }

                string validatorResult = validator.Validate(pv);

                if (validatorResult.ToUpper() !="OK")
                {

                }

                return validatorResult;

            }
            catch (Exception ex)
            {

                throw;
            }
        }


        // UPDATE
        public async Task Update(ProductVersie obj)
        {
            try
            {
                repository.Update(obj);
                await DB.SaveChangesAsync();


                if (obj.Status.Equals(2))
                // Als deze versie status product heeft
                {
                    // Zoek alle andere versies met status productie
                    List<ProductVersie> lst = await repository.GetFromProduct(obj.ProductId);

                    lst = lst.Where(x => x.Status.Equals(2) && x.Id != obj.Id).ToList();

                    foreach (ProductVersie item in lst)
                    {
                        item.Status = 1; // Set naar test

                        repository.Update(item);
                        await DB.SaveChangesAsync();
                    }

                    lst = null;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        // DELETE
        public async Task Delete(ProductVersie obj)
        {
            try
            {
                repository.Delete(obj);
                await DB.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateVersie(ref ProductVersie pv)
        // Bijwerken van versie
        {

            if (pv == null)
            {
                return;
            }

            // Waarde van eigenschap actualiseren met gegevens uit Product Versie
            UpdateWaardeVanEigenschap(ref pv);

            // Match globale eigenschappen met versie eigenschappen - actuele waarden
            ZoekEigenschapswaardeinGlobalproduct(ref pv);

            // Sorteer eigenschappen
            pv.ProductEigenschap = pv.ProductEigenschap.OrderBy(x => x.Eigenschap.Sort).ToList();

            // Sorteer Maak Cyclus Maak Instellingen
            foreach (ProductVersieCyclus versie in pv.ProductVersieCyclus.ToList())
            {
                versie.Cyclus.CyclusMaakInstelling = versie.Cyclus.CyclusMaakInstelling.OrderBy(x => x.Stap).ThenBy(x => x.ChildStap).ToList();
            }
        }

        // Waarde van maakinstelling uit product eigenschappen halen en actualiseren.
        private void UpdateWaardeVanEigenschap(ref ProductVersie pv)
        {
            if (pv == null)
            {
                return;
            }

            if (pv.ProductVersieCyclus == null)
            {
                return;
            }

            foreach (ProductVersieCyclus cyclus in pv.ProductVersieCyclus)
            // Alle Cyclussen aflopen
            {
                foreach (CyclusMaakInstelling item in cyclus.Cyclus.CyclusMaakInstelling)
                // Alle Cyclus maak instellingen aflopen en bijwerken
                {
                    if (item.ProductEigenschap == null)
                    // Als er geen onderliggend product eigenschap is, gebruik dan de static waarde
                    {
                        item.Waarde = item.StaticWaarde;
                        continue;
                    }

                    ProductEigenschap productEigenschap = pv.ProductEigenschap.Where(x => x.Eigenschap.Id.Equals(item.ProductEigenschap.Id)).FirstOrDefault();

                    if (productEigenschap == null)
                    // Geen product eigenschap
                    {
                        item.Waarde = "";
                        continue;
                    }

                    if (string.IsNullOrEmpty(productEigenschap.Waarde))
                    // Geen waarde in product eigenschap
                    {
                        item.Waarde = "";
                    }

                    // Waarde is beschikbaar
                    item.Waarde = productEigenschap.Waarde;
                }
            }
        }


        private void ZoekEigenschapswaardeinGlobalproduct(ref ProductVersie pv)
            // Match globale eigenschappen met versie eigenschappen - actuele waarden
        {
            try
            {
                // Zoek naar globale eigenschappen
                if (pv.Product == null)
                    // Geen machone onderdeel product object aanwezig
                {
                    return;
                }

                if (pv.Product.GlobalProduct == null)
                    //Geen globaal product aanwezig
                {
                    return;
                }

                if (pv.Product.GlobalProduct.Eigenschappen == null)
                    // Er zijn geen eigenschappen om te bekijken
                {
                    return;
                }

                // Get Lijst van globale eigenschappen
                List<GlobalProductEigenschap> globaleProductEigenschappen = pv.Product.GlobalProduct.Eigenschappen.ToList();


                //
                //
                // overloop alle eigenschappen in versie en neem de actuele waardes uit het globaal product mee

                if (pv.ProductEigenschap == null)
                    // Er zijn geen product eigenschappen
                {
                    return;
                }

                foreach (ProductEigenschap productEigenschap in pv.ProductEigenschap)
                {
                    GlobalProductEigenschap gpe = globaleProductEigenschappen.Where(x => x.Naam.Equals(productEigenschap.Eigenschap.GlobalEigenschap)).SingleOrDefault();

                    if (gpe == null)
                    {
                        continue;
                    }

                    productEigenschap.Waarde = gpe.Waarde;
                }


            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
