using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DAL;
using ModelsBaseData;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace BLL
{
    public class ProductVersieCyclusService : IProductVersieCyclusService
    {
        // PROPERTIES
        IProductVersieCyclusRepository repository;
        ProductieBaseDataContext DB;
        ICyclusMaakInstellingService CyclusMaakInstellingenService;
        IProductVersieRepository productVersieRepository;

        // CONSTRUCTOR
        public ProductVersieCyclusService(IProductVersieCyclusRepository _repository, ProductieBaseDataContext db, ICyclusMaakInstellingService _CyclusMaakInstellingenService, IProductVersieRepository _productVersieRepository)
        {
            repository = _repository;
            DB = db;
            CyclusMaakInstellingenService = _CyclusMaakInstellingenService;
            productVersieRepository = _productVersieRepository;
        }

        // CREATE
        public async Task Create(ProductVersieCyclus obj)
        {
            try
            {
                // Reset Product Versie status naar "Test"
                ProductVersie pv = await productVersieRepository.GetFrom(obj.ProductVersieId);

                if (pv == null)
                {
                    return;
                }

                if (pv.Status.Equals(2))
                // Als de Product Versie status "productie" heeft, omschakelen naar "test" status
                {
                    pv.Status = 1;
                }

                // Update object

                repository.Create(obj);
                await DB.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task CreateAll(long productVersieID, long cyclusID)
        // Zoek alle Maak Instellingen met dezelfde cyclus ID en voeg deze toe aan een product
        {

        }

        // READ
        public async Task<List<ProductVersieCyclus>> GetFrom(long versieID)
        {
            try
            {
                List<ProductVersieCyclus> tmpResult = await repository.GetFrom(versieID);

                return tmpResult;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ProductVersieCyclus> GetFromID(long productCyclusMaakInstellingenID)
        {
            try
            {
                ProductVersieCyclus tmpResult = await repository.GetFromID(productCyclusMaakInstellingenID);


                return tmpResult; ;
            }
            catch (Exception)
            {

                throw;
            }
        }


        // UPDATE
        public async Task Update(ProductVersieCyclus obj)
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
        public async Task Delete(ProductVersieCyclus obj)
        {
            try
            {
                // Reset Product Versie status naar "Test"
                ProductVersie pv = await productVersieRepository.GetFrom(obj.ProductVersieId);

                if (pv == null)
                {
                    return;
                }

                if (pv.Status.Equals(2))
                // Als de Product Versie status "productie" heeft, omschakelen naar "test" status
                {
                    pv.Status = 1;


                }

                pv.ProductVersieCyclus.Remove(pv.ProductVersieCyclus.Where(x => x.Id.Equals(obj.Id)).SingleOrDefault());

                obj.Cyclus = null;

                await DB.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
