using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DAL;
using ModelsBaseData;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace BLL
{
    public class ProductService : IProductService
    {
        // PROPERTIES
        private readonly IProductRepository repository;
        private readonly ProductieBaseDataContext DB;
        private readonly IProductVersieService productVersieService;

        // CONSTRUCTOR
        public ProductService(IProductRepository _repository, ProductieBaseDataContext db, IProductVersieService _productVersieService)
        {
            repository = _repository;
            DB = db;
            productVersieService = _productVersieService;
        }

        // CREATE
        public async Task Create(Product obj)
        {
            try
            {
                if (obj.MachineOnderdeelId == 0)
                {
                    throw new Exception("Geen poolnaam opgegeven");
                }

                if (obj.ArtikelCode == "")
                {
                    throw new Exception("Geen artikelcode opgegeven");
                }

                // UpperCase
                obj.ArtikelCode = obj.ArtikelCode.ToUpper();

                // Check of dit artikel code reeds bestaat oov deze pool
                List<Product> tmpProducts = await this.GetFrom(obj.MachineOnderdeelId);
                tmpProducts = tmpProducts.Where(x => x.ArtikelCode.Equals(obj.ArtikelCode)).ToList();

                if (tmpProducts.Count > 0)
                {
                    throw new Exception("Deze artikelcode bestaat reeds voor deze pool");
                }

                if (obj.ProductVersie != null)
                {
                    if (obj.ProductVersie.Count == 0)
                    // Als er nog geen versie aangemaakt is, maak er dan een
                    {
                        // nieuwe (blanco versie) toevoegen
                        obj.ProductVersie.Add(await productVersieService.NewVersieFromTemplate(obj, 0));
                    }
                }


                // Alles ok
                // Save nieuw product naar Database
                repository.Create(obj);
                await DB.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Copy(long productID)
        {
            try
            {
                // Source product voleldig inlezen
                Product refProduct = await this.GetFromID(productID);


                // Init nieuw product
                Product newProduct = new Product();
                newProduct.Id = 0;
                newProduct.ArtikelCode = refProduct.ArtikelCode + "(COPY)";
                newProduct.MachineOnderdeelId = refProduct.MachineOnderdeelId;


                // Laatste product versie zoeken die status "In Productie"(2) heeft
                ProductVersie pv = refProduct.ProductVersie.Where(x => x.Status == 2).OrderByDescending(x => x.Versie).FirstOrDefault();
                if (pv != null)
                {
                    ProductVersie newPV = await productVersieService.Copy(pv, 0);

                    if (newPV != null)
                    // Er is een nieuwe product versie aangemaakt
                    {
                        newProduct.ProductVersie.Add(newPV); // Toevoegen van nieuwe versie aan product
                    }
                }

                if (newProduct.ProductVersie.Count == 0)
                // Er is geen versie aangemaakt, maak dan ook het product niet aan
                {
                    return;
                }

                await this.Create(newProduct); // Save nieuw product naar database

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // READ
        public async Task<List<Product>> GetFrom(long MachineOnderdeelId)
        {
            try
            {
                List<Product> tmpResult = await repository.GetFrom(MachineOnderdeelId);
                tmpResult = tmpResult.OrderBy(x => x.ArtikelCode).ToList();

            
                return tmpResult; ;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<Product> GetFromID(long productID)
        {
            try
            {
                Product tmpResult = await repository.GetFromID(productID);

                // Valideer alle versie
                await ValidateAlleVersiesInProduct(tmpResult);

                return tmpResult; ;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<Product> GetFromArtikelCode(long machineOnderdeelID, string artikelCode)
        {
            try
            {
                Product tmpResult = await repository.GetFromArtikelCode(machineOnderdeelID, artikelCode);

                if (tmpResult!=null)
                {
                    if (tmpResult.ProductVersie.Count!=0)
                        // Als er versies aanwezig zijn in het opgehaalde product
                    {
                        // Valideer alle versie
                        await ValidateAlleVersiesInProduct(tmpResult);
                    }         
                }


                return tmpResult; ;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Product> GetLaatsteVersie(long machineOnderdeelID, string artikelCode)
        {
            try
            {
                Product tmpResult = await this.GetFromArtikelCode(machineOnderdeelID, artikelCode);

                if (tmpResult == null)
                {
                    return null;
                }

                // Zoek naar laatste versie
                ProductVersie pv = tmpResult.ProductVersie.Where(x => x.Status.Equals(2)).OrderByDescending(x => x.Versie).FirstOrDefault();

                if (pv == null)
                {
                    // Fail - Niet gevonden
                    return null;
                }

                // Valideer product versie

                string validatieResultaat = await productVersieService.ValidateVersie(pv.Id);

                if (validatieResultaat == "OK")
                // De productie versie is ok, en mag worden doorgestuurd naar productie
                {
                    tmpResult.ProductVersie.Clear(); // Delete alle versies

                    tmpResult.ProductVersie.Add(pv); // Voeg enkel de laatste versie toe aan het resultaat
                }
                else
                {
                    // Ophalen van ID uit gevonden versie
                    pv.Status = 0; // Reset status

                    await DB.SaveChangesAsync();

                    return null;
                }


                return tmpResult; ;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        // UPDATE
        public async Task Update(Product obj)
        {
            try
            {
                repository.Update(obj);
                await DB.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }


        // DELETE
        public async Task Delete(Product obj)
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

        // Methods
        private async Task ValidateAlleVersiesInProduct(Product product)
        // Valideer alle versies van een product.
        // Als de versie status "in productie heeft" en niet "OK" is, wordt deze versie in status "test" geplaatst
        {
            try
            {
                foreach (ProductVersie versie in product.ProductVersie.ToList())
                {
                    string validatieResultaat = await productVersieService.ValidateVersie(versie.Id);

                    if (validatieResultaat.ToUpper() != "OK")
                    // Als de validatie niet goed werd bevonden
                    {
                        if (versie.Status == 2)
                        // Deze versie staat "in productie"
                        {
                            // Set naar status "test"
                            versie.Status = 0;
                            await DB.SaveChangesAsync();
                        }
                    }
                }          


            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
