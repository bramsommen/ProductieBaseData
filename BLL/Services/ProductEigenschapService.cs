using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DAL;
using ModelsBaseData;
using System.Threading.Tasks;

namespace BLL
{
    public class ProductEigenschapService : IProductEigenschapService
    {
        // PROPERTIES
        IProductEigenschapRepository repository;
        ProductieBaseDataContext DB;
        IProductVersieRepository productVersieRepository;

        // CONSTRUCTOR
        public ProductEigenschapService(IProductEigenschapRepository _repository, ProductieBaseDataContext db, IProductVersieRepository _productVersieRepository)
        {
            repository = _repository;
            DB = db;
            productVersieRepository = _productVersieRepository;
        }

        // CREATE
        public async Task Create(ProductEigenschap obj)
        {
            try
            {
                repository.Create(obj);
                await DB.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }


        // READ
        public async Task<List<ProductEigenschap>> GetFrom(long versieID)
        {
            try
            {
                List<ProductEigenschap> tmpResult = await repository.GetFrom(versieID);

                tmpResult = tmpResult.OrderBy(x => x.Eigenschap.Sort).ToList();

                return tmpResult; ;
            }
            catch (Exception)
            {

                throw;
            }
        }


        // UPDATE
        public async Task Update(ProductEigenschap obj)
        {
            try
            {
                // Update Eigenschap
                repository.Update(obj);

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

                // Save to DB
                await DB.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        // DELETE
        public async Task Delete(ProductEigenschap obj)
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
