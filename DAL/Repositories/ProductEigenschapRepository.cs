using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ModelsBaseData;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
  public  class ProductEigenschapRepository : IProductEigenschapRepository
    {

        // PROPERTIES
        ProductieBaseDataContext DB;

        // CONSTRUCTOR
        public ProductEigenschapRepository(ProductieBaseDataContext dB)
        {
            DB = dB;
        }


        // CREATE
        public void Create(ProductEigenschap obj)
        {
            try
            {
                DB.ProductEigenschap.Add(obj);
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
                return await DB.ProductEigenschap.Where(x => x.ProductVersieId.Equals(versieID)).ToListAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        // UPDATE
        public void Update(ProductEigenschap obj)
        {
            try
            {
                DB.ProductEigenschap.Update(obj);
            }
            catch (Exception)
            {
                throw;
            }
        }


        // DELETE
        public void Delete(ProductEigenschap obj)
        {
            try
            {
                DB.ProductEigenschap.Remove(obj);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
