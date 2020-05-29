using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ModelsBaseData;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class ProductVersieCyclusRepository : IProductVersieCyclusRepository
    {

        // PROPERTIES
        ProductieBaseDataContext DB;

        // CONSTRUCTOR
        public ProductVersieCyclusRepository(ProductieBaseDataContext dB)
        {
            DB = dB;
        }


        // CREATE
        public void Create(ProductVersieCyclus obj)
        {
            try
            {
                DB.ProductVersieCyclus.Add(obj);
            }
            catch (Exception)
            {
                throw;
            }
        }


        // READ
        public async Task<List<ProductVersieCyclus>> GetFrom(long versieID)
        {
            try
            {
                return await DB.ProductVersieCyclus.Where(x => x.ProductVersieId.Equals(versieID)).ToListAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<ProductVersieCyclus> GetFromID(long productCyclusMaakInstellingenID)
        {
            try
            {
                return await DB.ProductVersieCyclus.Where(x => x.Id.Equals(productCyclusMaakInstellingenID)).SingleOrDefaultAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        // UPDATE
        public void Update(ProductVersieCyclus obj)
        {
            try
            {
                DB.ProductVersieCyclus.Update(obj);
            }
            catch (Exception)
            {
                throw;
            }
        }


        // DELETE
        public void Delete(ProductVersieCyclus obj)
        {
            try
            {
                DB.ProductVersieCyclus.Remove(obj);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
