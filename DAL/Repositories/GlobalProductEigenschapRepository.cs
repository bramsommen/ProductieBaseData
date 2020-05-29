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
    public class GlobalProductEigenschapRepository : IGlobalProductEigenschapRepository
    {

        // PROPERTIES
        private readonly ProductieBaseDataContext DB;

        // CONSTRUCTOR
        public GlobalProductEigenschapRepository(ProductieBaseDataContext dB)
        {
            DB = dB;
        }


        // CREATE
        public void Create(GlobalProductEigenschap obj)
        {
            try
            {
                DB.GlobalProductEigenschap.Add(obj);
            }
            catch (Exception)
            {
                throw;
            }
        }


        // READ
        public async Task<GlobalProductEigenschap> GetFromID(long ID)
        {
            try
            {
                return await DB.GlobalProductEigenschap
                    .Include(x => x.ArtikelCode)
                    .Where(x => x.Id.Equals(ID)).SingleOrDefaultAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<List<GlobalProductEigenschap>> GetFromArtikelCode(string artikelCode)
        {
            try
            {
                return await DB.GlobalProductEigenschap
                           .Include(x => x.ArtikelCode)
                           .Where(x => x.ArtikelCode.Equals(artikelCode))
                           .ToListAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        // UPDATE
        public void Update(GlobalProductEigenschap obj)
        {
            try
            {
                DB.GlobalProductEigenschap.Update(obj);
            }
            catch (Exception)
            {
                throw;
            }
        }


        // DELETE
        public void Delete(GlobalProductEigenschap obj)
        {
            try
            {
                DB.GlobalProductEigenschap.Remove(obj);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
