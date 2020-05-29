using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ModelsBaseData;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class GlobalProductRepository : IGlobalProductRepository
    {

        // PROPERTIES
        private readonly ProductieBaseDataContext DB;

        // CONSTRUCTOR
        public GlobalProductRepository(ProductieBaseDataContext dB)
        {
            DB = dB;
        }


        // CREATE
        public void Create(GlobalProduct obj)
        {
            try
            {
                obj.ArtikelCode = obj.ArtikelCode.ToUpper();
                DB.GlobalProduct.Add(obj);
            }
            catch (Exception)
            {
                throw;
            }
        }


        // READ
        public async Task<GlobalProduct> GetFromArtikelCode(string artikelCode)
        {
            try
            {
                return await DB.GlobalProduct
                    .Include(x => x.Eigenschappen)
                    .Where(x => x.ArtikelCode.Equals(artikelCode)).SingleOrDefaultAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<List<GlobalProduct>> GetAll()
        {
            try
            {
                return await DB.GlobalProduct
                       .Include(x => x.Eigenschappen)
                       .ToListAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        // UPDATE
        public void Update(GlobalProduct obj)
        {
            try
            {
                obj.ArtikelCode = obj.ArtikelCode.ToUpper();
                DB.GlobalProduct.Update(obj);
            }
            catch (Exception)
            {
                throw;
            }
        }


        // DELETE
        public void Delete(GlobalProduct obj)
        {
            try
            {
                DB.GlobalProduct.Remove(obj);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
