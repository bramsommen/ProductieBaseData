using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DAL;
using ModelsBaseData;
using System.Threading.Tasks;
using BLL.Interfaces;
using DAL.Interfaces;

namespace BLL.Services
{
    public class GlobalProductService : IGlobalProductService
    {
        // PROPERTIES
        private readonly IGlobalProductRepository repository;
        private readonly ProductieBaseDataContext DB;

        // CONSTRUCTOR
        public GlobalProductService(IGlobalProductRepository _repository, ProductieBaseDataContext db)
        {
            repository = _repository;
            DB = db;
        }

        // CREATE
        public async Task Create(GlobalProduct obj)
        {
            try
            {
                // Save Tot DB
                repository.Create(obj);
                await DB.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task CopyFrom(GlobalProduct obj, string _artikelCode)
        // Copieren van Global Product en alle onderliggende eigenschappen
        {
            try
            {
                // Inlezen van origineel van DB
                GlobalProduct gbp = await this.GetFromArtikelCode(obj.ArtikelCode);

                if (gbp == null)
                {
                    throw new Exception("Geen bron artikel gevonden in database");
                }

                // Maak nieuw object
                GlobalProduct newGbp = new GlobalProduct();
                newGbp.ArtikelCode = _artikelCode;
                newGbp.Naam = gbp.Naam;
                newGbp.Omschrijving = gbp.Omschrijving;
                newGbp.Eigenschappen = new List<GlobalProductEigenschap>();

                // Vul object met onderliggende eigenschappen
                foreach (GlobalProductEigenschap item in gbp.Eigenschappen)
                {
                    GlobalProductEigenschap gbpe = new GlobalProductEigenschap();
                    gbpe.Id = 0;
                    gbpe.ArtikelCode = newGbp.ArtikelCode == null ? "" : _artikelCode;
                    gbpe.Sort = item.Sort;
                    gbpe.Naam = item.Naam == null ? "" : item.Naam;
                    gbpe.Omschrijving = item.Omschrijving == null ? "" : item.Omschrijving;
                    gbpe.DataType = item.DataType == null ? "" : item.DataType;
                    gbpe.Waarde = item.Waarde == null ? "" : item.Waarde;

                    // Toevoegen van eigenschap aan parent
                    newGbp.Eigenschappen.Add(gbpe);
                }

                await this.Create(newGbp);


            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // READ
        public async Task<GlobalProduct> GetFromArtikelCode(string artikelCode)
        {
            try
            {
                GlobalProduct tmpResult = await repository.GetFromArtikelCode(artikelCode);

                // Sorteren van onderliggende eigenschappen
                tmpResult.Eigenschappen = tmpResult.Eigenschappen.OrderBy(x => x.Naam).ToList();

                return tmpResult;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<GlobalProduct>> GetAll()
        {
            try
            {
                List<GlobalProduct> tmpResult = await repository.GetAll();

                foreach (GlobalProduct item in tmpResult)

                // Sorteren van onderliggende eigenschappen
                {
                    item.Eigenschappen = item.Eigenschappen.OrderBy(x => x.Naam).ToList();
                }

                return tmpResult;
            }
            catch (Exception)
            {

                throw;
            }
        }

        // UPDATE
        public async Task Update(GlobalProduct obj)
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
        public async Task Delete(GlobalProduct obj)
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
