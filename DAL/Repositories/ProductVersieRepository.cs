using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ModelsBaseData;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DAL
{
    public class ProductVersieRepository : IProductVersieRepository
    {

        // PROPERTIES
        ProductieBaseDataContext DB;

        // CONSTRUCTOR
        public ProductVersieRepository(ProductieBaseDataContext dB)
        {
            DB = dB;
        }


        // CREATE
        public void Create(ProductVersie obj)
        {
            try
            {
                DB.ProductVersie.Add(obj);
            }
            catch (Exception)
            {
                throw;
            }
        }


        // READ
        public async Task<ProductVersie> GetFrom(long productVersieID)
        {
            try
            {
                return await DB.ProductVersie
                   // Nested PRODUCT EIGENSCHAPPEN opahlane
                   .Include(x => x.ProductEigenschap).ThenInclude(x => x.Eigenschap)
                   .Include(x => x.Product).ThenInclude(x=>x.GlobalProduct).ThenInclude(x=>x.Eigenschappen)

                    // Nested PRODUCT CYCLUS data ophalen
                    .Include(x => x.ProductVersieCyclus).ThenInclude(x => x.Cyclus).ThenInclude(x => x.CyclusType)
                    .Include(x => x.ProductVersieCyclus).ThenInclude(x => x.Cyclus).ThenInclude(x => x.CyclusMaakInstelling).ThenInclude(x => x.MaakInstelling)
                    .Include(x => x.ProductVersieCyclus).ThenInclude(x => x.Cyclus).ThenInclude(x => x.CyclusMaakInstelling).ThenInclude(x => x.ProductEigenschap)

                    .Where(x => x.Id.Equals(productVersieID)).SingleOrDefaultAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<List<ProductVersie>> GetFromProduct(long productID)
        {
            try
            {
                return await DB.ProductVersie
                    .Include(x => x.Product).ThenInclude(x => x.GlobalProduct).ThenInclude(x => x.Eigenschappen)
                    .Where(x => x.ProductId.Equals(productID))
                    .OrderBy(x => x.Versie)
                    .ToListAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        // UPDATE
        public void Update(ProductVersie obj)
        {
            try
            {
                DB.ProductVersie.Update(obj);
            }
            catch (Exception)
            {
                throw;
            }
        }


        // DELETE
        public void Delete(ProductVersie obj)
        {
            try
            {
                DB.ProductVersie.Remove(obj);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
