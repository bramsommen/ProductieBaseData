using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ModelsBaseData;
using System.Threading.Tasks;

namespace DAL
{
    public class ProductRepository : IProductRepository
    {
        // PROPERTIES
        private readonly ProductieBaseDataContext DB;

        // CONSTRUCTOR
        public ProductRepository(ProductieBaseDataContext dB)
        {
            DB = dB;
        }


        // CREATE
        public void Create(Product obj)
        {
            try
            {
                obj.ArtikelCode = obj.ArtikelCode.ToUpper();
                DB.Product.Add(obj);
            }
            catch (Exception)
            {
                throw;
            }
        }


        // READ
        public async Task<List<Product>> GetFrom(long machineOnderdeelID)
        // Ophalen van producten op basis van machine onderdeel ID
        {
            try
            {
                return await DB.Product
                    .Include(x => x.GlobalProduct).ThenInclude(x => x.Eigenschappen)
                    //.Include(x => x.ProductVersie).ThenInclude(x => x.ProductEigenschap).ThenInclude(x => x.Eigenschap)

                    //.Include(x => x.MachineOnderdeel)

                    // .Include(x => x.ProductVersie).ThenInclude(x => x.ProductVersieCyclus).ThenInclude(x => x.Cyclus).ThenInclude(x => x.CyclusType)

                    //.Include(x => x.ProductVersie).ThenInclude(x => x.ProductVersieCyclus).ThenInclude(x => x.Cyclus).ThenInclude(x => x.CyclusMaakInstelling)
                    //.Include(x => x.ProductVersie).ThenInclude(x => x.ProductVersieCyclus).ThenInclude(x => x.Cyclus).ThenInclude(x => x.CyclusMaakInstelling).ThenInclude(x => x.ProductEigenschap)
                    //.Include(x => x.ProductVersie).ThenInclude(x => x.ProductVersieCyclus).ThenInclude(x => x.Cyclus).ThenInclude(x => x.CyclusMaakInstelling).ThenInclude(x => x.MaakInstelling)

                    .Where(x => x.MachineOnderdeel.Id.Equals(machineOnderdeelID))
                    .ToListAsync();
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
                return await DB.Product
                                                   .Include(x => x.GlobalProduct).ThenInclude(x => x.Eigenschappen)
                    .Include(x => x.ProductVersie).ThenInclude(x => x.ProductEigenschap).ThenInclude(x => x.Eigenschap)

                         .Include(x => x.MachineOnderdeel)

                     .Include(x => x.ProductVersie).ThenInclude(x => x.ProductVersieCyclus).ThenInclude(x => x.Cyclus).ThenInclude(x => x.CyclusType)

                    .Include(x => x.ProductVersie).ThenInclude(x => x.ProductVersieCyclus).ThenInclude(x => x.Cyclus).ThenInclude(x => x.CyclusMaakInstelling)
                    .Include(x => x.ProductVersie).ThenInclude(x => x.ProductVersieCyclus).ThenInclude(x => x.Cyclus).ThenInclude(x => x.CyclusMaakInstelling).ThenInclude(x => x.ProductEigenschap)
                    .Include(x => x.ProductVersie).ThenInclude(x => x.ProductVersieCyclus).ThenInclude(x => x.Cyclus).ThenInclude(x => x.CyclusMaakInstelling).ThenInclude(x => x.MaakInstelling)

                    .Where(x => x.Id.Equals(productID)).SingleOrDefaultAsync();
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
                return await DB.Product
                                    .Include(x => x.GlobalProduct).ThenInclude(x => x.Eigenschappen)
                     .Include(x => x.ProductVersie).ThenInclude(x => x.ProductEigenschap).ThenInclude(x => x.Eigenschap)

                    .Include(x => x.MachineOnderdeel)

                     .Include(x => x.ProductVersie).ThenInclude(x => x.ProductVersieCyclus).ThenInclude(x => x.Cyclus).ThenInclude(x => x.CyclusType)

                    .Include(x => x.ProductVersie).ThenInclude(x => x.ProductVersieCyclus).ThenInclude(x => x.Cyclus).ThenInclude(x => x.CyclusMaakInstelling)
                    .Include(x => x.ProductVersie).ThenInclude(x => x.ProductVersieCyclus).ThenInclude(x => x.Cyclus).ThenInclude(x => x.CyclusMaakInstelling).ThenInclude(x => x.ProductEigenschap)
                    .Include(x => x.ProductVersie).ThenInclude(x => x.ProductVersieCyclus).ThenInclude(x => x.Cyclus).ThenInclude(x => x.CyclusMaakInstelling).ThenInclude(x => x.MaakInstelling)

                    .Where(x => x.MachineOnderdeel.Id.Equals(machineOnderdeelID) && x.ArtikelCode.Equals(artikelCode)).SingleOrDefaultAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        // UPDATE
        public void Update(Product obj)
        {
            try
            {
                obj.ArtikelCode = obj.ArtikelCode.ToUpper();
                DB.Product.Update(obj);
            }
            catch (Exception)
            {
                throw;
            }
        }


        // DELETE
        public void Delete(Product obj)
        {
            try
            {
                DB.Product.Remove(obj);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
