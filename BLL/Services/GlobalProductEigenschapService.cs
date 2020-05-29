using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DAL;
using ModelsBaseData;
using System.Threading.Tasks;
using BLL.Interfaces;
using DAL.Interfaces;
using System.Reflection.PortableExecutable;

namespace BLL.Services
{
    public class GlobalProductEigenschapService : IGlobalProductEigenschapService
    {
        // PROPERTIES
        private readonly IGlobalProductEigenschapRepository repository;
        private readonly IGlobalProductService globalProductService;

        private readonly IEigenschapService eigenschapservice;

        private readonly ProductieBaseDataContext DB;

        // CONSTRUCTOR
        public GlobalProductEigenschapService(IGlobalProductEigenschapRepository _repository, IGlobalProductService _globalProductService, IEigenschapService _eigenschapservice, ProductieBaseDataContext db)
        {
            repository = _repository;
            globalProductService = _globalProductService;
            eigenschapservice = _eigenschapservice;
            DB = db;
        }

        // CREATE
        public async Task Create(GlobalProductEigenschap obj)
        {
            try
            {
                if (!string.IsNullOrEmpty(obj.Naam))
                {
                    string name = obj.Naam;

                    if (name.Length >= 1)
                    // Er zijn één of meerdere karakters
                    {
                        if (!name.Substring(0, 1).Equals("@"))
                        // Als het eerste teken geen @ is, voeg dit dan toe
                        {
                            obj.Naam = $"@{name}";
                        }
                    }
                }

                // Save Tot DB
                repository.Create(obj);
                await DB.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task AddEigenschapFromMachineonderdeel(string artikelCode, long machineonderdeeliD)
        {
            try
            {
                // inlezen van Global product (Hoofd artikel)
                GlobalProduct globalProduct = await this.globalProductService.GetFromArtikelCode(artikelCode);

                // ophalen van alle eigenschappen van één machine odnerdeel
                List<Eigenschap> nieuweEigenschappen = await eigenschapservice.GetFromMachineOnderdeel(machineonderdeeliD);


                ////
                ///   
                ///      
                // Zoek naar onbrekende items
                // enkel de verschillen die voorkomen in de machine onderdeel eigenschappen worden toegevoegd aan de globale product eigenschappen

                List<string> bestaandeEigenschappen = globalProduct.Eigenschappen.Select(p => p.Naam)
                    .Distinct() // Enkel de unieke eigenschapsnamen uitlezen
                    .ToList();

                List<string> eigenschappen = nieuweEigenschappen
                    .Where(x => !string.IsNullOrEmpty(x.GlobalEigenschap)) // Geef enkel de values waar er een parent eigenschaps naam is opgegeven
                    .ToList().Select(p => p.GlobalEigenschap)
                    .Distinct() // Enkel de unieke eigenschapsnamen uitlezen
                    .ToList();

                // Zoek naar alle eigenschappen uit de machine onderdelen die ontbreken als globaal product eigenschap
                List<string> differenceQuery = eigenschappen.Except(bestaandeEigenschappen).ToList();


                ////
                ///   
                /// 



                foreach (string artikelpointer in differenceQuery)
                {
                    // Alle onbrekende eigenschappen toevoegen
                    Eigenschap org = nieuweEigenschappen.Where(x => x.GlobalEigenschap.Equals(artikelpointer)).FirstOrDefault();

                    if (org == null)
                    {
                        continue;
                    }

                    GlobalProductEigenschap newGpe = new GlobalProductEigenschap();
                    newGpe.Id = 0;
                    newGpe.ArtikelCode = artikelCode;
                    newGpe.Sort = org.Sort;
                    newGpe.Naam = org.GlobalEigenschap;
                    newGpe.Omschrijving = org.Omschrijving;
                    newGpe.DataType = org.DataType;
                    newGpe.Waarde = "";

                    // Save to DB
                    await this.Create(newGpe);
                }

                //
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // READ
        public async Task<GlobalProductEigenschap> GetFromID(long ID)
        {
            try
            {
                GlobalProductEigenschap tmpResult = await repository.GetFromID(ID);

                return tmpResult;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<GlobalProductEigenschap>> GetFromArtikelCode(string artikelCode)
        {
            try
            {
                List<GlobalProductEigenschap> tmpResult = await repository.GetFromArtikelCode(artikelCode);

                tmpResult = tmpResult.OrderBy(x => x.Naam).ToList();

                return tmpResult;
            }
            catch (Exception)
            {

                throw;
            }
        }

        // UPDATE
        public async Task Update(GlobalProductEigenschap obj)
        {
            try
            {

                if (!string.IsNullOrEmpty(obj.Naam))
                {
                    string name = obj.Naam;

                    if (name.Length >= 1)
                    // Er zijn één of meerdere karakters
                    {
                        if (!name.Substring(0, 1).Equals("@"))
                        // Als het eerste teken geen @ is, voeg dit dan toe
                        {
                            obj.Naam = $"@{name}";
                        }
                    }
                }



                repository.Update(obj);
                await DB.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        // DELETE
        public async Task Delete(GlobalProductEigenschap obj)
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
    }
}
